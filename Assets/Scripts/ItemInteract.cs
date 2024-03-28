using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInteract : MonoBehaviour
{
    private static ItemInteract _instance;
    public static ItemInteract Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<ItemInteract>();

                if (_instance == null)
                {
                    Debug.LogError("No ItemInteract Instance found in the scene");
                }
            }

            return _instance;
        }
    }

    [SerializeField] Store _storeScript;
    [SerializeField] Inventory _inventoryScript;
    [SerializeField] Plantation _plantationScript;
    [SerializeField] Selling _sellingScript;

    public bool IDCreated;

    public bool IsPlanting;
    public bool IsSelling;

    public void Click(GameObject item)
    {
        if (item.transform.parent.tag == "StoreSlot")
        {
            _storeScript.ItemChoose(item);
        }

        if (item.transform.parent.tag == "InventorySlot" && IsPlanting)
        {
            Debug.Log($"clicked {_inventoryScript.Items[0].GetComponent<Item>().ID}");
            _plantationScript.PlantItem(item);
        }

        if (item.transform.parent.tag == "InventorySlot" && IsSelling)
        {
            _sellingScript.ShowContextBox(item);
        }
    }
}
