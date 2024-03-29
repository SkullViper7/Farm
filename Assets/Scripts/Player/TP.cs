using System.Collections;
using UnityEngine;

public class TP : MonoBehaviour
{
    [SerializeField] Transform _warehouse;
    [SerializeField] Transform _appart;

    [SerializeField] GameObject _black;

    /// <summary>
    /// Teleports the player to the warehouse.
    /// </summary>
    /// <returns>The coroutine.</returns>
    public IEnumerator TeleportToWarehouse()
    {
        // Show black screen
        _black.SetActive(true);

        // Wait for 0.55 seconds
        yield return new WaitForSeconds(0.55f);

        // Teleport player to the warehouse
        transform.position = _warehouse.position;

        // Wait for 1.45 seconds
        yield return new WaitForSeconds(1.45f);

        // Hide black screen
        _black.SetActive(false);
    }

    /// <summary>
    /// Teleports the player to the apartment.
    /// </summary>
    /// <returns>The coroutine.</returns>
    public IEnumerator TeleportToAppart()
    {
        // Show black screen
        _black.SetActive(true);

        // Wait for 0.55 seconds
        yield return new WaitForSeconds(0.55f);

        // Teleport player to the apartment
        transform.position = _appart.position;

        // Wait for 1.45 seconds
        yield return new WaitForSeconds(1.45f);

        // Hide black screen
        _black.SetActive(false);
    }
}
