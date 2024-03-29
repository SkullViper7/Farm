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

    public bool IsBuying;

    public void Click(ItemSO item)
    {
        if (IsBuying)
        {
            _storeScript.ItemChoose(item);
        }

        if (IsPlanting)
        {
            _plantationScript.PlantItem(item);
        }

        if (IsSelling && item.Name == "Canabis" || item.Name == "Mushroom")
        {
            _sellingScript.ShowContextBox(item);
        }
    }
}
