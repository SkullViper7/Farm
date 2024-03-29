using System.Collections;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField] Transform _sun;
    [SerializeField] float _secondsInADay;

    /// <summary>
    /// Starts the day cycle coroutine which rotates the sun over time.
    /// </summary>
    void Start()
    {
        // Start the day cycle coroutine which rotates the sun over time
        StartCoroutine(DayCycle());
    }

    /// <summary>
    /// Coroutine that rotates the sun around the Y axis over time.
    /// </summary>
    /// <returns>Returns an enumerator object to be used in a coroutine.</returns>
    IEnumerator DayCycle()
    {
        // Calculate the time it takes for the sun to make a full rotation
        float timeForFullRotation = _secondsInADay * 60f / _sun.localEulerAngles.x;

        // Calculate the degrees per second that the sun should rotate
        float degreesPerSecond = 360f / timeForFullRotation;

        // Loop indefinitely
        while (true)
        {
            // Rotate the sun
            _sun.Rotate(degreesPerSecond * Time.deltaTime, 0, 0);

            // Return the enumerator object to be used in a coroutine
            yield return null;
        }
    }

}
