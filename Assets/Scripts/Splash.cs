using UnityEngine;
using UnityEngine.SceneManagement;

public class Splash : MonoBehaviour
{
    /// <summary>
    /// Invokes the LoadMenu method after a delay of 2 seconds
    /// </summary>
    void Start()
    {
        // Invokes the LoadMenu method after a delay of 2 seconds
        Invoke("LoadMenu", 2f);
    }

    /**
     * Loads the main menu scene
     *
     * Loads the scene with the name "Menu" which is the main menu
     * scene. This is the first scene that the player sees when they
     * start the game.
     */
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE1006:Naming Styles", Justification = "Unity message")]
    void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
