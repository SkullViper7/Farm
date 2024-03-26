using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;

public class Inventory : MonoBehaviour
{
    public List<Item> Items;

    [SerializeField] GameObject _inventory;
    [SerializeField] GameObject _itemsUI;
    [SerializeField] GameObject _pauseMenu;

    StarterAssetsInputs _starterAssetsInputs;

    private void Awake()
    {
        _starterAssetsInputs = GetComponent<StarterAssetsInputs>();
    }

    private void Start()
    {
        AddItemsToInventory();
    }

    public void OnInventory(InputValue value)
    {
        _inventory.SetActive(true);
        _pauseMenu.SetActive(false);
        _starterAssetsInputs.IsLocked = true;
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
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


    private void AddItemsToInventory()
    {
        Items = new List<Item>();
        Item[] items = Resources.LoadAll<Item>("Items");

        foreach (Item item in items)
        {
            Items.Add(item);
        }
    }
}
