using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HumanType : MonoBehaviour
{
    public bool doTask;
    private void Update()
    {
        if (doTask)
        {
            //StartDoTask();
            //doTask = false;
        }
    }
    protected abstract void DoTask();
    protected abstract void StartDoTask();
}
