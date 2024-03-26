using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Inventory : MonoBehaviour
{
    public List<Item> Items;

    [SerializeField] GameObject _inventory;
    [SerializeField] GameObject _itemsUI;
    [SerializeField] GameObject _pauseMenu;

    public int Money;
    [SerializeField] TMP_Text _moneyText;

    StarterAssetsInputs _starterAssetsInputs;

    private void Awake()
    {
        _starterAssetsInputs = GetComponent<StarterAssetsInputs>();
    }

    public void OnInventory(InputValue value)
    {
        _inventory.SetActive(true);
        _pauseMenu.SetActive(false);
        _starterAssetsInputs.IsLocked = true;
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        _moneyText.text = Money.ToString();
        UpdateInventory();
    }

    public void OpenInventory()
    {
        _inventory.SetActive(true);
        _pauseMenu.SetActive(false);
    }


    public void CloseInventory()
    {
        _inventory.SetActive(false);
        _starterAssetsInputs.IsLocked = false;
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
    }

    /// <summary>
    /// Updates the inventory UI based on the items in the inventory list
    /// </summary>
    void UpdateInventory()
    {
        // Iterate through all the items in the inventory
        for (int i = 0; i < Items.Count; i++)
        {
            // Get the child GameObject for the current item, and get the Item component
            Item itemUI = _itemsUI.transform.GetChild(i).GetComponent<Item>();

            // Set the item data for the UI to display
            itemUI.ItemData = Items[i].ItemData;

            // Show/hide the item UI based on whether there are any items in the inventory
            itemUI.gameObject.SetActive(Items.Count > 0);

            // Get the tag of the current item
            string itemTag = Items[i].tag;

            // Find out how many items are in the inventory with the same tag as the current item
            int itemCount = Items.FindAll(x => x.tag == itemTag).Count;

            // If there is more than one item with the same tag,
            // hide all the items with the same tag except for the first one
            if (itemCount > 1)
            {
                for (int j = i + 1; j < Items.Count; j++)
                {
                    if (Items[j].tag == itemTag)
                    {
                        Items[j].gameObject.SetActive(false);

                        // Set the quantity text on the first item in the inventory with the same tag
                        itemUI.transform.GetChild(1).GetComponent<TMP_Text>().text = itemCount.ToString();
                    }
                }
            }
        }
    }


    /// <summary>
    /// Adds an item to the player's inventory
    /// </summary>
    /// <param name="item">The item to add to the inventory</param>
    public void AddItem(Item item)
    {
        Items.Add(item);
    }

}
