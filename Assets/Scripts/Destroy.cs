using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    void Start()
    {
        Invoke("Disable", 3f);
    }

    void Disable()
    {
        gameObject.SetActive(false);
    }
}
