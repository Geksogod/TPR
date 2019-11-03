using UnityEngine;

public class MainResources : MonoBehaviour
{
    public enum TypeResources
    {
        Wood
    }
    [Header("Resources Settings")]
    public TypeResources typeResources;
    public int balance;
    public float progress;
    public bool chosen;
    [SerializeField, Header("Outline Settings")]
    private bool OutlineEnabled = true;
    private cakeslice.Outline outline;
    //private  outline;
    private void Awake()
    {
        outline = gameObject.GetComponent<cakeslice.Outline>();
    }

    void Start()
    {
        outline.enabled = false;
        balance = Random.Range(10, 20);
        transform.localScale = ((float)balance / 10) * transform.localScale;
        progress = 100;
    }

    /// <summary>
    /// Give resources when progress <= 0 
    /// </summary>
    /// <returns>Resource</returns>
    public TypeResources GiveResource()
    {
        progress = 100;
        return typeResources;
    }
    private void OnMouseEnter()
    {
        if (EventManager.CurrentEvent == EventManager.Events.ChooseResources)
        {
            if (OutlineEnabled && !chosen)
            {
                outline.enabled = true;
                outline.color = 1;
            }
        }
    }
    private void OnMouseDown()
    {
        if (EventManager.CurrentEvent == EventManager.Events.ChooseResources && !chosen)
        {
            chosen = TaskManager.AddTask("Get Resurces", Task.TaskType.resourceGathering, this.gameObject, "Get this Wood");
            if (chosen)
            {
                if (OutlineEnabled)
                {
                    outline.enabled = true;
                    outline.color = 0;
                }
            }
        }
    }
    private void OnMouseExit()
    {
        if (EventManager.CurrentEvent == EventManager.Events.ChooseResources)
        {
            if (OutlineEnabled && !chosen)
                outline.enabled = false;
        }
    }
}
