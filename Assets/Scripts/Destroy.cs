using UnityEngine;

public class Destroy : MonoBehaviour
{
    /// <summary>
    /// Invokes the Disable method on this object after a delay of 3 seconds
    /// </summary>
    void Start()
    {
        // Invoke the Disable method on this object after a delay of 3 seconds
        Invoke("Disable", 3f);
    }

    /// <summary>
    /// Disables this game object after the specified time has passed
    /// </summary>
    void Disable()
    {
        // Disable the game object after the specified time has passed
        gameObject.SetActive(false);
    }

}
