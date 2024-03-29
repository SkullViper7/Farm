using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;

public class TP : MonoBehaviour
{
    [SerializeField] Transform _warehouse;
    [SerializeField] Transform _appart;

    [SerializeField] GameObject _black;

    public IEnumerator TeleportToWarehouse()
    {
        _black.SetActive(true);

        yield return new WaitForSeconds(0.55f);

        transform.position = _warehouse.position;

        yield return new WaitForSeconds(1.45f);

        _black.SetActive(false);
    }

    public IEnumerator TeleportToAppart()
    {
        _black.SetActive(true);

        yield return new WaitForSeconds(0.55f);

        transform.position = _appart.position;

        yield return new WaitForSeconds(1.45f);

        _black.SetActive(false);
    }
}
