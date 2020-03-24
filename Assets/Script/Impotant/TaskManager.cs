using cakeslice;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class TaskManager : MonoBehaviour
{
    [SerializeField]
    private List<Type_Worker> workers = new List<Type_Worker>();
    public Dictionary<Type_Worker, Task> workerTask = new Dictionary<Type_Worker, Task>();

    private Queue<Task> taskInQueue = new Queue<Task>();
    private bool isReadyToTakeTask = false;
    private MouseMonitor mouseMonitor;
    private int taskIndex = 0;
    private OutlineListener outlineListener;

    public void AddWorker(Type_Worker worker)
    {
        workers.Add(worker);
    }
    private void Start()
    {
        outlineListener = GameObject.FindObjectOfType<OutlineListener>();
    }
    public void RemoveWorker(Type_Worker worker)
    {
        workers.Remove(worker);
        foreach (var item in workerTask)
        {
            if (item.Key == worker)
            {
                ChangeWorker(worker);
                return;
            }
        }
    }

    private bool isHasFreeTask()
    {
        return taskInQueue.Count > 0;
    }
    private void Update()
    {
        if (EventManager.CurrentEvent == EventManager.Events.ChooseResources)
        {
            isReadyToTakeTask = true;
        }
        else
        {
            isReadyToTakeTask = false;
        }
        if (isReadyToTakeTask)
        {
            if (mouseMonitor == null)
            {
                mouseMonitor = GameObject.FindObjectOfType<MouseMonitor>().GetComponent<MouseMonitor>();
            }
            if (mouseMonitor.currentSelectedGameObject != null && !isTaskAlreadyCreate(mouseMonitor.currentSelectedGameObject))
            {
                if (mouseMonitor.currentGameObject.GetComponent<ResourceContainer>() != null && mouseMonitor.currentGameObject.GetComponent<ResourceContainer>().HasResources())
                    PrepareToTakeTask(mouseMonitor.currentGameObject);
            }
        }
        if (isHasFreeTask() && FindFreeWorker() != null)
            SetWorkerTaskInQueue(FindFreeWorker());
    }
    private void PrepareToTakeTask(GameObject taskTarget)
    {
        Task newTask = new Task("Get " + taskTarget.name, Task.TaskType.resourceGathering, taskTarget, "", taskIndex++, 0, false);
        if (outlineListener == null)
            outlineListener = GameObject.FindObjectOfType<OutlineListener>();
        if (outlineListener != null && outlineListener.CanOutline(taskTarget))
        {
            outlineListener.AddToOutlines(taskTarget);
        }
        AddTask(newTask);
    }

    private void SetWorkerTaskInQueue(Type_Worker freeWorker)
    {
        if (!isHasFreeTask())
            return;
        Task newTask = taskInQueue.Dequeue();
        freeWorker.haveTask = true;
        workerTask.Add(freeWorker, newTask);
        freeWorker.SetTask(newTask);
    }

    private void AddTask(Task newTask)
    {
        Type_Worker freeWorker = FindFreeWorker();
        if (freeWorker != null)
        {
            freeWorker.haveTask = true;
            workerTask.Add(freeWorker, newTask);
            freeWorker.SetTask(newTask);
        }
        else
        {
            taskInQueue.Enqueue(newTask);
        }
    }

    private void ChangeWorker(Type_Worker currentWorker)
    {
        Task task = null;
        foreach (var item in workerTask)
        {
            if (item.Key == currentWorker)
            {
                task = item.Value;
            }
        }
        workerTask.Remove(currentWorker);
        AddTask(task);
    }

    private Type_Worker FindFreeWorker()
    {
        for (int i = 0; i < workers.Count; i++)
        {
            if (workers[i].haveTask == false && workers[i].GetState() == Type_Worker.State.ReadyToTakeTask)
            {
                return workers[i];
            }
        }
        return null;
    }
    private bool isTaskAlreadyCreate(GameObject taskTarget)
    {
        foreach (var item in workerTask)
        {
            if (item.Value.taskTarget == taskTarget)
                return true;
        }
        return false;
    }
    public void TaskProgresing(Type_Worker worker)
    {
        if (workerTask.Count > 0)
            foreach (var item in workerTask)
            {
                switch (item.Value.taskType)
                {
                    case Task.TaskType.resourceGathering:
                        if (item.Key == worker)
                        {
                            ResourceContainer mainResources = item.Value.taskTarget.GetComponent<ResourceContainer>();
                            if (mainResources != null)
                            {
                                if (mainResources.balance > 0)
                                    item.Value.taskProgress = (100 / mainResources.maxBalance) * (mainResources.maxBalance - mainResources.balance);
                                else
                                {
                                    item.Value.taskProgress = 100;
                                    FinishTask(item.Value);
                                }
                            }
                        }
                        break;
                }
            }
    }
    private void FinishTask(Task task)
    {
        Type_Worker worker = null;
        foreach (var item in workerTask)
        {
            if (item.Value == task)
            {
                item.Value.isCompleted = true;
                worker = item.Key;
                outlineListener.RemoveFromOutline(item.Value.taskTarget);
                item.Key.RemoveTask();
            }
        }
        if (worker != null)
            workerTask.Remove(worker);
        else
            Debug.LogError("Can't finish task");
    }

}


[System.Serializable]
public class Task
{
    public string taskName = String.Empty;
    public enum TaskType
    {
        resourceGathering
    }
    public TaskType taskType;
    public GameObject taskTarget;
    public string taskDescription = String.Empty;
    public float taskProgress;
    public bool isCompleted;
    public int taskIndex;

    public Task(string TaskName, TaskType _taskType, GameObject TaskTarget, string TaskDescription, int TaskIndex, float TaskProgress = 0, bool IsCompleted = false)
    {
        taskName = TaskName;
        taskType = _taskType;
        taskTarget = TaskTarget;
        taskDescription = TaskDescription;
        taskIndex = TaskIndex;
        taskProgress = TaskProgress;
        isCompleted = IsCompleted;
    }
}

