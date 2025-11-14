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
        // Отправляем событие перехода на уровень
        AnalyticsManager.Instance.TrackLevelEvent(SceneManager.GetActiveScene().buildIndex + 1);

        if (AdsManager.Instance != null)
        {
            AdsManager.Instance.ShowInterstitial(() =>
            {
                Debug.Log("[GameG] Реклама завершена, загружаем сцену: " + namelvl);
                SceneManager.LoadScene(namelvl);
            });
        }
        else
        {
            SceneManager.LoadScene(namelvl);
        }
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
