using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LOAD : MonoBehaviour
{
    public Slider slider;
    public GameObject load_menu;
    public int num_scene;
    // Start is called before the first frame update
    public void load_please()
    {
        SceneManager.LoadSceneAsync(num_scene);
        StartCoroutine(LoadingScreanOnFade());
    }
    IEnumerator LoadingScreanOnFade()
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(num_scene);
        load_menu.SetActive(true);
        while (!asyncOperation.isDone)
        {
            float progress = Mathf.Clamp01(asyncOperation.progress / .9f);
            slider.value = progress;
            yield return null;
        }

    }
}
