using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Movement : MonoBehaviour
{
    bool check = false;
    public void GoTo()
    {
        check = !check;
    }

    private void Update()
    {
        if (check)
        {
            GameObject target = this.gameObject.GetComponent<TargetFinder>().target;
            this.gameObject.GetComponent<NavMeshAgent>().isStopped = false;
            this.gameObject.GetComponent<NavMeshAgent>().SetDestination(target.transform.position);
        }
    }
}
