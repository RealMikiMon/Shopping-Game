using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlotUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Image Image;
    public TextMeshProUGUI AmountText;

    private Canvas canvas;
    private Transform parent;
    private ItemBase item;
    private InventoryUI inventory;
    private GraphicRaycaster raycaster;
    private EventSystem eventSystem;

    public void Initialize(ItemSlot slot, InventoryUI inventory)
    {
        Image.sprite = slot.Item.ImageUI;
        Image.SetNativeSize();
        AmountText.text = slot.Amount.ToString();
        AmountText.enabled = (slot.Amount > 1);

        item = slot.Item;
        this.inventory = inventory;

        if (!canvas) canvas = GetComponentInParent<Canvas>();
        if (canvas) raycaster = canvas.GetComponent<GraphicRaycaster>();
        eventSystem = EventSystem.current;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!canvas) canvas = GetComponentInParent<Canvas>();
        parent = transform.parent;
        transform.SetParent(canvas.transform, true);
        transform.SetAsLastSibling();
    }
    public void OnDrag(PointerEventData eventData)
    {
        transform.localPosition += new Vector3(eventData.delta.x, eventData.delta.y, 0);
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        List<RaycastResult> results = new List<RaycastResult>();
        raycaster.Raycast(eventData, results);
        foreach (var result in results)
        {
            var consumer = result.gameObject.GetComponent<IConsume>();
            if (consumer != null && item is ConsumableItem)
            {
                (item as ConsumableItem).Use(consumer);
                inventory.UseItem(item);
            }
            var receiver = result.gameObject.GetComponent<IInventoryReceiver>();
            if (receiver != null)
            {
                Inventory targetInventory = receiver.GetInventory();
                Inventory sourceInventory = inventory.Inventory;

                if (targetInventory != sourceInventory)
                {
                    var money = inventory.MoneyUI;

                    if (sourceInventory.name == "PlayerInventory" &&
                        targetInventory.name == "ShopInventory")
                    {
                        money.AddMoney(item.Cost);
                        targetInventory.AddItem(item);
                        sourceInventory.RemoveItem(item);
                    }
                    else if (sourceInventory.name == "ShopInventory" &&
                             targetInventory.name == "PlayerInventory")
                    {
                        if (money.CanAfford(item.Cost))
                        {
                            money.SpendMoney(item.Cost);
                            targetInventory.AddItem(item);
                            sourceInventory.RemoveItem(item);
                        }
                        else
                        {
                            Debug.Log("No tens prou diners!");
                        }
                    }
                }
            }
        }
        transform.SetParent(parent.transform);
        transform.localPosition = Vector3.zero;
    }
}
