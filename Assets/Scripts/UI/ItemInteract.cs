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

    /// <summary>
    /// Handles the player's click on an item
    /// </summary>
    /// <param name="item">The item being clicked</param>
    public void Click(ItemSO item)
    {
        // If the player is buying from the store, choose the item to buy
        if (IsBuying)
        {
            _storeScript.ItemChoose(item);
        }

        // If the player is planting, plant the item
        else if (IsPlanting)
        {
            _plantationScript.PlantItem(item);
        }

        // If the player is selling and the item is a canabis or mushroom, show the selling context box
        else if (IsSelling && (item.Name == "Canabis" || item.Name == "Mushroom"))
        {
            _sellingScript.ShowContextBox(item);
        }
    }

}
