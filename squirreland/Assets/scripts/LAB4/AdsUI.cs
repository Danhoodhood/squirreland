using UnityEngine;
using UnityEngine.UI;

public class AdsUI : MonoBehaviour
{
    public Button interstitialButton;
    public Button rewardedButton;
    public Button bannerButton;

    private void Start()
    {
        // Привязываем методы к кнопкам
        interstitialButton.onClick.AddListener(ShowInterstitialAd);
        rewardedButton.onClick.AddListener(ShowRewardedAd);
        bannerButton.onClick.AddListener(ShowBannerAd);
    }

    private void ShowInterstitialAd()
    {
        if (AdsManager.Instance != null)
        {
            AdsManager.Instance.ShowInterstitial(() =>
            {
                Debug.Log("[AdsUI] Interstitial закрыт");
            });
        }
    }

    private void ShowRewardedAd()
    {
        if (AdsManager.Instance != null)
        {
            AdsManager.Instance.ShowRewarded();
        }
    }

    private void ShowBannerAd()
    {
        if (AdsManager.Instance != null)
        {
            AdsManager.Instance.ShowBanner();
        }
    }
}
