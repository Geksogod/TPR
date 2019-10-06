using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCorutino : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.F))
            StartCoroutine(Timer());
    }
    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(1f);
        Debug.Log("HEllo");
    }
}
