using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    public InventoryItemData itemReference;

    public void OnHandlePickupItem()
    {
        Inventory.instance.Add(itemReference);
        Destroy(gameObject);
    }
}
