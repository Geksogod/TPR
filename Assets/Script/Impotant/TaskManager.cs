using cakeslice;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public static class TaskManager
{
    private static GameObject taskPanel;
    private static List<Task> taskList = new List<Task>();
    private static int taskIndex;
    private static List<Type_Worker> workers = new List<Type_Worker>();
    private static Dictionary<Task, Type_Worker> tasks = new Dictionary<Task, Type_Worker>();


    public static void AddWorker(Type_Worker worker)
    {
        workers.Add(worker);
    }
    private static bool ConfirmTask(Task task)
    {
        tasks.Add(task, null);
        Type_Worker freeWorker = FindFreeWorker();
        if (freeWorker!=null)
        TakeTask(freeWorker);
        RedoText();
        return true;
    }
    public static bool AddTask(string TaskName, Task.TaskType _taskType, GameObject TaskTarget, string TaskDescription, float TaskProgress = 0, bool IsCompleted = false)
    {
        Task newTask = new Task(TaskName, _taskType, TaskTarget, TaskDescription, taskIndex++, TaskProgress, IsCompleted);
        return ConfirmTask(newTask);
    }

    public static void CanselTask(GameObject taskTarget)
    {
        tasks.Remove(tasks.Keys.Where(target => target.taskTarget == taskTarget).Single());
        RedoText();
    }

    private static void RedoText()
    {
        if (taskPanel == null)
            taskPanel = GameObject.FindGameObjectWithTag("Task Board");
        taskPanel.GetComponentInChildren<TextMeshProUGUI>().text = string.Empty;
        taskPanel.GetComponentInChildren<TextMeshProUGUI>().text = "Task Board";
        foreach (var key in tasks.Keys)
        {
            if (tasks[key] == null)
                taskPanel.GetComponentInChildren<TextMeshProUGUI>().text += "\n*" + key.taskDescription + " - " + key.taskProgress + '%';
            else
                taskPanel.GetComponentInChildren<TextMeshProUGUI>().text += "<color=green>" + "\n*" + key.taskDescription + "<color=black>" + " - " + key.taskProgress + '%';
        }
    }

    public static void TaskProgresing(GameObject taskGameObject)
    {
        Task currentTask = tasks.Keys.Where(target => target.taskTarget == taskGameObject).Single();
        switch (currentTask.taskType)
        {
            case Task.TaskType.resourceGathering:
                MainResources mainResources = taskGameObject.GetComponent<MainResources>();
                if (mainResources.balance > 0)
                    currentTask.taskProgress = (100 / mainResources.maxBalance) * (mainResources.maxBalance - mainResources.balance);
                else
                    currentTask.taskProgress = 100;
                break;
            default:
                break;
        }
        if (currentTask.taskProgress >= 100)
            FinishTak(currentTask, taskGameObject);
        RedoText();
    }

    private static void FinishTak(Task currentTask,GameObject taskGameObject)
    {
        currentTask.isCompleted = true;
        Type_Worker worker = tasks[currentTask];
        worker.haveTask = false;
        tasks.Remove(currentTask);
        TakeTask(worker);
        taskGameObject.GetComponent<Outline>().enabled = false;
    }

    private static void TakeTask(Type_Worker worker)
    {
        if (tasks.Keys.Count > 0)
            foreach (var key in tasks.Keys.ToList())
            {
                if (tasks[key] == null)
                {
                    tasks[key] = worker;
                    worker.SetTask(key);
                }
            }
    }

    private static Type_Worker FindFreeWorker()
    {
        for (int i = 0; i < workers.Count; i++)
        {
            Type_Worker worker = workers[i];
            if (!worker.haveTask)
            {
                return workers[i];
            }
        }
        return null;
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
