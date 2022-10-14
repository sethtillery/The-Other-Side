using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    //calls pause menu objects
    public GameObject pauseMenu;

    //bool variable to check if pause menu is on or off
    public bool isPaused;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
        isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    //loads main menu
    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }

    //quits the game
    public void QuitGame()
    {
        //Closes the application
        Application.Quit();
    }
}
