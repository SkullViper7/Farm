using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plantation : MonoBehaviour
{
    [Header("Plants")]
    [SerializeField] GameObject _corn;
    [SerializeField] GameObject _potato;

    Interaction _interactionScript;

    Inventory _inventoryScript;

    private void Awake()
    {
        _interactionScript = GetComponent<Interaction>();
        _inventoryScript = GetComponent<Inventory>();
    }

    public void PlantItem(Item item)
    {
        var plantTransform = _interactionScript.Hit.transform;
        var plantPrefab = item.Tag == "Corn" ? _corn : _potato;

        GameObject newPlant = Instantiate(plantPrefab, plantTransform.position - new Vector3(0, 0.5f, 0), Quaternion.identity);

        newPlant.transform.SetParent(plantTransform);

        _inventoryScript.CloseInventory();
        _interactionScript.Hit.transform.GetComponent<Slot>().Lock();

        _inventoryScript.RemoveItem(item);   
    }
}
