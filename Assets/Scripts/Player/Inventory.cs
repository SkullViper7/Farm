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
            _itemsUI.transform.GetChild(i).GetComponent<Item>().ItemData = Items[i].ItemData;
            _itemsUI.transform.GetChild(i).gameObject.SetActive(Items.Count > 0);
        }
    }

    public void AddItem(Item item)
    {
        Items.Add(item);
    }
}
