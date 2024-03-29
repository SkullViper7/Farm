using UnityEngine;

public class Plant : MonoBehaviour
{
    [SerializeField] int _growthTime;

    Slot _slotScript;

    /// <summary>
    /// Start this instance.
    /// </summary>
    /// <remarks>
    /// Gets the parent slot script, Starts the growth of the plant in the slot
    /// </remarks>
    void Start()
    {
        // Get the parent slot script
        _slotScript = GetComponentInParent<Slot>();

        // Start the growth of the plant in the slot
        StartCoroutine(_slotScript.Grow(_growthTime));
    }
    
    /// <summary>
    /// Scales the plant to the appropriate size after it has grown.
    /// </summary>
    /// <remarks>
    /// Checks if the plant is a mushroom, if so scales it to a small size, otherwise scales it to a large size.
    /// </remarks>
    public void ShowGrewItem()
    {
        if (gameObject.tag == "Mushroom")
        {
            transform.localScale = new Vector3(0.03f, 0.03f, 0.03f); // Small size for mushroom plants
        }
        else
        {
            transform.localScale = new Vector3(50, 50, 50); // Large size for cannabis plants
        }
    }

}
