using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    string _name;
    
    Sprite _icon;

    [HideInInspector] public int Price;
    [HideInInspector] public int Value;

    Image _sprite;

    TMP_Text _text;

    public ItemSO ItemData;

    public string Tag;

    public int ID;

    /// <summary>
    /// Setups the item in the inventory/store
    /// </summary>
    private void Start()
    {
        // If the item is an inventory/store slot item
        if (transform.parent.CompareTag("InventorySlot") || transform.parent.CompareTag("StoreSlot"))
        {
            // Get the Image and Text components
            _sprite = GetComponent<Image>();
            _text = GetComponentInChildren<TMP_Text>();

            // Set the item's name, icon, price and value
            _name = ItemData.Name;
            gameObject.name = _name;
            _icon = ItemData.Icon;
            Price = ItemData.Price;
            Value = ItemData.Value;

            // If an ID has not been created yet, create one
            if (!ItemInteract.Instance.IDCreated)
            {
                ID = Random.Range(0, 10000);
                ItemInteract.Instance.IDCreated = true;
            }

            // Set the item's sprite and text
            _sprite.sprite = _icon;
            _text.text = _name;

            // Set the item's tag
            Tag = gameObject.tag;

            // Add a listener to the item's button to call the Click method
            // of the ItemInteract instance when clicked
            GetComponent<Button>().onClick.AddListener(delegate { ItemInteract.Instance.Click(ItemData); });
        }
    }
}
