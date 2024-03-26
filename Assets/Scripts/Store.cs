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
    [SerializeField] StarterAssetsInputs _starterAssetsInputs;
    [SerializeField] Inventory _inventoryScript;

    Item _tempItem;

    public void ItemChoose(Item item)
    {
        _contextBox.SetActive(true);
        _price.text = item.Price.ToString();
        _moneyText.text = _inventoryScript.Money.ToString();
        _tempItem = item;
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
        _inventoryScript.Money -= int.Parse(_price.text);
        _contextBox.SetActive(false);
        _inventoryScript.AddItem(_tempItem);
    }
}
