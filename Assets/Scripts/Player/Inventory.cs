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
    public List<GameObject> Items;

    [SerializeField] GameObject _inventory;
    [SerializeField] GameObject _pauseMenu;
    public List<GameObject> SlotList;

    [SerializeField] GameObject _itemPrefab;

    public int AvailableSlots = 6;
    public int Money;
    [SerializeField] TMP_Text _moneyText;

    StarterAssetsInputs _starterAssetsInputs;

    private void Awake()
    {
        _starterAssetsInputs = GetComponent<StarterAssetsInputs>();
    }

    /// <summary>
    /// Opens the inventory when the inventory button is pressed
    /// </summary>
    /// <param name="value">InputValue from the button being pressed</param>
    public void OnInventory(InputValue value)
    {
        // Show the inventory and pause menu
        _inventory.SetActive(true);
        _pauseMenu.SetActive(false);

        // Lock the player's movements
        _starterAssetsInputs.IsLocked = true;

        ItemInteract.Instance.IsPlanting = false;
        ItemInteract.Instance.IsSelling = false;

        // Pause the game
        Time.timeScale = 0;

        // Show the cursor
        Cursor.lockState = CursorLockMode.None;

        // Update the money text on the inventory
        _moneyText.text = Money.ToString();

        // Check for duplicate items in the inventory
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

    public void UpdateInventory()
    {
        foreach (GameObject slot in SlotList)
        {
            if (slot.transform.childCount > 0)
            {
                Destroy(slot.transform.GetChild(0).gameObject);
            }
        }

        Items = Items.OrderBy(x => x.GetComponent<Item>().ItemData.Name).ToList();

        for (int i = 0; i < Items.Select(x => x.GetComponent<Item>().ItemData).Distinct().OrderBy(x => x.Name).Count(); i++)
        {
            int count = Items.Count(x => x.GetComponent<Item>().ItemData == Items[i].GetComponent<Item>().ItemData);
            GameObject item = Instantiate(Items[i], GetFirstAvailableSlot().transform);
            item.transform.GetChild(1).GetComponent<TMP_Text>().text = count.ToString();
        }
    }

    public void AddItem(GameObject item, int amount)
    {
        for (int i = 1; i < amount + 1; i++)
        {
            Items.Add(item);
        }

        UpdateInventory();
    }

    public void RemoveItem(GameObject item, int amount)
    {
        for (int i = 1; i < amount + 1; i++)
        {
            Debug.Log(Items.Contains(item));
            Items.Remove(item);
        }

        UpdateInventory();
    }


    public GameObject GetFirstAvailableSlot()
    {
        for (int i = 0; i < SlotList.Count; i++)
        {
            if (SlotList[i].transform.childCount == 0)
            {
                return SlotList[i].gameObject;
            }
        }
        return null;
    }
}
