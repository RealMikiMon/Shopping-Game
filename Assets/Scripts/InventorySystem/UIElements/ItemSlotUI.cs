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

    public void Initialize(ItemSlot slot, InventoryUI inventory)
    {
        Image.sprite = slot.Item.ImageUI;
        Image.SetNativeSize();
        AmountText.text = slot.Amount.ToString();
        AmountText.enabled = (slot.Amount > 1);
        item = slot.Item;
        this.inventory = inventory;
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
        RaycastHit2D hitData = Physics2D.GetRayIntersection(
        Camera.main.ScreenPointToRay(Input.mousePosition));

        if (hitData)
        {
            var consumer = hitData.collider.gameObject.GetComponent<IConsume>();
            if ((consumer != null) && (item is ConsumableItem))
            {
                (item as ConsumableItem).Use(consumer);
                inventory.UseItem(item);
            }
        }
        transform.SetParent(parent.transform);
        transform.localPosition = Vector3.zero;
    }
}
