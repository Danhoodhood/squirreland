using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

// Скрипт для перехода между сценами с рекламой
public class gameg : MonoBehaviour
{
    public GameObject startbutton;  // Кнопка старта уровня
    public string namelvl;          // Имя следующей сцены
    public GameObject rewardButton; // Кнопка для Rewarded Ads

    void OnTriggerEnter2D(Collider2D col)
    {
        startbutton.SetActive(true);
        rewardButton.SetActive(true);
    }

    void OnTriggerExit2D(Collider2D col)
    {
        startbutton.SetActive(false);
        rewardButton.SetActive(false);
    }

    // Переход между сценами с межстраничной рекламой
    public void startgame()
    {
        StartCoroutine(ShowAdThenLoad());
    }

    private IEnumerator ShowAdThenLoad()
    {
        if (AdsManager.Instance != null)
        {
            AdsManager.Instance.ShowInterstitial();
            yield return new WaitForSeconds(3f); // ждем, пока реклама покажется
        }

        SceneManager.LoadScene(namelvl);
    }

    // Кнопка для вознаграждаемой рекламы
    public void ShowRewardedAd()
    {
        if (AdsManager.Instance != null)
        {
            AdsManager.Instance.ShowRewarded();
        }
    }
}
