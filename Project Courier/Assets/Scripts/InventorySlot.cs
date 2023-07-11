using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public struct InventorySlot
{
    private bool _isOccupied;

    private ScriptableItemBase _item;

    public bool IsOccupied { get { return _isOccupied; } }

    public ScriptableItemBase Item
    {
        get { return _item; }
        set
        {
            _item = value;
            if (value == null)
                _isOccupied = false;
            else
                _isOccupied = true;
        }
    }

    public InventorySlot(bool ignore = false)
    {
        _item = null;
        _isOccupied = false;
    }
}
