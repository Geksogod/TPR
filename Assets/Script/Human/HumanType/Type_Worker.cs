using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Type_Worker : MonoBehaviour
{
    public bool haveTask;
    public Task currentTask;
    private Human_Move move;

    private void Awake()
    {
        move = this.gameObject.GetComponent<Human_Move>() ?? this.gameObject.AddComponent<Human_Move>();
    }
    private void Start()
    {
        TaskManager.AddWorker(this);
    }

    public void SetTask(Task task)
    {
        currentTask = task;
        switch (currentTask.taskType)
        {
            case Task.TaskType.resourceGathering:
                Move_Position pos = new Move_Position(Vector3.zero,task.taskTarget);
                move.SetMovePosition(pos, task.taskTarget.GetComponent<Collider>().bounds.extents.x);
                haveTask = true;
                break;
        }
    }

    public void DoTask()
    {

    }
}
