using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Gamemenu : MonoBehaviour
{
    [SerializeField]
    private GameObject pausepael;
    
    void Awake()
    {
       pausepael.SetActive(false);
       
    }
    public void PauseGame()
    {
        Time.timeScale = 0f;
        pausepael.SetActive(true);
    }
    public void ResumeGame()
    {
        Time.timeScale = 1f;
        pausepael.SetActive(false);
    }
    public void Quite()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

}
