using System.Collections;
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

    private void Awake()
    {
        _starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        _inventoryScript = GetComponent<Inventory>();
        _animator = _inventory.GetComponent<Animator>();
    }


    void Start()
    {
        _slotList = _inventoryScript.SlotList;
        DepthOfField _tempDof;
        if (_volume.profile.TryGet<DepthOfField>(out _tempDof))
        {
            _dof = _tempDof;
        }
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

        IsOpen = true;

        _dof.focalLength.value = 55;
    }

    public void OpenInventory()
    {
        _inventory.SetActive(true);
        _pauseMenu.SetActive(false);

        IsOpen = true;

        _dof.focalLength.value = 55;
    }


    public void CloseInventory()
    {
        _starterAssetsInputs.IsLocked = false;
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;

        IsOpen = false;
        _animator.Play("DropPhone");
        _dof.focalLength.value = 0;
        Invoke("DisableInventory", 0.5f);
    }

    void DisableInventory()
    {
        _inventory.SetActive(false);
    }

    public void RemoveFromUI(ItemSO item)
    {
        for (int i = 0; i < _slotList.Count; i++)
        {
            if (_slotList[i].transform.childCount != 0)
            {
                TMP_Text text = _slotList[i].transform.GetChild(0).GetChild(1).GetComponent<TMP_Text>();

                if (int.Parse(text.text) == 1)
                {
                    Destroy(_slotList[i].transform.GetChild(0).gameObject);
                    break;
                }
                else
                {
                    text.text = (int.Parse(text.text) - 1).ToString();
                }
            }
        }

        UpdateInventory();
    }

    public void UpdateInventory()
    {
        _items = new List<ItemSO>(_inventoryScript.ItemList);

        _items = _items.GroupBy(x => x.Name).Select(x => x.ToList()).Select(x => x.First()).ToList();

        // Instantiate an _itemPrefab for each item in _items in the available slots
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
                GameObject slot = GetFirstAvailableSlot();

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

            // Modify the TMP_Text of each of with the correct amount stored earlier
            itemObj.transform.GetChild(1).GetComponent<TMP_Text>().text = _inventoryScript.ItemList.Count(x => x.Name == item.Name).ToString();
        }

        // Update the money text on the inventory
        _moneyText.text = _inventoryScript.Money.ToString();
    }


    GameObject GetFirstAvailableSlot()
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
