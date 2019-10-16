using UnityEngine;
using UnityEngine.AI;

public class Movement : MonoBehaviour
{
    private NavMeshAgent agent;
    private bool check = false;
    private bool MovingStart = false;
    private GameObject target;
    public bool drawPath;
    private Animal Animal;
    public bool finishPath;

    private void Start()
    {
        agent = this.gameObject.GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        Animal = this.gameObject.GetComponent<Animal>();
    }
    public void GoTo(GameObject newGameObject)
    {
        target = newGameObject;
        MovingStart = true;
        Moving();
    }

    private void Moving()
    {
        agent.destination = target.transform.position;
        agent.isStopped = false;
        check = !check;
    }

    private void Update()
    {
        if (check)
        {
            //finish path
            if (!agent.pathPending && Animal != null && Animal.state == Animal.State.GoingToEat && agent.remainingDistance <= target.GetComponent<Collider>().bounds.extents.magnitude * 2)
            {
                agent.isStopped = true;
                agent.ResetPath();
                Animal.state = Animal.State.Eating;
                finishPath = true;
                check = false;
            }
            //freset path
            else if (!agent.pathPending && Animal != null && Animal.state == Animal.State.Walk && agent.remainingDistance <= 0.5f) 
            {
                agent.isStopped = true;
                finishPath = true;
                agent.ResetPath();
                Destroy(GameObject.Find("Moving_" + this.gameObject.name));
                Animal.state = Animal.State.Stay;
                check = false;
            }
        }
        if (MovingStart && !agent.pathPending)
        {
            DrawPath();
            MovingStart = false;
        }
        if (agent.hasPath && !agent.pathPending)
        {
            transform.rotation = Quaternion.LookRotation(agent.velocity.normalized);
        }
    }

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
