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

    GameObject _tempItem;

    private void Awake()
    {
        _inventoryScript = GetComponent<Inventory>();
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

    public void ShowContextBox(GameObject item)
    {
        _contextBox.SetActive(true);
        _amountInt = 1;
        _amount.text = _amountInt.ToString();
        _value.text = item.GetComponent<Item>().Value.ToString();
        _tempItem = item;
    }

    public void SellItem()
    {
        _inventoryScript.RemoveItem(_tempItem, _amountInt);
        _inventoryScript.Money += _tempItem.GetComponent<Item>().Value;
        _contextBox.SetActive(false);
        _inventoryScript.UpdateInventory();
    }

    public void CancelSell()
    {
        _contextBox.SetActive(false);
    }
}
