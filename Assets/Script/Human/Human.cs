using Unity.Collections;
using UnityEngine;
using System.Collections.Generic;

public class Human : MonoBehaviour
{
    [Header("Human Settings"), SerializeField]
    private int health;
    [Range(1, 100)]
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
    private Collider coverage;

    private bool isDead = false;
    private const float animTimer = 1f;
    private float myAnimTimer = 1f;

    [SerializeField]
    public List<Item> inventory = new List<Item>();

    private void Awake()
    {
        coverage = this.gameObject.GetComponent<Collider>();
        animator = this.gameObject.GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        Customization();
        health = 100;
        myAnimTimer = animTimer;
    }


    public void Dead()
    {
        if (health > 0)
            health = 0;
        int deadTriger = Random.Range(0, 2);
        speed = 0;
        isDead = true;
        SetAnimationTrigger("Dead_" + deadTriger.ToString());
    }

    public void Customization()
    {
        СustomizationSystem cusmomizator = GameObject.Find("СustomizationSystem").GetComponent<СustomizationSystem>();
        head.GetComponent<MeshFilter>().mesh = cusmomizator.GetHead(humanType);
        Сlothes bodyClothes = cusmomizator.GetBody(humanType);
        inventory.Add(bodyClothes);
        body.GetComponent<SkinnedMeshRenderer>().sharedMesh = bodyClothes.GetMesh();
        rightHand = rightHand != null ? Instantiate(cusmomizator.GetRightHand(humanType), rightHand.transform) : null;
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
            animator.SetTrigger("Idle");
            while (myAnimTimer <= 0)
            {
                myAnimTimer -= Time.deltaTime;
            }
            myAnimTimer = animTimer;
            animator.SetTrigger(trigger);
        }
    }

    public bool IsDead()
    {
        return isDead;
    }
}





