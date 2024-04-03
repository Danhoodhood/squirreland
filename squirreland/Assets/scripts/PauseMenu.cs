using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public bool isPaused = false;
    public int sceneIndex;
    public GameObject pauseMenu;
    public AudioSource audioSource;
    public GameObject button_menu;


    public void PauseGame()
    {
        isPaused = true;
        button_menu.SetActive(false);
        pauseMenu.SetActive(true);
        audioSource.Pause();
        Time.timeScale = 0f; // Остановка времени
    }

    public void ResumeGame()
    {
        isPaused = false;
        audioSource.Play();
        button_menu.SetActive(true);
        pauseMenu.SetActive(false);
        Time.timeScale = 1f; // Возобновление времени
    }
    public void Reload_level()
    {

        GlobalScore.score = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Main_Menu()
    {

        GlobalScore.score = 0;
        SceneManager.LoadScene(sceneIndex);
        Save_progress.Save();

    }

}
