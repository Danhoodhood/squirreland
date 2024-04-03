using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Save_progress : MonoBehaviour
{
    public static void Save()
    {
        PlayerPrefs.SetInt("Last_lvl", SceneManager.GetActiveScene().buildIndex);
        PlayerPrefs.SetInt("count_hearts", HeartSystem.health);
        PlayerPrefs.Save();
    }
}
