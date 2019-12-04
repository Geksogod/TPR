using cakeslice;
using System.Collections;
using UnityEngine;

public class MainResources : MonoBehaviour, ITouchListener
{
    public enum TypeResources
    {
        None,
        Wood
    }
    [Header("Resources Settings")]
    public TypeResources typeResources;
    public int balance;
    [HideInInspector]
    public int maxBalance;
    public float progress = 100;
    public bool chosen;
    [Header("Growning")]
    public bool Finished = false;
    private const float coeffcientGrowth = 12.4f;
    private float startScale;
    [SerializeField]
    private float grownTime = 3f;
    [SerializeField]
    private float grownProgress = 0f;

    public Resource itemResources;
    

    void Start()
    {
        this.gameObject.GetComponent<Outline>().enabled = false;
        itemResources = new Resource(typeResources.ToString(), typeResources);
//        Debug.Log(itemResources.typeResources);
        startScale = transform.localScale.magnitude;
        ChangeBalance(Random.Range(10, 20));
        maxBalance = balance;
        StartCoroutine(Growth(grownTime));
    }

    private void ChangeBalance(int val)
    {
        balance = balance + val;
        if (!Finished && startScale / 2 <= transform.localScale.magnitude)
            transform.localScale = Vector3.one * balance / coeffcientGrowth;
    }


    /// <summary>
    /// Give resources when progress <= 0 
    /// </summary>
    /// <returns>Resource</returns>
    public TypeResources GiveResource(float workStrange)
    {
        progress -= workStrange;
        if (progress <= 0)
        {
            progress = 100;
            if (balance <= 0)
            {
                Finished = true;
                StopAllCoroutines();
            }
            ChangeBalance(-1);
            return typeResources;
        }
        return TypeResources.None;
    }

    private IEnumerator Growth(float growthTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(growthTime);
            grownProgress += Time.deltaTime;
            if (grownProgress >= 100)
            {
                ChangeBalance(1);
                grownProgress = 0;
            }

        }
    }


    public void MouseDown()
    {
        if (EventManager.CurrentEvent == EventManager.Events.ChooseResources && !chosen)
        {
            chosen = TaskManager.AddTask("Get Resurces", Task.TaskType.resourceGathering, this.gameObject, "Get this Wood");         
        }
        else
        {
            ChangeBalance(-1);
            TaskManager.TaskProgresing(this.gameObject);
        }
    }

    public void MouseEnter()
    {
    }

    public void MouseExit()
    {
    }
}
