using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TargetFinder : MonoBehaviour
{
    [HideInInspector]
    public GameObject target;
    private GameObject testTarget;
    public GameObject TargetFinderObject;
    private List<GameObject> targets = new List<GameObject>();

    public GameObject plane;

    private NavMeshAgent agent;

    public float smellCoefficient;
    public float smellMaxRadius;
    public float maxXOffset = 40;
    public float maxZOffset = 40;

    public int iter = 0;
    private string foodTag;

    private bool stopCour = true;
    private bool pathFound = true;

    private void Start()
    {
        agent = this.gameObject.GetComponent<NavMeshAgent>();
    }
    public void FindTarget(Animal.TypeOfFood typeOfFood)
    {
        stopCour = false;
        pathFound = false;
        TargetFinderObject.GetComponent<SphereCollider>().enabled = true;
        if (targets.Count <= 0)
            StartCoroutine(Timer());
        switch (typeOfFood)
        {
            case Animal.TypeOfFood.Grass:
                foodTag = "EdibleFlora";
                break;
            case Animal.TypeOfFood.Meet:
                foodTag = "EdibleFouna";
                break;
        }

    }
    public void FindNextPosition()
    {
        pathFound = false;
        stopCour = false;
        GameObject movingTo = new GameObject("Moving_" + this.gameObject.name);
        Vector3 pos = transform.position;
        pos = new Vector3(pos.x + Random.Range(-maxXOffset, maxXOffset), pos.y, pos.z + Random.Range(-maxZOffset, maxZOffset));
        if (plane == null)
            pos.y = Terrain.activeTerrain.SampleHeight(pos);
        else
        {
            pos.y = Mathem.FindYOnPlane(pos.x,pos.z,plane);
        }
        movingTo.transform.position = pos;
        agent.destination = movingTo.transform.position;
        testTarget = movingTo;
    }

    IEnumerator Timer()
    {
        while (!stopCour)
        {
            yield return new WaitForFixedUpdate();
            TargetFinderObject.GetComponent<SphereCollider>().radius += (smellCoefficient * Time.deltaTime);
            if (TargetFinderObject.GetComponent<SphereCollider>().radius >= smellMaxRadius)
            {
                TargetFinderObject.GetComponent<SphereCollider>().radius = 0;
            }
        }
    }
    private void Update()
    {
        if (!pathFound)
        {
            Debug.Log(agent.pathPending);
            if (agent.hasPath && !agent.pathPending)
            {
                if (agent.path.status == NavMeshPathStatus.PathComplete)
                {
                    pathFound = true;
                    agent.ResetPath();
                }
                else
                {
                    agent.ResetPath();
                    if (this.gameObject.GetComponent<Animal>().state == Animal.State.FindingNextPosition)
                    {
                        FindNextPosition();
                        Destroy(GameObject.Find("Moving_" + this.gameObject.name));
                        StopCor();
                    }
                }
            }
            else if (!agent.hasPath && !agent.pathPending)
            {
                agent.ResetPath();
                if (this.gameObject.GetComponent<Animal>().state == Animal.State.FindingNextPosition)
                {
                    FindNextPosition();
                    Destroy(GameObject.Find("Moving_" + this.gameObject.name));
                    StopCor();
                }
            }
            if (pathFound)
            {
                StopCor();
                if (this.gameObject.GetComponent<Animal>().state == Animal.State.SearchForEat)
                {
                    this.gameObject.GetComponent<Animal>().target = testTarget;
                    this.gameObject.GetComponent<Animal>().state = Animal.State.ReadyGoToEat;
                }
                if (this.gameObject.GetComponent<Animal>().state == Animal.State.FindingNextPosition)
                {
                    this.gameObject.GetComponent<Animal>().state = Animal.State.FoundNextPosition;
                    this.gameObject.GetComponent<Animal>().target = testTarget;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == foodTag)
        {
            agent.destination = other.gameObject.transform.position;
            testTarget = other.gameObject;
        }
    }
    private void StopCor()
    {
        TargetFinderObject.GetComponent<SphereCollider>().radius = 100f;
        TargetFinderObject.GetComponent<SphereCollider>().enabled = false;
        stopCour = true;
        StopCoroutine(Timer());
    }
}
