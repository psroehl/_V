using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{
    public void PlayGame ()
    {
        SceneManager.LoadScene("Map_1");
    }
    public void QuitGame()
    {
        SceneManager.LoadScene("StartMenuA");
        Application.Quit();

    }
}
