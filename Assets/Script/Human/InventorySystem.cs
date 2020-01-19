using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    [SerializeField]
    private List<Item> inventory = new List<Item>();
    [SerializeField]
    private bool isInventoryFull;
    [SerializeField]
    private int inventoryLenth;

    public void AddItem(Item item)
    {
        if (!isInventoryFull)
        {
            inventory.Add(item);
            if (inventory.Count >= inventoryLenth)
            {
                isInventoryFull = true;
            }
        }
    }
    public Item GetItem(string itemName)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].GetName() == itemName)
            {
                Item item = inventory[i];
                inventory.RemoveAt(i);
                if(inventory.Count<inventoryLenth){
                    isInventoryFull = false;
                }
                return item;
            }
        }
        return null;
    }
    public bool isFull(){
        return isInventoryFull;
    }
    
}
