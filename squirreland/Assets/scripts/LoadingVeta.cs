using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LoadingVeta : MonoBehaviour
{
    public int sceneToLoad;
    public GameObject loadScreen;
    public GameObject loadMenu;

    public void Load()
    {
        loadMenu.SetActive(false);
        loadScreen.SetActive(true);
        SceneManager.LoadScene(sceneToLoad);

    }
}
