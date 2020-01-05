using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human_Manager : MonoBehaviour
{
    public GameObject Worker;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AddWorker(){
        Vector3 workerPosition = new Vector3(Worker.transform.position.x+Random.Range(-5f,5f),Worker.transform.position.y,Worker.transform.position.z+Random.Range(-3f,3f));
        GameObject.Instantiate(Worker,workerPosition,Worker.transform.rotation);
    }
}
