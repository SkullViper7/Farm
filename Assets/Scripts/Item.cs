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

    Image _sprite;

    TMP_Text _text;

    public ItemSO ItemData;

    public string Tag;

    private void Start()
    {
        _sprite = GetComponent<Image>();
        _text = GetComponentInChildren<TMP_Text>();

        _name = ItemData.Name;
        _icon = ItemData.Icon;
        Price = ItemData.Price;

        _sprite.sprite = _icon;
        _text.text = _name;

        Tag = gameObject.tag;
    }
}
