using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LOAD : MonoBehaviour
{
    public Slider slider;
    public GameObject load_menu;
    public int num_scene;

    public void load_please()
    {
        // ✅ Запускаем ТОЛЬКО coroutine
        StartCoroutine(LoadingScreen());
    }

    IEnumerator LoadingScreen()
    {
        load_menu.SetActive(true);

        // ✅ ОДИН вызов LoadSceneAsync
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(num_scene);

        // ❗ Не активируем сцену сразу
        asyncOperation.allowSceneActivation = false;

        while (asyncOperation.progress < 0.9f)
        {
            // ✅ Корректный расчёт прогресса
            float progress = Mathf.Clamp01(asyncOperation.progress / 0.9f);
            slider.value = progress;
            yield return null;
        }

        // ✅ Загрузка завершена — можно активировать сцену
        slider.value = 1f;
        asyncOperation.allowSceneActivation = true;
    }
}
