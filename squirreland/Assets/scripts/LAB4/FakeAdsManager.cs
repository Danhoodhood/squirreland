using UnityEngine;
using SuperMobileAds;

public class AdsManager : MonoBehaviour
{
    public static AdsManager Instance;

    private SuperMobileAdsInterstitial interstitial;
    private SuperMobileAdsRewarded rewarded;
    private SuperMobileAdsBanner banner;

    public int rewardCoins = 1;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeAds();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeAds()
    {
        Debug.Log("[AdsManager] Инициализация рекламы...");

        interstitial = new SuperMobileAdsInterstitial();
        interstitial.Initialize("Interstitial_ID");
        interstitial.Load();

        interstitial.onAdLoaded += () => DebugLogAndTrack("Interstitial Loaded");
        interstitial.onAdShown += () => DebugLogAndTrack("Interstitial Shown");
        interstitial.onAdClicked += () => DebugLogAndTrack("Interstitial Clicked");
        interstitial.onAdDismissed += () => DebugLogAndTrack("Interstitial Dismissed");
        interstitial.onAdFailedToLoad += () => DebugLogAndTrack("Interstitial Failed to Load");

        rewarded = new SuperMobileAdsRewarded();
        rewarded.Initialize("Rewarded_ID");
        rewarded.Load();

        rewarded.onAdLoaded += () => DebugLogAndTrack("Rewarded Loaded");
        rewarded.onAdShown += () => DebugLogAndTrack("Rewarded Shown");
        rewarded.onAdRewarded += () =>
        {
            DebugLogAndTrack("Rewarded Completed — бонус выдан");
            GiveReward();
        };
        rewarded.onAdDismissed += () => DebugLogAndTrack("Rewarded Dismissed");
        rewarded.onAdClicked += () => DebugLogAndTrack("Rewarded Clicked");
        rewarded.onAdFailedToLoad += () => DebugLogAndTrack("Rewarded Failed to Load");

        banner = new SuperMobileAdsBanner();
        banner.Initialize("Banner_ID");
        banner.Load();

        banner.onAdLoaded += () => DebugLogAndTrack("Banner Loaded");
        banner.onAdFailedToLoad += () => DebugLogAndTrack("Banner Failed to Load");
    }

    // Лог + аналитика
    private void DebugLogAndTrack(string message)
    {
        Debug.Log("[AdsManager] " + message);
        AnalyticsManager.Instance?.TrackUserEvent("AdEvent", message);
    }

    public void ShowInterstitial(System.Action onClosed = null)
    {
        if (interstitial != null)
        {
            DebugLogAndTrack("Показ межстраничной рекламы");
            interstitial.onAdDismissed += () => onClosed?.Invoke();
            interstitial.Show();
        }
        else
        {
            onClosed?.Invoke();
        }
    }

    public void ShowRewarded()
    {
        DebugLogAndTrack("Попытка показа вознаграждаемой рекламы");
        rewarded?.Show();
    }

    public void ShowBanner()
    {
        DebugLogAndTrack("Показ баннера");
        banner?.Show();
    }

    private void GiveReward()
    {
        DebugLogAndTrack("Игрок получил " + rewardCoins + " монет за просмотр рекламы");
    }
}
