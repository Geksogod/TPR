using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestingMavMeshAgent : MonoBehaviour
{
    bool check = false;
    NavMeshAgent agent;
    void Start()
    {
        agent = this.gameObject.GetComponent<NavMeshAgent>();
        agent.isStopped = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {

            agent.destination = GameObject.Find("Cactus_s_BT_06").transform.position;
        }
        if (agent.hasPath)
        {
            Debug.Log(agent.pathPending);
            Debug.Log(agent.pathStatus);
            agent.ResetPath();
        }
        
    }

    public void TestNavMesh()
    {
        agent.destination = GameObject.Find("Cactus_s_BT_06").transform.position;
        Debug.Log(agent.pathPending);
    }
}
