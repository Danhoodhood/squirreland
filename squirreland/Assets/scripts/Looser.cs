using UnityEngine;
using UnityEngine.SceneManagement;

public class Looser : MonoBehaviour
{
    public GameObject pauseMenu;
    public AudioSource audioSource;
    void Update()
    {
        if (timer_for_hide.startTime == 0)
        {
            HeartSystem.health = 0;
        }
        if (HeartSystem.health == 0)
        {
            pauseMenu.SetActive(true);
            audioSource.Pause();
            Time.timeScale = 0f;
        }
    }
    public void Repeat()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
