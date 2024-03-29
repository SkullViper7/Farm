using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using StarterAssets;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interaction : MonoBehaviour
{
    [SerializeField] LayerMask _interactableLayer;

    public bool CanInteract;

    [SerializeField] float _interactDistance = 10f;

    [SerializeField] GameObject _interactText;
    [SerializeField] TMP_Text _interactTextTMP;
    [SerializeField] Camera _cam;

    [Header("Interactables")]
    [SerializeField] GameObject _store;

    public RaycastHit Hit;

    StarterAssetsInputs _starterAssetsInputs;
    TP _tpScript;
    Inventory _inventoryScript;
    InventoryUI _inventoryUIScript;

    [SerializeField] GameObject _itemPrefab;
    [SerializeField] ItemSO _canabisSO;
    [SerializeField] ItemSO _mushroomSO;

    [SerializeField] GameObject _mainUI;

    [Header("Cams")]
    [SerializeField] CinemachineVirtualCamera _mainCam;
    [SerializeField] CinemachineVirtualCamera _storeCam;

    private void Awake()
    {
        _starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        _inventoryScript = GetComponent<Inventory>();
        _inventoryUIScript = GetComponent<InventoryUI>();
        _tpScript = GetComponent<TP>();
    }

    void FixedUpdate()
    {
        CanInteract = Physics.Raycast(_cam.transform.position, _cam.transform.forward, out Hit, _interactDistance, _interactableLayer);
        _interactText.SetActive(CanInteract);

        if (CanInteract)
        {
            switch (Hit.transform.tag)
            {
                case "Store": _interactTextTMP.text = "Buy"; break;
                case "Plantation": _interactTextTMP.text = "Plant"; break;
                case "Grabbable": _interactTextTMP.text = "Pickup"; break;
                case "Selling" : _interactTextTMP.text = "Sell"; break;
                case "Door": _interactTextTMP.text = "Go to selling location"; break;
                case "Scooter": _interactTextTMP.text = "Go back home"; break;
            }
        }
    }

    /// <summary>
    /// Handles player interaction with interactables.
    /// </summary>
    /// <param name="value">Input value from InputSystem</param>
    public void OnInteract(InputValue value)
    {
        // If player is looking at interactable
        if (CanInteract)
        {
            // If interactable is store
            if (Hit.transform.tag == "Store")
            {
                // Unlock cursor and open the store
                Cursor.lockState = CursorLockMode.None;
                _storeCam.Priority = 10;
                _mainCam.Priority = 0;

                _mainUI.SetActive(false);

                // Disable player input and lock cursor
                _starterAssetsInputs.IsLocked = true;
                ItemInteract.Instance.IsBuying = true;
            }

            if (Hit.transform.tag == "Plantation")
            {
                Cursor.lockState = CursorLockMode.None;
                _inventoryUIScript.OpenInventory();
                _starterAssetsInputs.IsLocked = true;
                ItemInteract.Instance.IsPlanting = true;
            }

            if (Hit.transform.tag == "Grabbable")
            {
                if (Hit.transform.GetChild(0).tag == "Canabis")
                {
                    _inventoryScript.AddItem(_canabisSO, 1);
                }

                if (Hit.transform.GetChild(0).tag == "Mushroom")
                {
                    _inventoryScript.AddItem(_mushroomSO, 1);
                }

                Hit.transform.GetComponent<Slot>().Unlock();
                Hit.transform.tag = "Plantation";
                Destroy(Hit.transform.GetComponentInChildren<Plant>().gameObject);
            }

            if (Hit.transform.tag == "Selling")
            {
                _inventoryUIScript.OpenInventory();
                Cursor.lockState = CursorLockMode.None;
                _starterAssetsInputs.IsLocked = true;
                ItemInteract.Instance.IsSelling = true;
            }

            if (Hit.transform.tag == "Door")
            {
                StartCoroutine(_tpScript.TeleportToWarehouse());
            }
            if (Hit.transform.tag == "Scooter")
            {
                StartCoroutine(_tpScript.TeleportToAppart());
            }
        }
    }

    public void CloseStore()
    {
        _storeCam.Priority = 0;
        _mainCam.Priority = 10;
        Cursor.lockState = CursorLockMode.Locked;
        _starterAssetsInputs.IsLocked = false;
        _mainUI.SetActive(true);
        ItemInteract.Instance.IsBuying = false;
    }
}
