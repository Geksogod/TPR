using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanEventSystem : MonoBehaviour
{

    public interface IListener
    {
        void OnEvent(string val, int data);

    }

}
