using System.Collections;
using UnityEngine;
using SuperMobileAds; // пространство имён SDK

// Менеджер рекламы для всей игры
// Покрывает требования лабораторной: время размещения рекламы, реакция на события, возможность смены SDK
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
            // Межстраничная реклама (Interstitial)
            // ------------------------------
            interstitial = new SuperMobileAdsInterstitial();
            interstitial.Initialize("Interstitial_ID");
            interstitial.Load();
            Debug.Log("[AdsManager] Инициализация межстраничной рекламы");

            interstitial.onAdLoaded += () => Debug.Log("[AdsManager] Interstitial Loaded");
            interstitial.onAdShown += () => Debug.Log("[AdsManager] Interstitial Shown");
            interstitial.onAdDismissed += () => Debug.Log("[AdsManager] Interstitial Dismissed");
            interstitial.onAdClicked += () => Debug.Log("[AdsManager] Interstitial Clicked");
            interstitial.onAdFailedToLoad += () => Debug.Log("[AdsManager] Interstitial Failed to Load");

            // ------------------------------
            // Вознаграждаемая реклама (Rewarded)
            // ------------------------------
            rewarded = new SuperMobileAdsRewarded();
            rewarded.Initialize("Rewarded_ID");
            rewarded.Load();
            Debug.Log("[AdsManager] Инициализация вознаграждаемой рекламы");

            rewarded.onAdLoaded += () => Debug.Log("[AdsManager] Rewarded Loaded");
            rewarded.onAdShown += () => Debug.Log("[AdsManager] Rewarded Shown");
            rewarded.onAdRewarded += () =>
            {
                Debug.Log("[AdsManager] Rewarded Completed — выдан бонус игроку");
                GiveReward();
            };
            rewarded.onAdDismissed += () => Debug.Log("[AdsManager] Rewarded Dismissed");
            rewarded.onAdClicked += () => Debug.Log("[AdsManager] Rewarded Clicked");
            rewarded.onAdFailedToLoad += () => Debug.Log("[AdsManager] Rewarded Failed to Load");

            // ------------------------------
            // Баннерная реклама (Banner)
            // ------------------------------
            banner = new SuperMobileAdsBanner();
            banner.Initialize("Banner_ID");
            banner.Load();
            Debug.Log("[AdsManager] Инициализация баннера");

            // У баннера только onAdLoaded и onAdFailedToLoad
            banner.onAdLoaded += () => Debug.Log("[AdsManager] Banner Loaded");
            banner.onAdFailedToLoad += () => Debug.Log("[AdsManager] Banner Failed to Load");
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Показ межстраничной рекламы
    public void ShowInterstitial()
    {
        if (interstitial != null)
        {
            Debug.Log("[AdsManager] Попытка показа межстраничной рекламы");
            interstitial.Show();
        }
        else
        {
            Debug.LogWarning("[AdsManager] Interstitial не инициализирован");
        }
    }

    // Показ вознаграждаемой рекламы
    public void ShowRewarded()
    {
        if (rewarded != null)
        {
            Debug.Log("[AdsManager] Попытка показа вознаграждаемой рекламы");
            rewarded.Show();
        }
        else
        {
            Debug.LogWarning("[AdsManager] Rewarded не инициализирован");
        }
    }

    // Показ баннера
    public void ShowBanner()
    {
        if (banner != null)
        {
            Debug.Log("[AdsManager] Показ баннера");
            banner.Show();
        }
        else
        {
            Debug.LogWarning("[AdsManager] Banner не инициализирован");
        }
    }

    // Метод для выдачи бонуса за просмотр рекламы
    private void GiveReward()
    {
        Debug.Log("[AdsManager] Игрок получил " + rewardCoins + " монету(ы) за просмотр рекламы!");
        // Здесь можно добавить логику начисления в игровом менеджере
    }
}
