using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryItem
{
    [SerializeField] private InventoryItemData _data;
    [SerializeField] private int _stackSize;
    public InventoryItemData Data { get => _data; set => _data = value; }
    public int StackSize { get => _stackSize; set => _stackSize = value; }

    public InventoryItem(InventoryItemData data)
    {
        this._data = data;
        AddToStack();
    }

    public void AddToStack()
    {
        _stackSize++;
    }

    public void RemoveFromStack()
    {
        _stackSize--;
    }
}
