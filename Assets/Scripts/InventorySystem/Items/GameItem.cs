using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameItem : MonoBehaviour, ICanBePicked
{
    public ItemBase Item;

    private void OnTriggerEnter2D(Collider2D other)
    {
        var picker = other.gameObject.GetComponent<IPickUp>();
        if (picker != null)
        {
            picker.PickUp(this); 
            PickedUp(); 
        }
    }

    public void PickedUp()
    {
        Destroy(gameObject);
    }

    public ItemBase GetItem()
    {
        return Item;
    }
}