using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_Position 
{
    public Vector3 position;
    public GameObject targetPosition;
    public Move_Position(Vector3 Position, GameObject TargetPosition = null )
    {
        this.position = Position;
        this.targetPosition = TargetPosition;
    }
}
