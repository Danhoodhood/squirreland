using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class End_game : MonoBehaviour
{
    public AudioSource audioSource;
    public TMP_Text vin_lose;
    public TMP_Text score_text;
    public int wait;
    public float minValue;
    public float maxValue;
    public Color minColor;
    public Color maxColor;
    public GameObject button_continue;
    public GameObject menu;
    public GameObject defeat;
    public int sceneIndex;
    // Start is called before the first frame update
    void Start()
    {
        Save_progress.Save();
        Time.timeScale = 1;
        StartCoroutine(SomeCoroutine());
    }
    private IEnumerator SomeCoroutine()
    {
        yield return new WaitForSeconds(wait);
        audioSource.Pause();
        menu.SetActive(true);
        score_text.text = string.Format("набрано {0} очков", GlobalScore.score);
        Time.timeScale = 0f; // Остановка времени
        if (GlobalScore.score<=minValue)
        {
            defeat.SetActive(true);

        }
        else
        {
            SceneManager.LoadScene("Platf_Level_1");
        }
        

    }
    public void Reload_level()
    {
        GlobalScore.score = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Next_level()
    {
        GlobalScore.score = 0;
        SceneManager.LoadScene(sceneIndex);

    }
}
