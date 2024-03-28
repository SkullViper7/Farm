using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Selling : MonoBehaviour
{
    [SerializeField] List<Button> _slots;
    [SerializeField] GameObject _contextBox;

    Inventory _inventoryScript;

    [SerializeField] TMP_Text _amount;
    [SerializeField] TMP_Text _value;
    int _amountInt = 1;

    Item _tempItem;

    private void Awake()
    {
        _inventoryScript = GetComponent<Inventory>();
    }

    public void ChangeOnClick()
    {
        for (int i = 0; i < _slots.Count; i++)
        {
            Button slot = _slots[i];
            slot.onClick.RemoveAllListeners();
            slot.onClick.AddListener(() => ShowContextBox(slot.GetComponent<Item>()));
        }
    }
    
    public void IncreaseAmount()
    {
        _amount.text = (int.Parse(_amount.text) + 1).ToString();
        _amountInt++;
    }

    public void DecreaseAmount()
    {
        if (int.Parse(_amount.text) > 1)
        {
            _amount.text = (int.Parse(_amount.text) - 1).ToString();
            _amountInt--;
        }
    }

    public void ShowContextBox(Item item)
    {
        _contextBox.SetActive(true);
        _amountInt = 1;
        _amount.text = _amountInt.ToString();
        _value.text = item.Value.ToString();
        _tempItem = item;
    }

    public void SellItem()
    {
        _inventoryScript.RemoveItem(_tempItem, _amountInt);
        _inventoryScript.Money += _tempItem.Value;
        _contextBox.SetActive(false);
        _inventoryScript.CheckDuplicates();
    }

    public void CancelSell()
    {
        _contextBox.SetActive(false);
    }
}
