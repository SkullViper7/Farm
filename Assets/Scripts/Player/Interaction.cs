using Cinemachine;
using StarterAssets;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

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

    DepthOfField _dof;
    [SerializeField] Volume _volume;

    /// <summary>
    /// Gets necessary components on Awake
    /// </summary>
    private void Awake()
    {
        // Get component for input
        _starterAssetsInputs = GetComponent<StarterAssetsInputs>();

        // Get inventory and inventory UI scripts
        _inventoryScript = GetComponent<Inventory>();
        _inventoryUIScript = GetComponent<InventoryUI>();

        // Get teleportation script
        _tpScript = GetComponent<TP>();
    }


    /// <summary>
    /// Gets the DepthOfField component when the game starts
    /// </summary>
    private void Start()
    {
        // Try to get the DepthOfField component from the volume profile
        // If successful, assign it to the private field
        DepthOfField _tempDof;
        if (_volume.profile.TryGet<DepthOfField>(out _tempDof))
        {
            _dof = _tempDof;
        }
    }


    /// <summary>
    /// Checks if player is looking at an interactable and updates the interaction text accordingly.
    /// </summary>
    void FixedUpdate()
    {
        // Cast a ray from the player's camera to interact distance and check if it hits an interactable
        CanInteract = Physics.Raycast(_cam.transform.position, _cam.transform.forward, out Hit, _interactDistance, _interactableLayer);
        // Set the visibility of the interaction text based on if the ray hit an interactable
        _interactText.SetActive(CanInteract);

        if (CanInteract)
        {
            // Switch on the tag of the interactable to update the interaction text
            switch (Hit.transform.tag)
            {
                // If the tag is "Store", set the text to "Buy"
                case "Store":
                    _interactTextTMP.text = "Buy";
                    break;
                // If the tag is "Plantation", set the text to "Plant"
                case "Plantation":
                    _interactTextTMP.text = "Plant";
                    break;
                // If the tag is "Grabbable", set the text to "Pickup"
                case "Grabbable":
                    _interactTextTMP.text = "Pickup";
                    break;
                // If the tag is "Selling", set the text to "Sell"
                case "Selling":
                    _interactTextTMP.text = "Sell";
                    break;
                // If the tag is "Door", set the text to "Go to selling location"
                case "Door":
                    _interactTextTMP.text = "Go to selling location";
                    break;
                // If the tag is "Scooter", set the text to "Go back home"
                case "Scooter":
                    _interactTextTMP.text = "Go back home";
                    break;
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
            var interactable = Hit.transform; // Interactable transform

            // If interactable is store
            if (interactable.CompareTag("Store"))
            {
                // Unlock cursor and open the store
                Cursor.lockState = CursorLockMode.None;
                _storeCam.Priority = 10;
                _mainCam.Priority = 0;

                _mainUI.SetActive(false); // Hide main UI

                // Disable player input and lock cursor
                _starterAssetsInputs.IsLocked = true;
                ItemInteract.Instance.IsBuying = true; // Set instance IsBuying to true

                Invoke("SetDOF", 0.5f); // Set DOF after 0.5 seconds
            }

            else if (interactable.CompareTag("Plantation"))
            {
                Cursor.lockState = CursorLockMode.None;
                _inventoryUIScript.OpenInventory(); // Open inventory
                _starterAssetsInputs.IsLocked = true; // Disable player input
                ItemInteract.Instance.IsPlanting = true; // Set instance IsPlanting to true
            }

            else if (interactable.CompareTag("Grabbable"))
            {
                var item = interactable.GetChild(1); // Item child transform

                // Add item to inventory based on item tag
                if (item.CompareTag("Canabis"))
                    _inventoryScript.AddItem(_canabisSO, 1);
                else if (item.CompareTag("Mushroom"))
                    _inventoryScript.AddItem(_mushroomSO, 1);

                interactable.GetComponent<Slot>().Unlock(); // Unlock slot
                interactable.tag = "Plantation"; // Set interactable tag to Plantation
                Destroy(item.GetComponent<Plant>().gameObject); // Destroy plant gameObject
            }

            else if (interactable.CompareTag("Selling"))
            {
                _inventoryUIScript.OpenInventory(); // Open inventory
                Cursor.lockState = CursorLockMode.None;
                _starterAssetsInputs.IsLocked = true; // Disable player input
                ItemInteract.Instance.IsSelling = true; // Set instance IsSelling to true
            }

            else if (interactable.CompareTag("Door"))
                _tpScript.TeleportToWarehouse(); // Teleport to warehouse
            else if (interactable.CompareTag("Scooter"))
                _tpScript.TeleportToAppart(); // Teleport to appartment
        }
    }



    /// <summary>
    /// Sets the DepthOfField focal length to 55
    /// </summary>
    /// <remarks>
    /// Used in the interaction script to set the focal length when opening the store UI
    /// </remarks>
    void SetDOF()
    {
        _dof.focalLength.value = 55;
    }

    /// <summary>
    /// Closes the store UI and disables store interaction
    /// </summary>
    public void CloseStore()
    {
        // Set the store UI camera priority to 0 to hide it
        _storeCam.Priority = 0;
        // Set the main camera priority to 10 to show it
        _mainCam.Priority = 10;
        // Unlock the cursor
        Cursor.lockState = CursorLockMode.Locked;
        // Unlock player input
        _starterAssetsInputs.IsLocked = false;
        // Show the main UI
        _mainUI.SetActive(true);
        // Set the IsBuying property of the instance to false
        ItemInteract.Instance.IsBuying = false;
        // Set the focal length of the DepthOfField component to 0
        _dof.focalLength.value = 0;
    }
}
