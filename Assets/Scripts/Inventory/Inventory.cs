using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : CoreComponents
{
    [SerializeField] private LayerMask _itemLayer;

    public void LogicUpdate()
    {
        if (core.CollisionSenses.ItemChecker)
        {
            PickUpItem(core.CollisionSenses.ItemChecker);
        }

    }

    private void PickUpItem(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent<ItemObject>(out ItemObject item))
        {
            Debug.Log("Destroy and add item");
            item.OnHandlePickupItem();

        }
    }
}
