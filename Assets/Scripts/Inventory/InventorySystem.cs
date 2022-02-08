using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    private Dictionary<InventoryItemData, InventoryItem> _itemDictionary;
    [SerializeField] private List<InventoryItem> _inventory;
    public List<InventoryItem> Inventory { get => _inventory; set => _inventory = value; }

    public static InventorySystem instance;

    private void Awake()
    {
        if(instance == null)
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

    public void Add(InventoryItemData itemReference)
    {
        if(_itemDictionary.TryGetValue(itemReference, out InventoryItem value))
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
        if(_itemDictionary.TryGetValue(itemReference, out InventoryItem value))
        {
            value.RemoveFromStack();

            if(value.StackSize == 0)
            {
                _inventory.Remove(value);
                _itemDictionary.Remove(itemReference);
            }
        }
    }
}
