using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class restart : MonoBehaviour
{

    public void restartlevel()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("Last_lvl"));

    }
    public void menu()
    {
        SceneManager.LoadScene("main_menu");

    }

}
