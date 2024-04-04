using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadingScreenManager : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider loadingBar;
    public static LoadingScreenManager Instance; // Статическая ссылка на экземпляр

    private void Awake()
    {
        // Проверяем, что экземпляр еще не был установлен
        if (Instance == null)
        {
            Instance = this; // Устанавливаем экземпляр текущего объекта
            DontDestroyOnLoad(gameObject); // Не уничтожать при загрузке новой сцены
        }
        else
        {
            Destroy(gameObject); // Уничтожаем дублирующийся объект
        }
    }

    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadAsyncScene(sceneName));
    }

    IEnumerator LoadAsyncScene(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        loadingScreen.SetActive(true);

        while (!asyncLoad.isDone)
        {
            float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f); // Нормализуем прогресс загрузки
            loadingBar.value = progress;

            yield return null;
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        loadingScreen.SetActive(false);
    }
}
