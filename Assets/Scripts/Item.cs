using System.Collections;
using System.Collections.Generic;
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

    private void Start()
    {
        if (transform.parent.tag == "InventorySlot" || transform.parent.tag == "StoreSlot")
        {
            _sprite = GetComponent<Image>();
            _text = GetComponentInChildren<TMP_Text>();

            _name = ItemData.Name;
            gameObject.name = _name;
            _icon = ItemData.Icon;
            Price = ItemData.Price;
            Value = ItemData.Value;

            if (!ItemInteract.Instance.IDCreated)
            {
                ID = Random.Range(0, 10000);
                ItemInteract.Instance.IDCreated = true;
            }

            _sprite.sprite = _icon;
            _text.text = _name;
            Tag = gameObject.tag;

            GetComponent<Button>().onClick.AddListener(delegate{ItemInteract.Instance.Click(ItemData);});
        }
    }
}
