using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Good : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AllGoToSleep()
    {
        for (int i = 0; i < GameObject.FindObjectsOfType<Animal>().Length; i++)
        {
            GameObject.FindObjectsOfType<Animal>()[i].Energy = -100;
        }
    }
}
