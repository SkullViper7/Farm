using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField] Transform _sun;
    [SerializeField] float _secondsInADay;

    void Start()
    {
        StartCoroutine(DayCycle());
    }

    IEnumerator DayCycle()
    {
        float timeForFullRotation = _secondsInADay * 60f / _sun.localEulerAngles.x;
        float degreesPerSecond = 360f / timeForFullRotation;

        while (true)
        {
            _sun.Rotate(degreesPerSecond * Time.deltaTime, 0, 0);
            yield return null;
        }
    }
}
