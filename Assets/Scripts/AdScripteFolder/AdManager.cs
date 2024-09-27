using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds;
using GoogleMobileAds.Api;
using System;

public class AdManager : MonoBehaviour
{
    private BannerView bannerAd;
    private InterstitialAd interstitial;
    private RewardedAd rewardedAd;

    public MainMenu mainMenu;

    public static AdManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    [Obsolete]
    private void Start()
    {

        MobileAds.Initialize(InitializationStatus => { });
        this.RequestBanner();
        RequestRewardedAd();
    }

    [Obsolete]
    private void RequestBanner()
    {
        string adUnitId = "ca-app-pub-8344458138226211/1179674829";
        this.bannerAd = new BannerView(adUnitId, AdSize.SmartBanner, AdPosition.Bottom);
        AdRequest request = new AdRequest();
        this.bannerAd.LoadAd(request);
    }

    public void RequestInterstitial()
    {
        string adUnitId = "ca-app-pub-8344458138226211/8683131454";
        if(this.interstitial != null)
        {
            this.interstitial.Destroy();
        }
        AdRequest request = new AdRequest();
        InterstitialAd.Load(adUnitId, request, (InterstitialAd ad, LoadAdError error) =>
        {
            if (error != null || ad == null)
            {
                return;
            }
            interstitial = ad;
        });
    }

    public void RequestRewardedAd()
    {
        string adUnitId = "ca-app-pub-8344458138226211/8884944456";
        if (this.rewardedAd != null)
        {
            rewardedAd.Destroy();
            rewardedAd = null;
        }

        AdRequest request = new AdRequest();

        RewardedAd.Load(adUnitId, request,(RewardedAd ad, LoadAdError error) =>
        {
            if (error != null || ad == null)
            {
                return;
            }

            rewardedAd = ad;
            RegisterEventHandlers(rewardedAd);
        });
    }

    public void ShowRewardedAd()
    {
        if (rewardedAd != null && rewardedAd.CanShowAd() && !Events.isRevived)
        {
            rewardedAd.Show((Reward reward) =>
            {
                Events.instance.OnUserRevived();
            });
        }
    }

    public void ShowInterstitialAd()
    {
        if (interstitial != null && interstitial.CanShowAd())
        {
            interstitial.Show();
        }
    }

    private void RegisterEventHandlers(RewardedAd ad)
    {
        // Raised when the ad is estimated to have earned money.
        ad.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log(String.Format("Rewarded ad paid {0} {1}.",
                adValue.Value,
                adValue.CurrencyCode));
        };
        // Raised when an impression is recorded for an ad.
        ad.OnAdImpressionRecorded += () =>
        {
            Debug.Log("Rewarded ad recorded an impression.");
        };
        // Raised when a click is recorded for an ad.
        ad.OnAdClicked += () =>
        {
            Debug.Log("Rewarded ad was clicked.");
        };
        // Raised when an ad opened full screen content.
        ad.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("Rewarded ad full screen content opened.");
        };
        // Raised when the ad closed full screen content.
        ad.OnAdFullScreenContentClosed += () =>
        {
            RequestRewardedAd();
        };
        // Raised when the ad failed to open full screen content.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            RequestRewardedAd();
        };
    }
}
