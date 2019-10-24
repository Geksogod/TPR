using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainResources : MonoBehaviour
{
    public enum TypeResources
    {
        Wood
    }
    public TypeResources typeResources;
    public int balance;
    public float progress;

    // Start is called before the first frame update
    void Start()
    {
        balance = Random.Range(10, 20);
        progress = 100;
    }

    /// <summary>
    /// Give resources when progress == 0 
    /// </summary>
    /// <returns>Resource</returns>
    public TypeResources GiveResource()
    {
        return typeResources;
    }
}
