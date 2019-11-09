using cakeslice;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public static class TaskManager
{
    [SerializeField]
    private static GameObject taskPanel;
    private static int tasksCount = 0;
    [SerializeField]
    private static List<Task> taskList = new List<Task>();
    private static int taskIndex;

    private static bool ConfirmTask(Task task)
    {
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

    private static void RedoText()
    {
        if (taskPanel == null)
            taskPanel = GameObject.FindGameObjectWithTag("Task Board");
        taskPanel.GetComponentInChildren<TextMeshProUGUI>().text = string.Empty;
        taskPanel.GetComponentInChildren<TextMeshProUGUI>().text = "Task Board";
        for (int i = 0; i < taskList.Count; i++)
        {
            taskPanel.GetComponentInChildren<TextMeshProUGUI>().text += "\n*" + taskList[i].taskDescription + " - " + taskList[i].taskProgress + '%';
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

    public Task(string TaskName, TaskType _taskType, GameObject TaskTarget, string TaskDescription,int TaskIndex, float TaskProgress = 0, bool IsCompleted = false)
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
