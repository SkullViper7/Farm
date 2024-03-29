using System.Collections.Generic;
using UnityEngine;

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

    /// <summary>
    /// Gets the InventoryUI component on start
    /// </summary>
    void Start()
    {
        // Get InventoryUI component
        _inventoryUIScript = GetComponent<InventoryUI>();
    }

    /// <summary>
    /// Adds an item to the player's inventory
    /// </summary>
    /// <param name="item">ItemSO to be added to inventory</param>
    /// <param name="amount">Number of items to be added</param>
    public void AddItem(ItemSO item, int amount)
    {
        // Loop through the amount specified and add the item to the inventory
        for (int i = 0; i < amount; i++)
        {
            ItemList.Add(item);
        }

        // Update the inventory UI with the new items
        _inventoryUIScript.UpdateInventory();
    }


    /// <summary>
    /// Removes an item from the player's inventory
    /// </summary>
    /// <param name="item">ItemSO to be removed from inventory</param>
    /// <param name="amount">Number of items to be removed</param>
    public void RemoveItem(ItemSO item, int amount)
    {
        // Loop through the amount specified and remove the item from the inventory
        for (int i = 0; i < amount; i++)
        {
            // Find the index of the item in the ItemList
            int index = ItemList.FindIndex(tempItem => tempItem.Name == item.Name);

            // If the item exists in the list, remove it
            if (index != -1)
                ItemList.RemoveAt(index);
        }

        // Update the inventory UI to reflect the changes
        _inventoryUIScript.RemoveFromUI(item);
    }
}
