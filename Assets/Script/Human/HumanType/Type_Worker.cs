using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Type_Worker : HumanType
{

    public enum State
    {
        GoToTask,
        DoTask,
        ReadyToTakeTask,
        GoToStorage
    }
    [SerializeField]
    private State state;
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
        Time.timeScale = 4;
        move = this.gameObject.GetComponent<Human_Move>() ?? this.gameObject.AddComponent<Human_Move>();
        human = this.gameObject.GetComponent<Human>() ?? this.gameObject.AddComponent<Human>();
    }
    private void Start()
    {
        taskManager = GameObject.FindObjectOfType<TaskManager>();
        if (taskManager != null)
        {
            taskManager.AddWorker(this);
            state = State.ReadyToTakeTask;
        }
    }

    private void Update()
    {
        if (human.IsDead())
        {
            if (taskManager != null)
                taskManager.RemoveWorker(this);
        }
        if (haveTask && !taskIsPause && !isTaskDoing && currentTask.taskType == Task.TaskType.resourceGathering && move.arrived)
        {
            StartDoTask();
        }
        if (human.inventory.isFull())
            isGoToStorage = true;
        if (isGoToStorage && state != State.GoToStorage)
        {
            if (haveTask && !taskIsPause)
            {
                SetTaskPause(true);
            }
            GoToStorage();
        }
        else if (state == State.GoToStorage && move.arrived)
        {
            GetComponent<InventorySystem>().GetAllResorces(storage.GetComponent<InventorySystem>());
            if (haveTask)
            {
                SetTaskPause(false);
            }
            else
            {
                if (state != State.ReadyToTakeTask)
                    state = State.ReadyToTakeTask;
            }
            isGoToStorage = false;
        }

    }

    public void SetTask(Task task)
    {
        if (state != State.ReadyToTakeTask)
        {
            Debug.LogError("Worker can't take task");
            return;
        }
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
        state = State.GoToTask;
    }
    public void RemoveTask()
    {
        StopCoroutine(taskProcesing);
        isTaskDoing = false;
        currentTask = null;
        haveTask = false;
        if (human.inventory.HaveResources())
        {
            isGoToStorage = true;
        }
    }

    private void GoToStorage()
    {
        storage = HumanHelper.FindNearBuilding(Building.BuildingType.Storage, transform.position);
        Move_Position pos = new Move_Position(storage.gameObject);
        move.SetMovePosition(pos, storage.gameObject.GetComponent<Collider>().bounds.extents.x * 2);
        state = State.GoToStorage;
    }

    protected override void DoTask()
    {
    }
    protected override void StartDoTask()
    {
        isTaskDoing = true;
        human.SetAnimationTrigger("Work");
        taskProcesing = StartCoroutine(DoingTask(3));
        state = State.DoTask;
    }

    private IEnumerator DoingTask(float time)
    {

        while (true)
        {
            yield return new WaitForSeconds(time);
            if (currentTask == null)
                break;
            if (!taskIsPause)
            {
                Item resource = currentTask.taskTarget.GetComponent<ResourceContainer>().GiveResource(workStrange);
                if (resource != null)
                {
                    human.inventory.AddItem(resource);
                    if (taskManager != null && currentTask != null && haveTask)
                    {
                        try
                        {
                            taskManager.TaskProgresing(this);
                        }

                        catch{

                        }
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
        taskIsPause = isPause;
        if (isPause)
        {
            StopCoroutine(taskProcesing);
        }
        else
        {
            state = State.ReadyToTakeTask;
            SetTask(currentTask);
        }

    }
}
