using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Type_Worker : HumanType
{

    private bool isTaskDoing;
    public bool haveTask;
    public Task currentTask;
    private Human_Move move;
    private bool isCoverageArea;
    private Human human;
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
        if (haveTask && !isTaskDoing && currentTask.taskType == Task.TaskType.resourceGathering && move.arrived)
        {
            StartDoTask();
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
            Resource resource = currentTask.taskTarget.GetComponent<MainResources>().GiveResource(20);
            if (resource != null){
                human.inventory.Add(resource);
                Debug.Log("Inventory add Wood");
            }

        }
    }
}
