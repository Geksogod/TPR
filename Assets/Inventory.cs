using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Human))]
public class Inventory : MonoBehaviour
{
    public Сlothes clothedCloth;
    
    public Item rightHandItem;
    public Item leftHandItem;
    private Human human;
    private СustomizationSystem customizeSystem;
    private void Awake() {
        human = GetComponent<Human>();
    }

    private void Start() {
        customizeSystem = GameObject.Find("СustomizationSystem").GetComponent<СustomizationSystem>();
        ClotheClothes(customizeSystem.GetBody(human.humanType));
        //SetItemToLeftHand(customizeSystem.GetLeftHand(human.humanType));
    }
    public void ClotheClothes(Сlothes clothes){
        clothedCloth  = clothes;  
    }

    private void SetItemToLeftHand(Item item){
        leftHandItem = item;       
        human.leftHand = Instantiate(customizeSystem.GetLeftHand(human.humanType),human.leftHand.transform);
    }
}
