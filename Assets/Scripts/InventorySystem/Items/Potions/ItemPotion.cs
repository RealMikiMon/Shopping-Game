using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Inventory System/Items/Potion")]
public class ItemPotion : ConsumableItem
{
    public int LifeRestore;

    private void OnEnable() { 
        Type = ItemType.Consumable; 
    }

    public override void Use(IConsume consumer)
    {
        consumer.Use(this);
    }
}
