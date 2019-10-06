using UnityEngine;
using UnityEngine.AI;

public class Movement : MonoBehaviour
{
    private NavMeshAgent agent;
    private bool check = false;
    private bool MovingStart = false;
    private GameObject target;
    public bool drawPath;

    private void Start()
    {
        agent = this.gameObject.GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
    }
    public void GoTo(GameObject newGameObject)
    {
        target = newGameObject;
        check = !check;
        Moving();
        MovingStart = true;
    }

    private void Moving()
    {
        if (check)
        {
            agent.destination = target.transform.position;
            transform.rotation = Quaternion.LookRotation(agent.velocity.normalized);
            agent.isStopped = false;
            if (MovingStart)
            {
                DrawPath();
                MovingStart = false;
            }
        }
    }

    private void Update()
    {
        if (check)
        {
            if (!agent.pathPending && agent.remainingDistance <= target.GetComponent<Collider>().bounds.extents.magnitude * 2)
            {
                agent.isStopped = true;
                agent.ResetPath();
                this.gameObject.GetComponent<Animal>().state = Animal.State.Eating;
                check = false;
            }
        }
    }
    //private void OnCollisionEnter(Collision collision)
    //{
    //    Debug.Log(collision.gameObject.name);
    //    if(collision.gameObject == target)
    //    {
    //        agent.isStopped = true;
    //        agent.ResetPath();
    //        this.gameObject.GetComponent<Animal>().state = Animal.State.Eating;
    //        check = false;
    //    }
    //}
    [System.Obsolete]
    private void DrawPath()
    {
        if (agent == null || agent.path == null)
            return;

        LineRenderer line = this.GetComponent<LineRenderer>();
        if (line == null)
        {
            line = this.gameObject.AddComponent<LineRenderer>();
            line.material = new Material(Shader.Find("Sprites/Default")) { color = Color.yellow };
            line.SetWidth(0.5f, 0.5f);
            line.SetColors(Color.yellow, Color.yellow);

        }

        NavMeshPath path = agent.path;
        line.SetVertexCount(path.corners.Length);
        line.SetPositions(path.corners);
    }
}
