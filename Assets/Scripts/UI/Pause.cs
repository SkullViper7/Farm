using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;

public class Pause : MonoBehaviour
{
    [SerializeField] GameObject _pauseMenu;

    StarterAssetsInputs _starterAssetsInputs;

    /**
     * Gets the StarterAssetsInputs component at Awake
     */
    private void Awake()
    {
        // Get the StarterAssetsInputs component
        _starterAssetsInputs = GetComponent<StarterAssetsInputs>();
    }

    /// <summary>
    /// Pauses the game by pausing time and showing the pause menu.
    /// Locks the player's movements and hides the cursor.
    /// </summary>
    /// <param name="value">Input value from InputSystem</param>
    public void OnPause(InputValue value)
    {
        // Pause time
        Time.timeScale = 0;

        // Show the pause menu and enable the player's movements
        _pauseMenu.SetActive(true);
        _starterAssetsInputs.IsLocked = true;

        // Hide the cursor
        Cursor.lockState = CursorLockMode.None;
    }

    /// <summary>
    /// Resumes the game by unpausing time and hiding the pause menu.
    /// Unlocks the player's movements and locks the cursor.
    /// </summary>
    public void ResumeGame()
    {
        // Resume time
        Time.timeScale = 1;

        // Hide the pause menu and disable the player's movements
        _pauseMenu.SetActive(false);
        _starterAssetsInputs.IsLocked = false;

        // Lock the cursor
        Cursor.lockState = CursorLockMode.Locked;
    }
}
