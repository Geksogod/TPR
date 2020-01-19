using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Type_Worker : HumanType
{

    private bool isTaskDoing;
    public bool haveTask;
    [SerializeField]
    private bool taskIsPause;
    public Task currentTask;
    private Human_Move move;
    private bool isCoverageArea;
    private Human human;
    [SerializeField]
    private float workStrange;
    private void Awake()
    {
        move = this.gameObject.GetComponent<Human_Move>() ?? this.gameObject.AddComponent<Human_Move>();
        human = this.gameObject.GetComponent<Human>() ?? this.gameObject.AddComponent<Human>();
    }
    private void Start()
    {
        TaskManager.AddWorker(this);
    }

    private void Update()
    {
        if (human.IsDead())
        {
            TaskManager.RemoveWorker(this);
        }
        if (haveTask && !taskIsPause && !isTaskDoing && currentTask.taskType == Task.TaskType.resourceGathering && move.arrived)
        {
            StartDoTask();
        }

        if (currentTask.taskType == Task.TaskType.resourceGathering&&human.inventory.isFull())
        {
            SetTaskPause(true);
            //TODO find warehouse
        }
        else if (currentTask.taskType == Task.TaskType.resourceGathering&&!human.inventory.isFull())
        {
            SetTaskPause(false);
        }

    }

    public void SetTask(Task task)
    {
        isTaskDoing = false;
        currentTask = task;
        switch (currentTask.taskType)
        {
            case Task.TaskType.resourceGathering:
                Move_Position pos = new Move_Position(Vector3.zero, task.taskTarget);
                move.SetMovePosition(pos, task.taskTarget.GetComponent<Collider>().bounds.extents.x);
                haveTask = true;
                break;
        }
    }



    protected override void DoTask()
    {
    }
    protected override void StartDoTask()
    {
        isTaskDoing = true;
        human.SetAnimationTrigger("Work");
        StartCoroutine(DoingTask(3));
    }

    private IEnumerator DoingTask(float time)
    {

        while (true)
        {
            yield return new WaitForSeconds(time);
            if (!taskIsPause)
            {
                Item resource = currentTask.taskTarget.GetComponent<ResourceContainer>().GiveResource(workStrange);
                if (resource != null)
                {
                    human.inventory.AddItem(resource);
                    Debug.Log("Inventory add Wood");
                }
            }
            else if(taskIsPause){
                human.SetAnimationTrigger("Idle");
            }

        }
    }
    public void SetTaskPause(bool isPause)
    {
        if (currentTask != null)
        {
            taskIsPause = isPause;
        }
    }
}
