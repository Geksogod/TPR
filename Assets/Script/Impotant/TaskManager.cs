using cakeslice;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public static class TaskManager
{
    private static GameObject taskPanel;
    private static int tasksCount = 0;
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
        Type_Worker freeWorker = FindFreeWorker();
        if (freeWorker != null)
        {
            freeWorker.SetTask(task);
        }
        tasks.Add(task, freeWorker);
        taskList.Add(task);
        Debug.Log("Task - " + task.taskName + " Confirm");
        RedoText();
        tasksCount = taskList.Count;

        return true;
    }
    public static bool AddTask(string TaskName, Task.TaskType _taskType, GameObject TaskTarget, string TaskDescription, float TaskProgress = 0, bool IsCompleted = false)
    {
        Task newTask = new Task(TaskName, _taskType, TaskTarget, TaskDescription, taskIndex++, TaskProgress, IsCompleted);
        return ConfirmTask(newTask);
    }
    public static void CanselTask(GameObject taskTarget)
    {
        //to DO
        taskList.Remove(taskList.Where(a => a.taskTarget == taskTarget).Single());
        RedoText();
    }

    private static void RedoText()
    {
        if (taskPanel == null)
            taskPanel = GameObject.FindGameObjectWithTag("Task Board");
        taskPanel.GetComponentInChildren<TextMeshProUGUI>().text = string.Empty;
        taskPanel.GetComponentInChildren<TextMeshProUGUI>().text = "Task Board";
        for (int i = 0; i < tasks.Count; i++)
        {
            if (tasks[taskList[i]]==null)
                taskPanel.GetComponentInChildren<TextMeshProUGUI>().text += "\n*" + taskList[i].taskDescription + " - " + taskList[i].taskProgress + '%';
            else
                taskPanel.GetComponentInChildren<TextMeshProUGUI>().text += "<color=green>" + "\n*" + taskList[i].taskDescription + "<color=black>" + " - " + taskList[i].taskProgress + '%';
        }
    }

    public static void TaskProgresing(GameObject taskGameObject)
    {
        Task currentTask = taskList.Where(target => target.taskTarget == taskGameObject).Single();
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
        {
            currentTask.isCompleted = true;
            taskList.RemoveAt(currentTask.taskIndex);
            taskGameObject.GetComponent<Outline>().enabled = false;
        }
        RedoText();
    }
    private static Type_Worker FindFreeWorker()
    {
        for (int i = 0; i < workers.Count; i++)
        {
            Type_Worker worker = workers[i];
            if (!worker.haveTask)
            {
                //workers[i].SetTask(task);
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
