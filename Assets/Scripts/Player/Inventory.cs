using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;

public class Inventory : MonoBehaviour
{
    public List<Item> Items;

    [SerializeField] GameObject _inventory;
    [SerializeField] GameObject _pauseMenu;

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
}
