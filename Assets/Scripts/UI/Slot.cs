using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    [SerializeField] Material _interactable;
    [SerializeField] Material _locked;
    MeshRenderer _meshRenderer;

    [SerializeField] Slider _slider;
    [SerializeField] GameObject _sliderObject;

    [SerializeField] InventoryUI _inventoryUIScript;
    Plant _plantScript;

    void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    void OnMouseOver()
    {
        if (!_inventoryUIScript.IsOpen)
        {
            _meshRenderer.enabled = true;
        }
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

    public IEnumerator Grow(int growthTime)
    {
        _slider.value = 0;
        _sliderObject.SetActive(true);

        float increment = 0.1f/growthTime;
        while(_slider.value < 1)
        {
            _slider.value += increment * Time.deltaTime;
            yield return null;
        }

        _sliderObject.SetActive(false);

        _plantScript = GetComponentInChildren<Plant>();
        _plantScript.ShowGrewItem();
        gameObject.tag = "Grabbable";
        Unlock();
    }
}
