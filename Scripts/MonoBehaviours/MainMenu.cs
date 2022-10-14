using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void Start(){
        Time.timeScale = 1f;
    }

    public void PlayGame()
    {
        //Loads the next scene in sequence
        SceneManager.LoadScene(2);
    }

    //loads main menu
    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    //loads instruction menu
    public void loadInstructions()
    {
        SceneManager.LoadScene(1);
    }

    //quits the game
    public void QuitGame()
    {
        //Closes the application
        Application.Quit();
    }
}
