using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public static class Mathem
{
    public static float FindYOnPlane(float x, float z,GameObject plane)
    {
        RaycastHit hit;
        Vector3 pos = new Vector3(x,1000,z);
        Ray ray = new Ray(pos, Vector3.down);
        if (plane.GetComponent<Collider>().Raycast(ray, out hit, 2.0f * 1000f))
        {
            pos.y = hit.point.y;
            Debug.Log("Hit point: " + hit.point);
        }
        return pos.y;
        
    }


}
