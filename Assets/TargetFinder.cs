using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFinder : MonoBehaviour
{
    public GameObject target;

    public void FindTarget(/*Animal.TypeOfFood typeOfFood*/)
    {
        Animal.TypeOfFood typeOfFood = Animal.TypeOfFood.Grass; //Test
        float distance = float.MaxValue;
        switch (typeOfFood)
        {
            case Animal.TypeOfFood.Grass:
                target = GameObject.FindGameObjectWithTag("EdibleFlora");
                for (int i = 0; i < GameObject.FindGameObjectsWithTag("EdibleFlora").Length; i++)
                {
                    GameObject preTarget = GameObject.FindGameObjectsWithTag("EdibleFlora")[i];
                    float newDistanceF = Vector3.Distance(this.gameObject.transform.position , preTarget.transform.position);
                    if (newDistanceF < distance)
                    {
                        distance = newDistanceF;
                        target = preTarget;
                    }
                }
                break;
        }
        //return this.gameObject;
        Debug.Log(target.name);//Test
    }
}
