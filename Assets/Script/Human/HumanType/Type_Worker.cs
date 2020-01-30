using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Type_Worker : HumanType
{

    private Coroutine taskProcesing;
    private bool isTaskDoing;
    public bool haveTask;
    [SerializeField]
    private bool taskIsPause;
    public Task currentTask;
    private Human_Move move;
    private Human human;
    [SerializeField]
    private float workStrange;
    [SerializeField]
    private Building storage;
    [SerializeField]
    private bool isGoToStorage;
    private TaskManager taskManager;
    private void Awake()
    {
        move = this.gameObject.GetComponent<Human_Move>() ?? this.gameObject.AddComponent<Human_Move>();
        human = this.gameObject.GetComponent<Human>() ?? this.gameObject.AddComponent<Human>();
    }
    private void Start()
    {
        TaskManager taskManager = GameObject.FindObjectOfType<TaskManager>();
        taskManager.AddWorker(this);
    }

    private void Update()
    {
        if (human.IsDead())
        {
            if(taskManager!=null)
                taskManager.RemoveWorker(this);
                //Destroy(this);
                //Destroy(human);
               // Destroy(move);
        }
        if (haveTask && !taskIsPause && !isTaskDoing && currentTask.taskType == Task.TaskType.resourceGathering && move.arrived)
        {
            StartDoTask();
        }

        if (currentTask.taskType == Task.TaskType.resourceGathering && human.inventory.isFull())
        {
            if (!taskIsPause)
            {
                SetTaskPause(true);
                storage = HumanHelper.FindNearBuilding(Building.BuildingType.Storage, transform.position);
                Move_Position pos = new Move_Position(storage.gameObject);
                move.SetMovePosition(pos, storage.gameObject.GetComponent<Collider>().bounds.extents.x * 2);
                isGoToStorage= true;
                StopCoroutine(taskProcesing);
            }
            else
            {
                if (move.arrived&&isGoToStorage)
                {
                    GetComponent<InventorySystem>().GetAllResorces(storage.GetComponent<InventorySystem>());
                    isGoToStorage = false;
                }
            }
        }
        else if (currentTask!=null&&taskIsPause&&currentTask.taskType == Task.TaskType.resourceGathering && !isGoToStorage)
        {
            SetTaskPause(false);
            SetTask(currentTask);
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
        taskProcesing = StartCoroutine(DoingTask(3));
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
                    if(taskManager!=null){
                        taskManager.TaskProgresing(this);
                    }
                    Debug.Log("Inventory add Wood");
                }
            }
            else
            {
                human.SetAnimationTrigger("Idle");
                //StopCoroutine(DoingTask(3));
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
