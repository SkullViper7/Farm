using System.Collections.Generic;
using StarterAssets;
using TMPro;
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
    ItemSO _tempItem;

    /// <summary>
    /// Instantiates the items in the store with their respective data.
    /// </summary>
    void Start()
    {
        // Instantiate the canabis item
        GameObject canabis = Instantiate(_itemPrefab, _slots[0].transform);
        // Set its item data to the canabis item data
        canabis.GetComponent<Item>().ItemData = _canabisSO;

        // Instantiate the mushroom item
        GameObject mushroom = Instantiate(_itemPrefab, _slots[1].transform);
        // Set its item data to the mushroom item data
        mushroom.GetComponent<Item>().ItemData = _mushroomSO;
    }


    /// <summary>
    /// Sets the item to be sold and enables the context box
    /// </summary>
    /// <param name="item">The item to be sold</param>
    public void ItemChoose(ItemSO item)
    {
        // Set the amount to 1 and update the text
        _amountInt = 1;
        _amount.text = _amountInt.ToString();

        // Enable the context box and set the price and money text
        _contextBox.SetActive(true);
        _price.text = item.Price.ToString();
        _moneyText.text = _inventoryScript.Money.ToString();

        // Set the item to be sold
        _tempItem = item;
    }


    /// <summary>
    /// Increases the amount of the item that is being sold
    /// </summary>
    public void IncreaseAmount()
    {
        // Increase the amount of the item by 1 and update the text
        _amount.text = (_amountInt + 1).ToString();

        // Increase the amount
        _amountInt++;
    }


    /// <summary>
    /// Decreases the amount of the item being sold
    /// </summary>
    public void DecreaseAmount()
    {
        // Only decrease the amount if it is greater than 1
        if (int.Parse(_amount.text) > 1)
        {
            // Decrease the amount of the item by 1 and update the text
            _amount.text = (int.Parse(_amount.text) - 1).ToString();

            // Decrease the amount
            _amountInt--;
        }
    }

    /// <summary>
    /// Closes the context box without buying any items
    /// </summary>
    public void CancelBuy()
    {
        _contextBox.SetActive(false); // Hide the context box
    }

    /// <summary>
    /// Buys the item being sold if there are enough slots in the inventory and the player has enough money
    /// </summary>
    public void BuyItem()
    {
        // Check if there are enough slots in the inventory and if the player has enough money
        if (_amountInt <= _inventoryScript.AvailableSlots && _inventoryScript.Money >= int.Parse(_price.text))
        {
            // Remove the cost from the player's money and hide the context box
            _inventoryScript.Money -= int.Parse(_price.text);
            _contextBox.SetActive(false);

            // Add the item to the inventory
            _inventoryScript.AddItem(_tempItem, _amountInt);
        }

        // If there are not enough slots in the inventory, show the no slot error
        if (_amountInt > _inventoryScript.AvailableSlots)
        {
            _noSlotError.SetActive(true);
        }

        // If the player does not have enough money, show the no money error
        if (_inventoryScript.Money < int.Parse(_price.text))
        {
            _noMoneyError.SetActive(true);
        }
    }
}
