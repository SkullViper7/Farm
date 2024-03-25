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

    private void Awake()
    {
        _starterAssetsInputs = GetComponent<StarterAssetsInputs>();
    }

    void FixedUpdate()
    {
        CanInteract = Physics.Raycast(_cam.transform.position, _cam.transform.forward, out Hit, _interactDistance, _interactableLayer);
        _interactText.SetActive(CanInteract);
    }

    public void OnInteract(InputValue value)
    {
        if (CanInteract)
        {
            if (Hit.transform.tag == "Store")
            {                    
                Cursor.lockState = CursorLockMode.None;
                _store.SetActive(true);
                _starterAssetsInputs.IsLocked = true;
            }
        }
    }
}
