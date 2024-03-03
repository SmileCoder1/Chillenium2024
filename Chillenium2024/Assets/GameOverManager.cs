using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{

    [SerializeField] GameObject gameOverCanvas;
    [SerializeField] Button restart;
    [SerializeField] Button quit;

    // Start is called before the first frame update
    void Start()
    {
        restart.onClick.AddListener(restartGame);
        quit.onClick.AddListener(quitGame);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void gameOverTriggered()
    {
        Time.timeScale = 0;
        gameOverCanvas.SetActive(true);
    }

    void restartGame()
    {
        Time.timeScale = 1;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void quitGame()
    {
        Time.timeScale = 1;

        SceneManager.LoadScene("Menu");
    }
}
