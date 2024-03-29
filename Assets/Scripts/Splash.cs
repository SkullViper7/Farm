using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Splash : MonoBehaviour
{
    void Start()
    {
        Invoke("LoadMenu", 2);
    }

    void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
