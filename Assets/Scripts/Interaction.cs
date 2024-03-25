using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    LayerMask _interactable;

    bool _canInteract;

    [SerializeField] GameObject _interactText;
    [SerializeField] Camera _cam;

    void FixedUpdate()
    {
        if (Physics.Raycast(_cam.transform.position, _cam.transform.forward, out RaycastHit hit, 10f, _interactable))
        {
            _canInteract = true;
            _interactText.SetActive(true);
        }
        else
        {
            _canInteract = false;
            _interactText.SetActive(false);
        }
    }
}
