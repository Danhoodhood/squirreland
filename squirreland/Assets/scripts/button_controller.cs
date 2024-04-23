using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject mainMenu; // Renamed for consistency
    public GameObject levelsMenu;

    public void OpenLastLevel()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("Last_lvl"));
    }

    public void newgame()
    {
        SceneManager.LoadScene(1);
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
        SceneManager.LoadScene(1);
    }
    public void open_sc_2()
    {
        SceneManager.LoadScene(2);
    }
    public void open_sc_3()
    {
        SceneManager.LoadScene(3);
    }
    public void open_sc_4()
    {
        SceneManager.LoadScene(4);
    }
}
