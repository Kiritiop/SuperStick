using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("LevelSelection");
    }

    public void Arena1()
    {
        SceneManager.LoadScene("Arena1");
        Time.timeScale = 1f;
    }

    public void Arena2()
    {
        SceneManager.LoadScene("Arena2");
        Time.timeScale = 1f;
    }

    public void Locked()
    {
       UIManager.instance.ShowMessage("WIP, level not created yet", 2.5f);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
