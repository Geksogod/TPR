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
    private HumanType humanType;
    private float offset;
    public bool arrived = false;

    private void Awake()
    {
        navMesh = this.gameObject.GetComponent<NavMeshAgent>() ?? gameObject.AddComponent<NavMeshAgent>();
        human = this.gameObject.GetComponent<Human>();
        humanType = this.gameObject.GetComponent<HumanType>();
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
                arrived = false;
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
        arrived = true;
        human.SetAnimationTrigger("Idle");
        humanType.doTask = arrived;
        StopCoroutine(DesiredVelocity());
    }
}
