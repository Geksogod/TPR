using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public static class TaskManager
{
    [SerializeField]
    private static GameObject taskPanel;
    private static int tasksCount = 0;
    [SerializeField]
    private static List<Task> taskList = new List<Task>();

    private static bool ConfirmTask(Task task)
    {
        taskList.Add(task);
        Debug.Log("Task - " + task.taskName + " Confirm");
        tasksCount = taskList.Count;
        if (taskPanel == null)
            taskPanel = GameObject.Find("Tasks List");
        taskPanel.GetComponentInChildren<TextMeshProUGUI>().text += "\n*"+task.taskDescription;
        return true;
    }
    public static bool AddTask(string TaskName, Task.TaskType _taskType, GameObject TaskTarget, string TaskDescription, bool IsCompleted = false)
    {
        Task newTask = new Task(TaskName, _taskType, TaskTarget, TaskDescription, IsCompleted);
        return ConfirmTask(newTask);
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
    public bool isCompleted;

    public Task(string TaskName, TaskType _taskType, GameObject TaskTarget, string TaskDescription, bool IsCompleted = false)
    {
        taskName = TaskName;
        taskType = _taskType;
        taskTarget = TaskTarget;
        taskDescription = TaskDescription;
        isCompleted = IsCompleted;
    }
}
