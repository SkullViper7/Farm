using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    /// <summary>
    /// Loads a scene by name
    /// </summary>
    /// <param name="levelName">The name of the scene to load</param>
    public void Load(string levelName)
    {
        // Load the scene with the given name
        SceneManager.LoadScene(levelName);
    }


    /// <summary>
    /// Quits the application
    /// </summary>
    public void Quit()
    {
        // Quit the application
        Application.Quit();
    }
}
