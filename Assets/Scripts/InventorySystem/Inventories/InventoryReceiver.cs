using UnityEngine;

public class InventoryReceiver : MonoBehaviour, IInventoryReceiver
{
    public Inventory Inventory;

    public Inventory GetInventory()
    {
        return Inventory;
    }
}
