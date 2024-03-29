using System.Collections;
using System.Collections.Generic;
using System.Linq;
using StarterAssets;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public List<ItemSO> ItemList;

    public List<GameObject> SlotList;

    [SerializeField] Transform _inventory;

    [SerializeField] GameObject _itemPrefab;

    [SerializeField] Store _storeScript;

    public int AvailableSlots = 6;
    public int Money;

    InventoryUI _inventoryUIScript;

    void Start()
    {
        _inventoryUIScript = GetComponent<InventoryUI>();
    }

    public void AddItem(ItemSO item, int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            ItemList.Add(item);
        }

        _inventoryUIScript.UpdateInventory();
    }

    public void RemoveItem(ItemSO item, int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            int index = ItemList.FindIndex(tempItem => tempItem.Name == item.Name);
            if (index != -1)
                ItemList.RemoveAt(index);
        }

        _inventoryUIScript.RemoveFromUI(item);
    }
}
