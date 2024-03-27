using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Plant : MonoBehaviour
{
    [SerializeField] Slider _slider;
    [SerializeField] GameObject _sliderObject;
    [SerializeField] float _growthTime;

    Slot _slotScript;

    void Start()
    {
        _slotScript = GetComponentInParent<Slot>();
        _slider.value = 0;
        StartCoroutine(Grow());
    }

    IEnumerator Grow()
    {
        float increment = 0.1f/_growthTime;
        while(_slider.value < 1)
        {
            _slider.value += increment * Time.deltaTime;
            yield return null;
        }

        _slotScript.Unlock();
        _sliderObject.SetActive(false);
        _slotScript.gameObject.tag = "Grabbable";
    }
}
