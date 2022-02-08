using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryItem
{
    [SerializeField] private InventoryItemData _data;
    public int StackSize { get; private set; }
    public InventoryItemData Data { get => _data; set => _data = value; }

    public InventoryItem(InventoryItemData data)
    {
        this._data = data;
        AddToStack();
    }

    public void AddToStack()
    {
        StackSize++;
    }

    public void RemoveFromStack()
    {
        StackSize--;
    }
}
