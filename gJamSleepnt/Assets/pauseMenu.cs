using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject restartMenuUI;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        pauseMenuUI.SetActive(false);
        restartMenuUI.SetActive(false);
        Score score = (Score)FindObjectOfType(typeof(Score));
        score.SetScoreToZero();
        Health health = (Health)FindObjectOfType(typeof(Health));
        for (int i = 0; i < 3; i++)
        {
            health.TakeHeal();
        }
        SceneManager.LoadScene(0);

    }

    public void restartPanelActive()
    {
        Time.timeScale = 0f;
        restartMenuUI.SetActive(true);
    }

    public void restartLevel()
    {
        Time.timeScale = 1f;
        Health health = (Health)FindObjectOfType(typeof(Health));
        for(int i = 0; i < 3; i++)
        {
            health.TakeHeal();
        }       
        restartMenuUI.SetActive(false);
        SceneManager.LoadScene(1);


    }

    public void QuitGame()
    {
        Application.Quit();
    }
}