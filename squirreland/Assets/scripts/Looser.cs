using UnityEngine;
using UnityEngine.SceneManagement;

public class Looser : MonoBehaviour
{
    public HeartSys HeartSys;
    public GameObject pauseMenu;
    public AudioSource audioSource;
    void Update()
    {
        if (HeartSys.health == 0)
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
