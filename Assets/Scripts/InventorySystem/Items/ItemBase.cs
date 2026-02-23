using UnityEngine;

public enum ItemType { Generic, Consumable, Equipment }

[CreateAssetMenu(fileName = "Item", menuName = "Inventory System/Items/Generic")]
public class ItemBase : ScriptableObject
{
    public string Name;
    [TextArea] public string Description;
    public Sprite ImageUI;

    public bool IsStackable;
    public int Cost;

    public ItemType Type;
}