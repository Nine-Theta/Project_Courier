using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class InventoryManager : MonoBehaviour
{
    private int _slotsOccupied = 0;
    private int _maxSlots;

    [SerializeField]
    private InventorySlot[,] _inventorySlots;

    [SerializeField]
    private Vector2Int _inventorySize;

    [SerializeField]
    private List<ScriptableItemBase> _inventoryItems;


    private void Awake()
    {
        _maxSlots = _inventorySize.x * _inventorySize.y;

        _inventoryItems = new List<ScriptableItemBase>();
        _inventorySlots = new InventorySlot[_inventorySize.x, _inventorySize.y];
    }

    public bool TryAddItem(ScriptableItemBase pItem)
    {
        if (pItem.InventorySize.x * pItem.InventorySize.y > _maxSlots - _slotsOccupied) return false;

        _inventoryItems.Add(pItem);


        return true;

    }

    private void AddItemToSlots(ScriptableItemBase pItem)
    {
        for(int i = 0; i < _inventorySize.x; i++)
        {
            for (int j = 0; j < _inventorySize.y; j++)
            {
                if (_inventorySlots[i, j].IsOccupied)
                {
                    continue;
                }
                else if (pItem.InventorySize.x * pItem.InventorySize.y == 1)
                {
                    _inventorySlots[i, j].Item = pItem;
                    return;
                }
                else if(CheckSlots(pItem, i, j))
                {
                    _inventorySlots[i, j].Item = pItem;
                    return;
                }
            }
        }

        SortInventory();

    }

    //makes me cry
    private bool CheckSlots(ScriptableItemBase pItem, int pXoffset, int pYoffset)
    {
        for (int k = 0; k < pItem.InventorySize.x; k++)
        {
            for (int l = 0; l < pItem.InventorySize.y; l++)
            {
                if (_inventorySlots[pXoffset + k, pYoffset + l].IsOccupied)
                    return false;
            }
        }
        return true;
    }

    public void SortInventory()
    {
        //TODO
        throw null; //bad
    }

}
