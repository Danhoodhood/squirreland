using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject mainMenu; // Renamed for consistency
    public GameObject levelsMenu;

    public void OpenLastLevel()
    {
        if (PlayerPrefs.HasKey("Last_lvl"))
        {
            SceneManager.LoadScene(PlayerPrefs.GetInt("Last_lvl"));
        }
        else
        {
            SceneManager.LoadScene("bar_scense_1");
            //или какая там
        }

    }

    public void newgame()
    {
        SceneManager.LoadScene("bar_scense_1");
    }
    public void OpenLevelsMenu()
    {
        mainMenu.SetActive(false);
        levelsMenu.SetActive(true);
    }
    public void Back()
    {
        levelsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }
    public void open_sc_1()
    {
        SceneManager.LoadScene("Platf_level1");
    }
    public void open_sc_2()
    {
        SceneManager.LoadScene("Heart_bit");
    }
    public void open_sc_3()
    {
        SceneManager.LoadScene("search_acorns");
    }
    public void open_sc_4()
    {
        SceneManager.LoadScene("Game");
    }
}
