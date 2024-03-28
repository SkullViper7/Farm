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

    [SerializeField] List<GameObject> _slots;

    [SerializeField] GameObject _itemPrefab;

    [SerializeField] ItemSO _canabisSO;
    [SerializeField] ItemSO _mushroomSO;

    [SerializeField] TMP_Text _amount;
    int _amountInt = 1;
    GameObject _tempItem;

    void Start()
    {
        GameObject canabis = Instantiate(_itemPrefab, _slots[0].transform);
        GameObject mushroom = Instantiate(_itemPrefab, _slots[1].transform);

        canabis.GetComponent<Item>().ItemData = _canabisSO;
        mushroom.GetComponent<Item>().ItemData = _mushroomSO;
    }

    public void ItemChoose(GameObject item)
    {
        _amountInt = 1;
        _amount.text = _amountInt.ToString();
        _contextBox.SetActive(true);
        _price.text = item.GetComponent<Item>().Price.ToString();
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
        Debug.Log($"bought {_inventoryScript.Items[0].GetComponent<Item>().ID}");
    }
}
