using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class Store : MonoBehaviour
{
    [SerializeField] GameObject _contextBox;

    [SerializeField] StarterAssetsInputs _starterAssetsInputs;

    public void ItemChoose()
    {
        _contextBox.SetActive(true);
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
