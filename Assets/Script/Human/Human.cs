using UnityEngine;

public class Human : MonoBehaviour
{
    private int health;
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
    public GameObject head;
    public GameObject body;
    public GameObject leftHand;
    public GameObject rightHand;


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


    private void Dead()
    {

    }

    public void Customization()
    {
        СustomizationSystem cusmomizator = GameObject.Find("СustomizationSystem").GetComponent<СustomizationSystem>();
        if (leftHand == null)
            cusmomizator.Customization(ref head, ref body, ref rightHand, humanType);
        else if (rightHand == null)
            cusmomizator.Customization(ref head, ref body, ref leftHand, humanType);
        else
            cusmomizator.Customization(ref head, ref body, ref leftHand, ref rightHand, humanType);
    }
}


