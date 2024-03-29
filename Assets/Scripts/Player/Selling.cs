using TMPro;
using UnityEngine;

public class Selling : MonoBehaviour
{
    [SerializeField] GameObject _contextBox;

    Inventory _inventoryScript;

    [SerializeField] TMP_Text _amount;
    [SerializeField] TMP_Text _value;
    int _amountInt = 1;

    ItemSO _tempItem;

    /**
     * Gets the Inventory script on Awake
     */
    private void Awake()
    {
        // Get the Inventory script
        _inventoryScript = GetComponent<Inventory>();
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
    /// Shows the context box for selling items
    /// </summary>
    /// <param name="item">Item to be sold</param>
    public void ShowContextBox(ItemSO item)
    {
        // Show the context box
        _contextBox.SetActive(true);

        // Set the amount to 1 and update the text
        _amountInt = 1;
        _amount.text = _amountInt.ToString();

        // Set the value of the item and update the text
        _value.text = item.Value.ToString();

        // Set the item to be sold
        _tempItem = item;
    }


    /// <summary>
    /// Sells the item being sold and adds the value of the item to the player's money
    /// </summary>
    public void SellItem()
    {
        // Remove the item from the player's inventory and add the value of the item to their money
        _inventoryScript.RemoveItem(_tempItem, _amountInt);
        _inventoryScript.Money += _tempItem.Value;

        // Close the context box
        _contextBox.SetActive(false);
    }

    /// <summary>
    /// Closes the context box without selling anything
    /// </summary>
    public void CancelSell()
    {
        // Close the context box without selling anything
        _contextBox.SetActive(false);
    }

}
