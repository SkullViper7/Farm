using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;
using UnityEngine.InputSystem;
using StarterAssets;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

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

    Animator _animator;

    DepthOfField _dof;
    [SerializeField] Volume _volume;

    /// <summary>
    /// Gets the necessary components on Awake.
    /// </summary>
    private void Awake()
    {
        // Get the input manager for the inventory UI
        _starterAssetsInputs = GetComponent<StarterAssetsInputs>();

        // Get the inventory script to retrieve items and slots
        _inventoryScript = GetComponent<Inventory>();

        // Get the Animator component for the inventory animations
        _animator = _inventory.GetComponent<Animator>();
    }


    /// <summary>
    /// Gets the DepthOfField component from the volume profile
    /// </summary>
    void Start()
    {
        _slotList = _inventoryScript.SlotList;

        // Try to get the DepthOfField component from the volume profile
        // If successful, assign it to the private field
        DepthOfField _tempDof;
        if (_volume.profile.TryGet<DepthOfField>(out _tempDof))
        {
            _dof = _tempDof;
        }
    }

        
    /// <summary>
    /// Opens the inventory UI by showing the inventory and pausing the game.
    /// </summary>
    /// <param name="value">Input value from InputSystem</param>
    public void OnInventory(InputValue value)
    {
        // Show the inventory and pause menu
        _inventory.SetActive(true);
        _pauseMenu.SetActive(false);

        // Lock the player's movements
        _starterAssetsInputs.IsLocked = true;

        // Set instance IsPlanting and IsSelling to false
        ItemInteract.Instance.IsPlanting = false;
        ItemInteract.Instance.IsSelling = false;

        // Pause the game
        Time.timeScale = 0;

        // Show the cursor
        Cursor.lockState = CursorLockMode.None;

        IsOpen = true;

        // Set the DepthOfField component's focal length to 55
        _dof.focalLength.value = 55;
    }


    /// <summary>
    /// Opens the inventory UI by showing the inventory and pausing the game.
    /// </summary>
    public void OpenInventory()
    {
        // Show the inventory and pause menu
        _inventory.SetActive(true);
        _pauseMenu.SetActive(false);

        // Set IsOpen to true so the player can't open the inventory again
        IsOpen = true;

        // Set the DepthOfField component's focal length to 55
        _dof.focalLength.value = 55;
    }


    /// <summary>
    /// Closes the inventory UI by unpausing the game and disabling the inventory UI.
    /// </summary>
    public void CloseInventory()
    {
        // Unlock the player's movements
        _starterAssetsInputs.IsLocked = false;

        // Resume the game
        Time.timeScale = 1;

        // Lock the cursor
        Cursor.lockState = CursorLockMode.Locked;

        // Set IsOpen to false so the player can open the inventory again
        IsOpen = false;

        // Play the animation that drops the phone
        _animator.Play("DropPhone");

        // Set the DepthOfField component's focal length to 0
        _dof.focalLength.value = 0;

        // Disable the inventory UI after the animation has finished playing
        Invoke("DisableInventory", 0.5f);
    }

    /// <summary>
    /// Disables the inventory UI after the "DropPhone" animation has finished playing.
    /// </summary>
    void DisableInventory()
    {
        // Disable the inventory UI
        _inventory.SetActive(false);
    }

    /// <summary>
    /// Removes an item from the inventory UI.
    /// </summary>
    /// <param name="item">The ItemSO to remove from the inventory UI.</param>
    public void RemoveFromUI(ItemSO item)
    {
        // Loop through the slots in the inventory UI
        for (int i = 0; i < _slotList.Count; i++)
        {
            // Check if the slot has an item child object
            if (_slotList[i].transform.childCount != 0)
            {
                // Get a reference to the text child of the item child object
                TMP_Text text = _slotList[i].transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>();

                // Check if the amount of items in the slot is 1
                if (int.Parse(text.text) == 1)
                {
                    // If it is, destroy the item child object and break out of the loop
                    Destroy(_slotList[i].transform.GetChild(0).gameObject);
                    break;
                }
                else
                {
                    // Decrement the amount of items in the slot and update the inventory UI
                    text.text = (int.Parse(text.text) - 1).ToString();
                }
            }
        }

        // Update the inventory UI to reflect the changes
        UpdateInventory();
    }


    /// <summary>
    /// Updates the inventory UI to reflect the current state of the inventory.
    /// </summary>
    public void UpdateInventory()
    {
        // Get a distinct list of items in the inventory (to avoid duplicates)
        _items = new List<ItemSO>(_inventoryScript.ItemList.GroupBy(x => x.Name).Select(x => x.First()));

        // Instantiate an item prefab for each item that is not already instantiated in the available slots
        for (int i = 0; i < _items.Count; i++)
        {
            ItemSO item = _items[i];

            bool alreadyInstantiated = false;

            // Check if an item is already instanciated in the slot
            for (int j = 0; j < _slotList.Count; j++)
            {
                if (_slotList[j].transform.childCount != 0)
                {
                    ItemSO slotItem = _slotList[j].transform.GetChild(0).GetComponent<Item>().ItemData;

                    if (slotItem.Name == item.Name)
                    {
                        alreadyInstantiated = true;
                        break;
                    }
                }
            }

            if (!alreadyInstantiated)
            {
                // Get the first available slot
                GameObject slot = GetFirstAvailableSlot();

                // Instantiate the item prefab in the slot if there is one available
                if (slot != null)
                {
                    GameObject itemObj = Instantiate(_itemPrefab, slot.transform);
                    itemObj.GetComponent<Item>().ItemData = item;
                }
            }
        }

        // Update the items in the inventory with the correct ItemData
        for (int i = 0; i < _items.Count; i++)
        {
            ItemSO item = _items[i];

            GameObject itemObj = _slotList[i].transform.GetChild(0).gameObject;
            itemObj.GetComponent<Item>().ItemData = item;

            // Modify the TMP_Text of each item with the correct amount stored earlier
            itemObj.transform.GetChild(1).GetComponent<TMP_Text>().text = _inventoryScript.ItemList.Count(x => x.Name == item.Name).ToString();
        }

        // Update the money text on the inventory
        _moneyText.text = _inventoryScript.Money.ToString();
    }



    /// <summary>
    /// Returns the first available slot in the inventory UI
    /// </summary>
    /// <returns>The first available slot as a GameObject</returns>
    GameObject GetFirstAvailableSlot()
    {
        // Loop through the slots in the inventory UI
        for (int i = 0; i < _slotList.Count; i++)
        {
            // Check if the slot is empty
            if (_slotList[i].transform.childCount == 0)
            {
                // If it is, return the slot as a GameObject and exit the function
                return _slotList[i].gameObject;
            }
        }
        // If no slot is found, return null
        return null;
    }

}
