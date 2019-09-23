using UnityEngine;

public class Animal : MonoBehaviour
{
    public float health;
    public float energy;
    public float Energy
    {
        get { return energy; }
        set
        {
            if (!initialization)
            {
                this.energy = value;
                return;
            }
            this.energy = this.energy - value;
            if (energy <= 0)
            {
                Sleep(ref state);
            }
        }
    }
    public float speed;
    private bool initialization;
    public enum State
    {
        Stay,
        Sleep,
        Relax
    }
    public State state;

    private Animator animator;
    private void Awake()
    {
        this.animator = this.gameObject.GetComponent<Animator>();
        Energy = 100;
        initialization = true;
    }
    private void Sleep(ref State state)
    {
        state = State.Sleep;
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


}
