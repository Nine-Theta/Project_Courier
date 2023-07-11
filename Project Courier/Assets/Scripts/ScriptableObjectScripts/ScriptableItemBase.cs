using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemScriptable", menuName = "ScriptableObjects/Item")]
public class ScriptableItemBase : ScriptableObject
{
    [SerializeField]
    private string _name;
    [SerializeField, Multiline]
    private string _description;

    [SerializeField]
    private Sprite _icon;

    [SerializeField]
    private int _value;

    [SerializeField]
    private Vector2Int _inventorySize;

    public string Name { get { return _name; } }
    public string Description { get { return _description; } }

    public Sprite Icon { get { return _icon; } }

    public int Value { get { return _value; } }

    public Vector2 InventorySize { get { return _inventorySize; } }
}
