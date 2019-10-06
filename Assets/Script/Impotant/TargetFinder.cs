using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TargetFinder : MonoBehaviour
{
    [HideInInspector]
    public GameObject target;
    private GameObject testTarget;
    private List<GameObject> targets = new List<GameObject>();
    public float smellRadius;
    public GameObject TargetFinderObject;
    private NavMeshAgent agent;
    private bool pathFound;
    private string foodTag;
    private bool stopCour;

    private void Start()
    {
        agent = this.gameObject.GetComponent<NavMeshAgent>();
    }
    public void FindTarget(Animal.TypeOfFood typeOfFood)
    {
        TargetFinderObject.GetComponent<SphereCollider>().enabled = true;
        if (targets.Count<=0)
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
    
    IEnumerator Timer()
    {
        while (!stopCour)
        {
            yield return new WaitForFixedUpdate();
            TargetFinderObject.GetComponent<SphereCollider>().radius += (smellRadius*Time.deltaTime);
        }
    }
    private void Update()
    {
        if (!pathFound)
        {
            if (agent.hasPath)
            {
                if (!agent.pathPending)
                {
                    if (agent.path.status == NavMeshPathStatus.PathComplete)
                    {
                        pathFound = true;
                        agent.ResetPath();
                    }
                    else
                        agent.ResetPath();
                }

            }
            if (pathFound)
            {
                StopCor();
                if(this.gameObject.GetComponent<Animal>().state == Animal.State.SearchForEat)
                {
                    this.gameObject.GetComponent<Animal>().target = testTarget;
                    this.gameObject.GetComponent<Animal>().state = Animal.State.ReadyGoToEat;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag== foodTag)
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
