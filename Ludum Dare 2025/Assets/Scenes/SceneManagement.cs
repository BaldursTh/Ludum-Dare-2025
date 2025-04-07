using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("TheDescent");
    }

    public void GoToTitle() {
        SceneManager.LoadScene("TitleScreen");
    }
    public void EndGame()
    {
        Application.Quit();
    }
}
