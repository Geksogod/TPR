using UnityEngine;

public class Animal : MonoBehaviour
{
    private float health;
    private float energy;
    private float timeToSleep;
    public float hunger;
    private bool alive;

    public GameObject target;
    public float maxHealht;
    public float damage;
    public float speed;

    private float Health
    {
        get { return health; }
        set
        {
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
            hunger = hunger + value;
            if (hunger <= 50&&hunger>0)
            {
                state = State.ReadyForEat;
            }
            else if (hunger <= 0)
            {
                Dead();
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
        Sleep,
        WakeUp,
        ReadyForEat,
        SearchForEat,
        ReadyGoToEat,
        GoingToEat,
        Eating,
        Relax
    }
    public State state;

    private Animator animator;
    private void Awake()
    {
        this.animator = this.gameObject.GetComponent<Animator>();
        Energy = 100;
        Hunger = 100;
        Health = maxHealht;
    }

    private void Update()
    {
        if(state!= State.Sleep)
        {
            Energy = -0.005f;
        }
        switch (state)
        {
            case State.Sleep:
                Energy = 0.01f;
                if (energy >= timeToSleep)
                {
                    WakeUP();
                }
                break;
            case State.WakeUp:
                state = State.Stay;
                break;
            case State.ReadyForEat:
                this.gameObject.GetComponent<TargetFinder>().FindTarget(typeOfFood);
                state = State.SearchForEat;
                break;
            case State.ReadyGoToEat:
                this.gameObject.GetComponent<Movement>().GoTo(target);
                state = State.GoingToEat;
                break;
        }
    }





    private void Dead()
    {
        alive = false;
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
}
