using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Plant : MonoBehaviour
{
    [SerializeField] int _growthTime;

    Slot _slotScript;

    void Start()
    {
        _slotScript = GetComponentInParent<Slot>();
        StartCoroutine(_slotScript.Grow(_growthTime));
    }

    public void ShowGrewItem()
    {
        if (gameObject.tag == "Mushroom")
        {
            transform.localScale = new Vector3(0.03f, 0.03f, 0.3f);
        }
        else
        {
            transform.localScale = new Vector3(50, 50, 50);
        }
    }
}
