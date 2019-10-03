using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Movement : MonoBehaviour
{
    private NavMeshAgent agent;
    private bool check = false;
    private bool MovingStart = false;
    public bool drawPath;

    private void Start()
    {
        agent = this.gameObject.GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
    }
    public void GoTo()
    {
        check = !check;
        MovingStart = true;
    }

    private void FixedUpdate()
    {
        if (check)
        {
            GameObject target = this.gameObject.GetComponent<TargetFinder>().target;
            agent.destination = target.transform.position;
            transform.rotation = Quaternion.LookRotation(agent.velocity.normalized);
            if (MovingStart && agent.hasPath&& !agent.pathPending)
            {
                DrawPath();
                MovingStart = false;
            }
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
