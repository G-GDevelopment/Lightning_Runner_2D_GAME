using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Inventory Item Data" ,menuName = "Inventory Data")]
public class InventoryItemData : ScriptableObject
{
    public string Id;
    public string DisplayName;
    public bool[] Ability = new bool[3];
    public Sprite Icon;
    public GameObject Prefab;
}
