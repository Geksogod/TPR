using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Animal : MonoBehaviour
{
    private float health;
    private float energy;
    private float timeToSleep;
    public float hunger;
    private bool alive = true;
    private Animator animator;

    public GameObject target;
    public float damage;
    public float speed;

    public string deadAnimation;
    private float Health
    {
        get { return health; }
        set
        {
            if (!alive)
                return;
            health = health + value;
            if (health <= 0)
            {
                Dead();
            }
        }
    }
    public float Energy
    {
        get { return energy; }
        set
        {
            if (!alive)
                return;
            this.energy = this.energy + value>=100?100: this.energy + value;
            if (energy <= 0)
            {
                this.energy = 0;
                Sleep();
            }
        }
    }
    public float Hunger
    {
        get { return hunger; }
        set
        {
            if (!alive)
                return;
            hunger = hunger + value;
            if (hunger <= 50&&hunger>0&&state != State.ReadyForEat && state != State.Eating)
            {
                state = State.ReadyForEat;
            }
            else if (hunger <= 0)
            {
                Dead();
            }
            else if (hunger>=60)
            {
                target = null;
                state = State.Stay;
            }
        }
    }
    public enum TypeOfFood
    {
        Meet,
        Grass
    }
    public TypeOfFood typeOfFood;
    public enum State
    {
        Stay,
        FindingNextPosition,
        FoundNextPosition,
        Walk,
        Sleep,
        WakeUp,
        ReadyForEat,
        SearchForEat,
        ReadyGoToEat,
        GoingToEat,
        Eating,
        Relax,
        Dead
    }
    public State state;

    private void Awake()
    {
        animator = this.gameObject.GetComponent<Animator>();
        Energy = 100;
        Hunger = 100;
        Health = 100;
    }

    private void Update()
    {
        //Debug.Log(animator.GetCurrentAnimatorClipInfo(0)[0].clip.);
        if (state == State.Dead) {
            if (this.gameObject.GetComponent<Animator>() != null && animator.GetCurrentAnimatorClipInfo(0)[0].clip.name == deadAnimation)
            {
                Destroy(this.gameObject.GetComponent<Animator>());
            }
            return;
        }
        if (state != State.Stay)
        {
            StopAllCoroutines();
        }
        if (state!= State.Sleep)
        {
            Energy = -0.005f;
        }
        switch (state)
        {
            case State.Sleep:
                Energy += 0.01f;
                if (energy >= timeToSleep)
                {
                    WakeUP();
                }
                break;
            case State.WakeUp:
                state = State.Stay;
                break;
            case State.Stay:
                StartCoroutine(Timer(Random.Range(10, 20)));
                ResetAnimator();
                break;
            case State.SearchForEat:
                if(target!=null)
                    state = Animal.State.ReadyGoToEat;
                break;
            case State.ReadyForEat:
                target = null;
                this.gameObject.GetComponent<TargetFinder>().FindTarget(typeOfFood);
                state = State.SearchForEat;
                break;
            case State.FoundNextPosition:
                this.gameObject.GetComponent<Movement>().GoTo(target);
                state = State.Walk;
                break;
            case State.ReadyGoToEat:
                this.gameObject.GetComponent<Movement>().GoTo(target);
                state = State.GoingToEat;
                break;
            case State.Walk:
                ResetAnimator();
                animator.SetBool("Walk", true);
                break;
            case State.GoingToEat:
                ResetAnimator();
                animator.SetBool("Walk",true);
                break;
            case State.Eating:
                ResetAnimator();
                animator.SetBool("Eat",true);
                break;
        }
    }

    private void ResetAnimator()
    {
        animator.SetBool("Walk",false);
        animator.SetBool("Eat",false);
        animator.SetBool("Run",false);
    }



    private void Dead()
    {
        alive = false;
        ResetAnimator();
        this.gameObject.GetComponent<Animator>().SetBool("Die", true);
        state = State.Dead;
        Destroy(this.gameObject.GetComponent<TargetFinder>());
        Destroy(this.gameObject.GetComponent<Movement>());
        Destroy(this.gameObject.GetComponent<NavMeshAgent>());
    }



    private void WakeUP()
    {
        state = State.WakeUp;
        Animation(state);
    }

    private void Sleep()
    {
        state = State.Sleep;
        timeToSleep = Random.Range(20, 80);
        Animation(state);
    }


    private void Animation(State state)
    {
        try
        {
            Debug.Log(state.ToString());
            animator.SetTrigger(state.ToString());
        }
        catch
        {
            Debug.LogWarning("Animator Doesn't have \" " + gameObject.name + state.ToString() + "\" triger");
        }
    }

    public bool TakeDamage(float damage_)
    {
        Health = damage_;
        return alive;
    }

    IEnumerator Timer(float time)
    {
        yield return new WaitForSeconds(time);
        state = State.FindingNextPosition;
        this.gameObject.GetComponent<TargetFinder>().FindNextPosition();
        StopAllCoroutines();
    }
    private void OnTriggerStay(Collider other)
    {
        if (state == State.Eating && other.gameObject == target)
        {
            if(other.gameObject.GetComponent<Plane>()!=null&& other.gameObject.GetComponent<Plane>().ready)
            {
                Hunger = 1f * Time.deltaTime;
                other.gameObject.GetComponent<Plane>().progress = other.gameObject.GetComponent<Plane>().progress - 1f * Time.deltaTime;
            }
        }
    }
}
