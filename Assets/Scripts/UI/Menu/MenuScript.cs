using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    [SerializeField] int sceneLevel;
 
   public void LoadFirstScene()
    {
        MusicClass.continueSong = false;
        SceneManager.LoadScene(5);
    }

    public void LoadSpecifiedScene(int level)
    {
        SceneManager.LoadScene(level);
    }

    public void LoadPracticeLevel(int level)
    {
        PlayerStats.SetPractice(true);
        SceneManager.LoadScene(level);
    }

    public void PlayAgain()
    {
        PlayerStats.ResetLevelTry();
        Score.resetScore();

        LoadFirstScene();
    }

    public void MainMenu()
    {
        PlayerStats.SetPractice(false);
        PlayerStats.ResetLevelTry();
        Score.resetScore();
        SceneManager.LoadScene(0);
    }

    public void exitTheGame()
    {
        Application.Quit();
    }

    public void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}
