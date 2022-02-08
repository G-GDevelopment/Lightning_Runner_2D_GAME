using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    public InventoryItemData itemReference;

    public void OnHandlePickupItem()
    {
        InventorySystem.instance.Add(itemReference);
        Destroy(gameObject);
    }
}
