using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Inventory System/Items/Weapons")]
public class ItemWeapon : ConsumableItem
{
    public int DamagePoints;

    public override void Use(IConsume consumer)
    {
        consumer.Use(this);
    }
}
