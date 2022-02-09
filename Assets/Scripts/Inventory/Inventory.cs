using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : CoreComponents
{
    [Header("Items in inventory")]
    [SerializeField] private int _coins;
    [SerializeField] private int _extraLife;
    [SerializeField] private int _godRemnants;

    private Dictionary<InventoryItemData, InventoryItem> _itemDictionary;
    [SerializeField] private List<InventoryItem> _inventory;
    public List<InventoryItem> PlayerInventory { get => _inventory; set => _inventory = value; }

    [Space]
    [SerializeField] private LayerMask _itemLayer;

    public int Coins { get => _coins; set => _coins = value; }
    public int ExtraLife { get => _extraLife; set => _extraLife = value; }
    public int GodRemnants { get => _godRemnants; set => _godRemnants = value; }

    public static Inventory instance;

    protected override void Awake()
    {
        base.Awake();

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        //DontDestroyOnLoad(gameObject);

        _inventory = new List<InventoryItem>();
        _itemDictionary = new Dictionary<InventoryItemData, InventoryItem>();

    }

    public void LogicUpdate()
    {
        if (core.CollisionSenses.ItemChecker)
        {
            PickUpItem(core.CollisionSenses.ItemChecker);
        }

        //Set items amount to match inventorySystem

    }

    public void Add(InventoryItemData itemReference)
    {
        if (_itemDictionary.TryGetValue(itemReference, out InventoryItem value))
        {
            value.AddToStack();
        }
        else
        {
            InventoryItem newItem = new InventoryItem(itemReference);
            _inventory.Add(newItem);
            _itemDictionary.Add(itemReference, newItem);

        }
    }
    public void Remove(InventoryItemData itemReference)
    {
        if (_itemDictionary.TryGetValue(itemReference, out InventoryItem value))
        {
            value.RemoveFromStack();

            if (value.StackSize == 0)
            {
                _inventory.Remove(value);
                _itemDictionary.Remove(itemReference);
            }
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

    private void SetItemAmount(InventoryItem p_item, int p_thisItem)
    {
        if(p_item.StackSize > 0)
        {
            p_thisItem = p_item.StackSize;
        }
    }
}
