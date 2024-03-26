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

    [SerializeField] ItemSO _itemData;

    private void Awake()
    {
        _sprite = GetComponent<Image>();
        _text = GetComponentInChildren<TMP_Text>();

        _name = _itemData.Name;
        _icon = _itemData.Icon;
        Price = _itemData.Price;

        _sprite.sprite = _icon;
        _text.text = _name;
    }
}
