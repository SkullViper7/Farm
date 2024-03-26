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

    [SerializeField] StarterAssetsInputs _starterAssetsInputs;

    public void ItemChoose(Item item)
    {
        _contextBox.SetActive(true);
        _price.text = item.Price.ToString();
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
}
