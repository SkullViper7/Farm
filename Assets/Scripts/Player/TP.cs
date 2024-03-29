using UnityEngine;
using System.Collections;

public class TP : MonoBehaviour
{
    [SerializeField] Transform warehouse, appart;
    [SerializeField] GameObject black;

    /// <summary>
    /// Teleports the player to the warehouse.
    /// </summary>
    public void TeleportToWarehouse() => Teleport(warehouse);

    /// <summary>
    /// Teleports the player to the apartment.
    /// </summary>
    public void TeleportToAppart() => Teleport(appart);

    /// <summary>
    /// Teleports the player to the given destination.
    /// </summary>
    /// <param name="destination">The destination transform to teleport to.</param>
    void Teleport(Transform destination)
    {
        // Activate black screen to fade out
        black.SetActive(true);

        // Start coroutine to wait and teleport
        StartCoroutine(WaitAndTeleport(destination));
    }

    /// <summary>
    /// Waits for a short period of time before teleporting to the given destination.
    /// </summary>
    /// <param name="destination">The destination transform to teleport to.</param>
    /// <returns>Coroutine</returns>
    IEnumerator WaitAndTeleport(Transform destination)
    {
        // Wait for half a second (to give time for the black screen to fade out)
        yield return new WaitForSeconds(0.55f);

        // Teleport the player to the destination
        transform.position = destination.position;

        // Wait for almost two seconds (to give time for the animation to finish)
        yield return new WaitForSeconds(1.45f);

        // Deactivate the black screen
        black.SetActive(false);
    }
}
