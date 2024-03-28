using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Plantation : MonoBehaviour
{
    [Header("Plants")]
    [SerializeField] GameObject _canabis;
    [SerializeField] GameObject _msuhroom;

    [SerializeField] List<Button> _slots;

    Interaction _interactionScript;

    Inventory _inventoryScript;

    private void Awake()
    {
        _interactionScript = GetComponent<Interaction>();
        _inventoryScript = GetComponent<Inventory>();
    }

    public void ChangeClick()
    {
        foreach (var slot in _slots)
        {
            slot.onClick.RemoveAllListeners();
            slot.onClick.AddListener(delegate{PlantItem(slot.GetComponent<Item>());});
        }
    }

    public void PlantItem(Item item)
    {
        var plantTransform = _interactionScript.Hit.transform;

        GameObject newPlant;
        if (item.name == "Canabis")
        {
            newPlant = Instantiate(_canabis, plantTransform.position - new Vector3(0, 0.5f, 0), Quaternion.identity);
        }
        else if (item.name == "Mushroom")
        {
            newPlant = Instantiate(_msuhroom, plantTransform.position - new Vector3(0, 0.5f, 0), Quaternion.identity);
        }
        else
        {
            Debug.Log("Error: Plant item is not canabis or mushroom");
            return;
        }

        newPlant.transform.SetParent(plantTransform);

        _inventoryScript.CloseInventory();
        _interactionScript.Hit.transform.GetComponent<Slot>().Lock();

        _inventoryScript.RemoveItem(item, 1);   
    }
}
