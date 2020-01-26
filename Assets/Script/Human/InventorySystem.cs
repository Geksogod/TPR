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
    public void AddItems(List<Item> items)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (!isInventoryFull)
            {
                inventory.Add(items[i]);
                if (inventory.Count >= inventoryLenth)
                {
                    isInventoryFull = true;
                }
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
                if (inventory.Count < inventoryLenth)
                {
                    isInventoryFull = false;
                }
                return item;
            }
        }
        return null;
    }
    public void GetAllResorces(InventorySystem toInventory)
    {
        List<int> removeIndex = new List<int>();
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].GetType() == typeof(Resource))
            {
                toInventory.AddItem(inventory[i]);
                removeIndex.Add(i);
              
            }
        }
    }
    public bool isFull()
    {
        return isInventoryFull;
    }

}
