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

    void UpdateInventory()
    {
        for (int i = 0; i < Items.Count; i++)
        {
            // Get the child GameObject for the current item, and get the Item component
            Item itemUI = _itemsUI.transform.GetChild(i).GetComponent<Item>();

            // Set the item data for the UI to display
            itemUI.ItemData = Items[i].ItemData;

            // Show/hide the item UI based on whether there are any items in the inventory
            itemUI.gameObject.SetActive(Items.Count > 0);
        }


        // iterate through all enabled items and remove duplicates
        for (int i = 0; i < Items.Count; i++)
        {
            // skip disabled items
            if (!Items[i].gameObject.activeSelf)
                continue;

            string itemTag = Items[i].tag;

            // find other items with the same tag
            List<Item> duplicates = Items.FindAll(x => x.tag == itemTag && x != Items[i]);

            if (duplicates.Count > 0)
            {
                // get the first duplicate and its count text
                Item duplicateItem = duplicates[0];
                TMP_Text countText = duplicateItem.transform.GetChild(1).GetComponent<TMP_Text>();

                // increment the count of the first item and disable the duplicate
                int.TryParse(countText.text, out int count);
                count++;
                countText.text = count.ToString();
                duplicateItem.gameObject.SetActive(false);
            }
        }
    }

    public void AddItem(Item item, int amount)
    {
        for (int i = 1; i < amount + 1; i++)
        {
            Items.Add(item);
        }
    }
}
