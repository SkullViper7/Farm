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

    public bool IsOpen;

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

        IsOpen = true;
    }

    public void OpenInventory()
    {
        _inventory.SetActive(true);
        _pauseMenu.SetActive(false);

        IsOpen = true;
    }


    public void CloseInventory()
    {
        _inventory.SetActive(false);
        _starterAssetsInputs.IsLocked = false;
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;

        IsOpen = false;
    }

    public void UpdateInventory()
    {
        // First find out which slots already have an item in them
        List<int> filledSlots = new List<int>();
        for (int i = 0; i < SlotList.Count; i++)
        {
            if (SlotList[i].transform.childCount > 0)
            {
                filledSlots.Add(i);
            }
        }

        // Then only instantiate items that are not already instantiated
        Items = Items.OrderBy(x => x.GetComponent<Item>().ItemData.Name).ToList();
        for (int i = 0; i < Items.Select(x => x.GetComponent<Item>().ItemData).Distinct().OrderBy(x => x.Name).Count(); i++)
        {
            int count = Items.Count(x => x.GetComponent<Item>().ItemData == Items[i].GetComponent<Item>().ItemData);
            if (!filledSlots.Contains(i))
            {
                GameObject item = Instantiate(Items[i], GetFirstAvailableSlot().transform);
                item.transform.GetChild(1).GetComponent<TMP_Text>().text = count.ToString();
            }
        }

        Debug.Log($"sorted {Items[0].GetComponent<Item>().ID}");
    }

    public void AddItem(GameObject item, int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Items.Add(item);
        }

        UpdateInventory();
    }

    public void RemoveItem(GameObject item, int amount)
    {
        Debug.Log($"removed {Items[0].GetComponent<Item>().ID}");
        for (int i = 0; i < amount; i++)
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
