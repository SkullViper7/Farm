using UnityEngine;

public class Plantation : MonoBehaviour
{
    [Header("Plants")]
    [SerializeField] GameObject _canabis;
    [SerializeField] GameObject _mushroom;

    Interaction _interactionScript;

    InventoryUI _inventoryUIScript;
    Inventory _inventoryScript;

    /// <summary>
    /// Gets necessary components on Awake
    /// </summary>
    private void Awake()
    {
        // Get the Interaction script for casting raycasts
        _interactionScript = GetComponent<Interaction>();

        // Get the InventoryUI and Inventory scripts
        _inventoryUIScript = GetComponent<InventoryUI>();
        _inventoryScript = GetComponent<Inventory>();
    }


    /// <summary>
    /// Instantiates a new plant at the user's current location and removes one item from their inventory
    /// </summary>
    /// <param name="item">The item being planted</param>
    public void PlantItem(ItemSO item)
    {
        // Get the transform of the plant slot that the user is currently looking at
        Transform plantTransform = _interactionScript.Hit.transform;

        // Instantiate the appropriate seed prefab at the user's location
        GameObject newPlant;
        if (item.Name == "Canabis Seed") // If the item is a canabis seed
        {
            newPlant = Instantiate(_canabis, plantTransform.position - new Vector3(0, 0.2f, 0), Quaternion.identity);
        }
        else if (item.Name == "Mushroom Seed") // If the item is a mushroom seed
        {
            newPlant = Instantiate(_mushroom, plantTransform.position - new Vector3(0, 0.2f, 0), Quaternion.identity);
        }
        else // If the item is not a canabis or mushroom seed
        {
            Debug.LogWarning("Error: Plant item is not canabis or mushroom");
            return;
        }

        // Set the new plant as a child of the plant slot
        newPlant.transform.SetParent(plantTransform);

        // Close the inventory UI and lock the plant slot
        _inventoryUIScript.CloseInventory();
        _interactionScript.Hit.transform.GetComponent<Slot>().Lock();

        // Remove one of the item from the player's inventory
        _inventoryScript.RemoveItem(item, 1);
    }
}
