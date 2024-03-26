using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Store : MonoBehaviour
{
    [SerializeField] GameObject _contextBox;
    [SerializeField] TMP_Text _price;
    [SerializeField] TMP_Text _moneyText;

    [SerializeField] GameObject _noSlotError;
    [SerializeField] GameObject _noMoneyError;

    [SerializeField] StarterAssetsInputs _starterAssetsInputs;
    [SerializeField] Inventory _inventoryScript;

    [SerializeField] TMP_Text _amount;
    int _amountInt = 1;
    Item _tempItem;

    public void ItemChoose(Item item)
    {
        _contextBox.SetActive(true);
        _price.text = item.Price.ToString();
        _moneyText.text = _inventoryScript.Money.ToString();
        _tempItem = item;
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

    public void CancelBuy()
    {
        _contextBox.SetActive(false);
    }

    public void CloseStore()
    {
        _starterAssetsInputs.IsLocked = false;
        Cursor.lockState = CursorLockMode.Locked;
        gameObject.SetActive(false);
    }

    public void BuyItem()
    {
        if (_amountInt <= _inventoryScript.AvailableSlots && _inventoryScript.Money >= int.Parse(_price.text))
        {
            _inventoryScript.Money -= int.Parse(_price.text);
            _contextBox.SetActive(false);
            _inventoryScript.AddItem(_tempItem, _amountInt);
        }

        if (_amountInt > _inventoryScript.AvailableSlots)
        {
            _noSlotError.SetActive(true);
        }

        if (_inventoryScript.Money < int.Parse(_price.text))
        {
            _noMoneyError.SetActive(true);
        }
    }
}
