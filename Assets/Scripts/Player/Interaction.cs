using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interaction : MonoBehaviour
{
    [SerializeField] LayerMask _interactableLayer;

    public bool CanInteract;

    [SerializeField] float _interactDistance = 10f;

    [SerializeField] GameObject _interactText;
    [SerializeField] Camera _cam;

    [Header("Interactables")]
    [SerializeField] GameObject _store;

    public RaycastHit Hit;

    StarterAssetsInputs _starterAssetsInputs;
    Inventory _inventoryScript;
    [SerializeField] GameObject _itemPrefab;
    [SerializeField] ItemSO _canabisSO;
    [SerializeField] ItemSO _mushroomSO;

    private void Awake()
    {
        _starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        _inventoryScript = GetComponent<Inventory>();
    }

    void FixedUpdate()
    {
        CanInteract = Physics.Raycast(_cam.transform.position, _cam.transform.forward, out Hit, _interactDistance, _interactableLayer);
        _interactText.SetActive(CanInteract);
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
                _store.SetActive(true);

                // Disable player input and lock cursor
                _starterAssetsInputs.IsLocked = true;
            }

            if (Hit.transform.tag == "Plantation")
            {
                Cursor.lockState = CursorLockMode.None;
                _inventoryScript.OpenInventory();
                _starterAssetsInputs.IsLocked = true;
                ItemInteract.Instance.IsPlanting = true;
            }

            if (Hit.transform.tag == "Grabbable")
            {
                GameObject item = Instantiate(_itemPrefab, _inventoryScript.GetFirstAvailableSlot().transform);
                _inventoryScript.AddItem(item, 1);

                if (Hit.transform.GetChild(0).tag == "Canabis")
                {
                    item.GetComponent<Item>().ItemData = _canabisSO;
                }

                if (Hit.transform.GetChild(0).tag == "Mushroom")
                {
                    item.GetComponent<Item>().ItemData = _mushroomSO;
                }

                Hit.transform.GetComponent<Slot>().Unlock();
                Hit.transform.tag = "Plantation";
                Destroy(Hit.transform.GetComponentInChildren<Plant>().gameObject);
            }

            if (Hit.transform.tag == "Selling")
            {
                _inventoryScript.OpenInventory();
                Cursor.lockState = CursorLockMode.None;
                _starterAssetsInputs.IsLocked = true;
                ItemInteract.Instance.IsSelling = true;
            }
        }
    }
}
