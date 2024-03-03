using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenGame : MonoBehaviour
{
    public void OpenPLay() {
        SceneManager.LoadScene("RopeDev");
    }

    public void quitGame()
    {
        Debug.Log("quit");
        Application.Quit();
    }
}
