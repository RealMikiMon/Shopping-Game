using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour, IPickUp
{
    public Inventory Inventory;

    public void PickUp(ICanBePicked item)
    {
        Inventory.AddItem(item.GetItem());
    }
}
