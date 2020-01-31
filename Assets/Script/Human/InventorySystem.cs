using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class InventorySystem : MonoBehaviour
{
    [SerializeField]
    private List<Item> inventory = new List<Item>();
    [SerializeField]
    private bool isInventoryFull;
    [SerializeField]
    private int inventoryLenth;
    [SerializeField]
    private bool isStorage;
    private ResourceManager resourceManager;
    private void Start()
    {
        if (isStorage)
        {
            resourceManager = GameObject.FindObjectOfType<ResourceManager>();
        }
    }

    public void AddItem(Item item)
    {
        if (!isInventoryFull)
        {
            inventory.Add(item);
            if (isStorage && typeof(Resource) == item.GetType())
            {
                resourceManager.AddResorces((Resource)item);
            }
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
                if (isStorage && typeof(Resource) == items[i].GetType())
                {
                    resourceManager.AddResorces((Resource)items[i]);
                }
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
        for (int i = removeIndex.Count - 1; i >= 0; i--)
        {
            inventory.RemoveAt(removeIndex[i]);
        }
        if (inventory.Count < inventoryLenth)
            isInventoryFull = false;

    }
    public bool isFull()
    {
        return isInventoryFull;
    }
    public bool HaveResources()
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].GetType() == typeof(Resource))
                return true;

        }
        return false;
    }

}
