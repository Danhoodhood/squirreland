using System.Collections;
using UnityEngine;
using SuperMobileAds; // пространство имён SDK
using FakeAnalytics; // Аналитика

public class AdsManager : MonoBehaviour
{
    public static AdsManager Instance;

    private SuperMobileAdsInterstitial interstitial;
    private SuperMobileAdsRewarded rewarded;
    private SuperMobileAdsBanner banner;

    public int rewardCoins = 1; // бонус за просмотр Rewarded

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // ------------------------------
            // Межстраничная реклама
            // ------------------------------
            interstitial = new SuperMobileAdsInterstitial();
            interstitial.Initialize("Interstitial_ID");
            interstitial.Load();

            interstitial.onAdLoaded += () => Debug.Log("[AdsManager] Interstitial Loaded");

            // ? ДОБАВЛЕНО: ставим игру на паузу при показе рекламы
            interstitial.onAdShown += () =>
            {
                Debug.Log("[AdsManager] Interstitial Shown");
                PauseGame(true); // ? НОВОЕ
                AnalyticsManager.Instance.TrackUserEvent("InterstitialShown");
            };

            // ? ДОБАВЛЕНО: снимаем игру с паузы после закрытия рекламы
            interstitial.onAdDismissed += () =>
            {
                Debug.Log("[AdsManager] Interstitial Dismissed");
                PauseGame(false); // ? НОВОЕ
                AnalyticsManager.Instance.TrackUserEvent("InterstitialClosed");
            };

            interstitial.onAdClicked += () =>
            {
                Debug.Log("[AdsManager] Interstitial Clicked");
                AnalyticsManager.Instance.TrackUserEvent("InterstitialClicked");
            };

            interstitial.onAdFailedToLoad += () => Debug.Log("[AdsManager] Interstitial Failed to Load");

            // ------------------------------
            // Вознаграждаемая реклама
            // ------------------------------
            rewarded = new SuperMobileAdsRewarded();
            rewarded.Initialize("Rewarded_ID");
            rewarded.Load();

            // ? ДОБАВЛЕНО: пауза при показе Rewarded
            rewarded.onAdShown += () =>
            {
                PauseGame(true); // ? НОВОЕ
                AnalyticsManager.Instance.TrackUserEvent("RewardedShown");
            };

            rewarded.onAdRewarded += () =>
            {
                GiveReward();
                AnalyticsManager.Instance.TrackUserEvent("RewardedCompleted", rewardCoins.ToString());
            };

            // ? ДОБАВЛЕНО: снятие паузы после закрытия Rewarded
            rewarded.onAdDismissed += () =>
            {
                PauseGame(false); // ? НОВОЕ
                AnalyticsManager.Instance.TrackUserEvent("RewardedClosed");
            };

            rewarded.onAdClicked += () => AnalyticsManager.Instance.TrackUserEvent("RewardedClicked");
            rewarded.onAdFailedToLoad += () => Debug.Log("[AdsManager] Rewarded Failed to Load");

            // ------------------------------
            // Баннер
            // ------------------------------
            banner = new SuperMobileAdsBanner();
            banner.Initialize("Banner_ID");
            banner.Load();

            banner.onAdLoaded += () =>
            {
                Debug.Log("[AdsManager] Banner Loaded");
                AnalyticsManager.Instance.TrackUserEvent("BannerLoaded");
            };

            banner.onAdFailedToLoad += () =>
                Debug.Log("[AdsManager] Banner Failed to Load");
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ShowInterstitial(System.Action callback = null)
    {
        if (interstitial != null)
        {
            interstitial.Show();
            callback?.Invoke();
        }
    }

    public void ShowRewarded()
    {
        if (rewarded != null)
        {
            rewarded.Show();
        }
    }

    public void ShowBanner()
    {
        if (banner != null)
        {
            banner.Show();
        }
    }

    private void GiveReward()
    {
        Debug.Log("[AdsManager] Игрок получил " + rewardCoins + " монету(ы) за просмотр рекламы!");
        AnalyticsManager.Instance.TrackUserEvent("RewardCollected", rewardCoins.ToString());
    }

    // --------------------------------------------
    // НОВЫЙ МЕТОД: ставит/снимает игру с паузы
    // --------------------------------------------
    private void PauseGame(bool pause)
    {
        Time.timeScale = pause ? 0 : 1;
        Debug.Log("[AdsManager] Game paused: " + pause);
    }
}
