using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Good : MonoBehaviour
{
    private static GameObject target;
    private bool check;

    public void AllGoToSleep()
    {
        for (int i = 0; i < GameObject.FindObjectsOfType<Animal>().Length; i++)
        {
            GameObject.FindObjectsOfType<Animal>()[i].Energy = -100;
        }
    }

    public void AllAnimalsFindTarget()
    {
        float distance = float.MaxValue;
        target = GameObject.FindGameObjectWithTag("EdibleFlora");
        for (int i = 0; i < GameObject.FindGameObjectsWithTag("EdibleFlora").Length; i++)
        {
            GameObject preTarget = GameObject.FindGameObjectsWithTag("EdibleFlora")[i];
            float newDistanceF = Vector3.Distance(GameObject.FindGameObjectsWithTag("Animal")[0].transform.position, preTarget.transform.position);
            if (newDistanceF < distance)
            {
                distance = newDistanceF;
                target = preTarget;
            }
        }
    }

    #region TestCommand
    public void AllAnimalsGoToTarget()
    {
        if (target != null)
        {
            check = true;
        }
    }


    private void FixedUpdate()
    {
        if (check)
        {
            for (int i = 0; i < GameObject.FindGameObjectsWithTag("Animal").Length; i++)
            {
                //GameObject target = this.gameObject.GetComponent<TargetFinder>().target;
                NavMeshAgent agent = GameObject.FindGameObjectsWithTag("Animal")[i].GetComponent<NavMeshAgent>();
                agent.isStopped = false;
                agent.destination = target.transform.position;
                if (agent.hasPath == false)
                {
                    //agent.ResetPath();
                    agent.destination = target.transform.position;
                }
                Debug.Log(agent.isStopped);
                GameObject.FindGameObjectsWithTag("Animal")[i].transform.rotation = Quaternion.LookRotation(agent.velocity.normalized);
                //Debug.Log(GameObject.FindGameObjectsWithTag("Animal")[i].GetComponent<NavMeshAgent>().hasPath);
            }
        }
    }
    #endregion
}

