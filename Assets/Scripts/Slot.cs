using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Slot : MonoBehaviour
{
    [SerializeField] Material _interactable;
    [SerializeField] Material _locked;
    MeshRenderer _meshRenderer;

    void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    void OnMouseOver()
    {
        _meshRenderer.enabled = true;
    }

    void OnMouseExit()
    {
        _meshRenderer.enabled = false;
    }

    public void Lock()
    {
        _meshRenderer.material = _locked;
        gameObject.layer = 0;
    }

    public void Unlock()
    {
        _meshRenderer.material = _interactable;
        gameObject.layer = 3;
    }
}
