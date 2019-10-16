using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour
{
    public float progress;
    public bool ready;
    public Mesh notReadyMesh;
    public Mesh ReadyMesh;
    private MeshFilter MeshFilter;

    /*in futher can be change on account of same factors*/
    private float progressCoefficient  = 4;
    // Start is called before the first frame update
    void Start()
    {
        MeshFilter = this.gameObject.GetComponent<MeshFilter>();
        if (progress > 100)
            progress = 100;
        else if (progress < 0)
            progress = 0;
        if (progress < 100)
        {
            MeshFilter.sharedMesh = notReadyMesh;
            ready = false;
        }
        if (progress == 100)
        {
            MeshFilter.sharedMesh = ReadyMesh;
            ready = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (progress > 100)
            progress = 100;
        else if (progress < 0)
            progress = 0;
        if (progress == 100&&!ready)
        {
            ready = true;
            MeshFilter.mesh = ReadyMesh;
        }
        else if(progress < 20&&ready)
        {
            ready = false;
            MeshFilter.mesh = notReadyMesh;
        }
        if (!ready)
            Progresing();
    }

    private void Progresing()
    {
        progress = progress + (progressCoefficient * Time.deltaTime);
        this.transform.localScale = new Vector3(progress/100, progress/100, progress/100);
    }
}
