using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Human_Move : MonoBehaviour
{
    public bool stop = true;
    public Vector3 nextPosition = Vector3.zero;
    private NavMeshAgent navMesh;
    private Human human;
    private float offset;

    private void Awake()
    {
        navMesh = this.gameObject.GetComponent<NavMeshAgent>() ?? gameObject.AddComponent<NavMeshAgent>();
        human = this.gameObject.GetComponent<Human>();
    }

    public void SetMovePosition(Move_Position position,float Offset)
    {
        if (stop) 
        {
            if (position.targetPosition == null)
                nextPosition = position.position;
            else
                nextPosition = position.targetPosition.transform.position;
            Debug.Log(nextPosition);
            if (!nextPosition.Equals(Vector3.zero))
            {
                human.SetAnimationTrigger("Walk");
                navMesh.SetDestination(nextPosition);
                navMesh.isStopped = false;
                stop = false;
                offset = Offset;
                StartCoroutine(DesiredVelocity());
            }
        }
    }

    private IEnumerator DesiredVelocity()
    {
        if(!stop)
        while (true)
        {
            yield return new WaitForFixedUpdate();
                if (navMesh.remainingDistance <= offset)
                    break;
        }
        stop = true;
        navMesh.isStopped = true;
        human.SetAnimationTrigger("Idle");
        StopCoroutine(DesiredVelocity());
    }
}
