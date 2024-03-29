using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Plantation : MonoBehaviour
{
    [Header("Plants")]
    [SerializeField] GameObject _canabis;
    [SerializeField] GameObject _mushroom;

    Interaction _interactionScript;

    InventoryUI _inventoryUIScript;
    Inventory _inventoryScript;

    private void Awake()
    {
        _interactionScript = GetComponent<Interaction>();
        _inventoryUIScript = GetComponent<InventoryUI>();
        _inventoryScript = GetComponent<Inventory>();
    }

    public void PlantItem(ItemSO item)
    {
        Transform plantTransform = _interactionScript.Hit.transform;

        GameObject newPlant;
        if (item.Name == "Canabis Seed")
        {
            newPlant = Instantiate(_canabis, plantTransform.position - new Vector3(0, 0.5f, 0), Quaternion.identity);
        }
        else if (item.Name == "Mushroom Seed")
        {
            newPlant = Instantiate(_mushroom, plantTransform.position - new Vector3(0, 0.5f, 0), Quaternion.identity);
        }
        else
        {
            Debug.Log("Error: Plant item is not canabis or mushroom");
            return;
        }

        newPlant.transform.SetParent(plantTransform);

        _inventoryUIScript.CloseInventory();
        _interactionScript.Hit.transform.GetComponent<Slot>().Lock();

        _inventoryScript.RemoveItem(item, 1);   
    }
}
