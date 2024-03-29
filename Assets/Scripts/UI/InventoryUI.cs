using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;
using UnityEngine.InputSystem;
using StarterAssets;

public class InventoryUI : MonoBehaviour
{
    Inventory _inventoryScript;
    StarterAssetsInputs _starterAssetsInputs;

    List<GameObject> _slotList;
    List<ItemSO> _items;

    [SerializeField] GameObject _inventory;
    [SerializeField] GameObject _pauseMenu;

    [SerializeField] TMP_Text _moneyText;

    [SerializeField] GameObject _itemPrefab;

    public bool IsOpen;

    int _amount;

    private void Awake()
    {
        _starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        _inventoryScript = GetComponent<Inventory>();
    }


    void Start()
    {
        _slotList = _inventoryScript.SlotList;
    }
        
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
        _moneyText.text = _inventoryScript.Money.ToString();

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
        _items = new List<ItemSO>(_inventoryScript.ItemList);

        for (int i = 0; i < _slotList.Count; i++)
        {
            if (_slotList[i].transform.childCount > 0)
            {
                Destroy(_slotList[i].transform.GetChild(0).gameObject);
            }
        }

        // Loop through items and delete duplicates
        for (int i = 0; i < _items.Count; i++)
        {
            int j = i + 1;
            _amount = 1;
            while (j < _items.Count)
            {
                if (_items[i].name == _items[j].name)
                {
                    _amount++;
                    _items.RemoveAt(j);
                }
                else
                {
                    j++;
                }
            }
        }

        // Add items to inventory
        foreach (ItemSO item in _items)
        {
            GameObject slot = GetFirstAvailableSlot();
            if (slot != null)
            {
                GameObject newItem = Instantiate(_itemPrefab, slot.transform);
                newItem.GetComponent<Item>().ItemData = item;
                newItem.transform.GetChild(1).GetComponent<TMP_Text>().text = _amount.ToString();
            }
        }
    }


    public GameObject GetFirstAvailableSlot()
    {
        for (int i = 0; i < _slotList.Count; i++)
        {
            if (_slotList[i].transform.childCount == 0)
            {
                return _slotList[i].gameObject;
            }
        }
        return null;
    }
}
