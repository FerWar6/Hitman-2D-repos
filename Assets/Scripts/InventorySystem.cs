using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InventorySystem : MonoBehaviour
{
    public List<Item> items = new List<Item>();
    public static Item itemInHand;
    private int currentItemIndex = 0;

    private void Start()
    {
        AddItem("ScrewDriver", true, 1);
        AddItem("ComputerMouse", true, 2);
        itemInHand = items[0];
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            RemoveCurrentItem();
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            Debug.Log(itemInHand.itemName + itemInHand.isLethal);
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            SwitchItems();
            Debug.Log(itemInHand.itemName + itemInHand.isLethal);

        }
    }
    public void AddItem(string name, bool throwable, int lethal)
    {
        Item existingItem = items.Find(item => item.itemName == name);
        if (existingItem != null)
        {
            existingItem.quantity = existingItem.quantity ++;
        }
        else
        {
            Item newItem1 = new Item(name, throwable, lethal);
            items.Add(newItem1);
            Item newItem2 = new Item(name, throwable, lethal);
            items.Add(newItem2);
        }
    }
    public void RemoveCurrentItem()
    {
        items.Remove(itemInHand);
    }    
    public void SwitchItems()
    {
        currentItemIndex = (currentItemIndex + 1) % items.Count;
        itemInHand = items[currentItemIndex];
    }
}

[System.Serializable]
public class Item
{
    public string itemName;
    public bool isThrowable;
    public bool? isLethal;
    public int quantity;

    public Item(string name, bool throwable, int lethal)
    {
        itemName = name;
        isThrowable = throwable;

        if (lethal == 0)
        {
            isLethal = null;
        }
        else if (lethal == 1)
        {
            isLethal = true;
        }
        else if (lethal == 2)
        {
            isLethal = false;
        }
    }
}