using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameg : MonoBehaviour
{
    public GameObject startbutton;
    public string namelvl;

    void OnTriggerEnter2D(Collider2D col)
    {
        startbutton.SetActive(true);
    }

    void OnTriggerExit2D(Collider2D col)
    {
        startbutton.SetActive(false);
    }

    public void startgame()
    {
        // ✅ Аналитика перехода на уровень
        if (AnalyticsManager.Instance != null)
        {
            AnalyticsManager.Instance.TrackLevelEvent(
                SceneManager.GetActiveScene().buildIndex + 1
            );
        }

        // ✅ Если есть реклама — показываем, затем грузим сцену асинхронно
        if (AdsManager.Instance != null)
        {
            AdsManager.Instance.ShowInterstitial(() =>
            {
                Debug.Log("[GameG] Реклама завершена, начинаем асинхронную загрузку сцены");
                StartCoroutine(LoadLevelAsync());
            });
        }
        else
        {
            // ✅ Без рекламы — просто асинхронно грузим сцену
            StartCoroutine(LoadLevelAsync());
        }
    }

    // ✅ Асинхронная загрузка сцены (решает фризы)
    IEnumerator LoadLevelAsync()
    {
        AsyncOperation load = SceneManager.LoadSceneAsync(namelvl);

        // Отключаем автоматическую активацию
        load.allowSceneActivation = false;

        // Пока сцена грузится (до 90%)
        while (load.progress < 0.9f)
        {
            // Здесь можно добавить loading screen
            yield return null;
        }

        // Активируем сцену
        load.allowSceneActivation = true;
    }

    public void ShowRewardAds()
    {
        if (AdsManager.Instance != null)
        {
            AdsManager.Instance.ShowRewarded();
        }
    }

    public void ShowBannerAds()
    {
        if (AdsManager.Instance != null)
        {
            AdsManager.Instance.ShowBanner();
        }
    }
}
