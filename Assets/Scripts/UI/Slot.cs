using System.Collections;
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

    /// <summary>
    /// Gets the MeshRenderer component on Awake
    /// </summary>
    void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        // Get the mesh renderer component of this slot
    }

    /// <summary>
    /// Enables the mesh renderer if the inventory UI is not open
    /// </summary>
    void OnMouseOver()
    {
        // If the inventory UI is not open, enable the mesh renderer
        if (!_inventoryUIScript.IsOpen)
        {
            _meshRenderer.enabled = true;
        }
    }

    /**
     * Disables the mesh renderer when the mouse exits this slot
     */
    void OnMouseExit()
    {
        // Disable the mesh renderer of this slot when the mouse exits
        _meshRenderer.enabled = false;
    }

    /// <summary>
    /// Locks the slot by changing its material and layer
    /// </summary>
    public void Lock()
    {
        // Set the material to _locked and disable interaction
        _meshRenderer.material = _locked;
        // Set the layer to 0 to prevent the player from interacting with the slot
        gameObject.layer = 0;
    }


    /// <summary>
    /// Unlocks the slot by changing its material and layer
    /// </summary>
    public void Unlock()
    {
        // Set the material to _interactable and enable interaction
        _meshRenderer.material = _interactable;
        // Set the layer to 3 to allow the player to interact with the slot
        gameObject.layer = 3;
    }

    /// <summary>
    /// Grow the plant in the slot
    /// </summary>
    /// <param name="growthTime">Time to grow</param>
    /// <returns>Coroutine</returns>
    public IEnumerator Grow(int growthTime)
    {
        // Set the slider value to 0 and enable it
        _slider.value = 0;
        _sliderObject.SetActive(true);

        // Set the increment to grow the slider
        float increment = 0.1f / growthTime;

        // While the slider value is less than 1 grow the slider
        while (_slider.value < 1)
        {
            _slider.value += increment * Time.deltaTime;
            yield return null;
        }

        // Disable the slider and show the grown item
        _sliderObject.SetActive(false);
        _plantScript = GetComponentInChildren<Plant>();
        _plantScript.ShowGrewItem();

        // Set the tag of the slot to grabbable
        gameObject.tag = "Grabbable";

        // Unlock the slot
        Unlock();
    }
}
