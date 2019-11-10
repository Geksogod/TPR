using Unity.Collections;
using UnityEngine;

public class Human : MonoBehaviour
{
    [Header("Human Settings"),SerializeField]
    private int health;
    [Range(1,100)]
    public int speed;
    public int Health
    {
        get { return health; }
        set
        {
            if (value > 100)
                health = 100;
            else
            {
                health += value;
            }
            if (health <= 0)
                Dead();
        }
    }   

    public enum HumanType
    {
        Worker
    }
    public HumanType humanType;
    [Header("Human customization")]
    public GameObject head;
    public GameObject body;
    public GameObject leftHand;
    public GameObject rightHand;
    private Animator animator;

    private void Awake()
    {
        animator = this.gameObject.GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        Customization();
        health = 100;
    }

    // Update is called once per frame
    void Update()
    {

    }

    

    public void Dead()
    {
        int deadTriger = Random.Range(0, 2);
        speed = 0;
        SetAnimationTrigger("Dead_" + deadTriger.ToString());
    }

    public void Customization()
    {
        СustomizationSystem cusmomizator = GameObject.Find("СustomizationSystem").GetComponent<СustomizationSystem>();
        cusmomizator.Customization(ref head, ref body, ref leftHand, ref rightHand, humanType);
    }

    /// <summary>
    /// Reset all trigers && Set value this triger animation
    /// </summary>
    /// <param name="trigger">trigger name</param>
    public void SetAnimationTrigger(string trigger)
    {
        if (animator != null)
        {
            foreach (AnimatorControllerParameter p in animator.parameters)
                if (p.type == AnimatorControllerParameterType.Trigger)
                    animator.ResetTrigger(p.name);
            animator.SetTrigger(trigger);
        }
    }
}


