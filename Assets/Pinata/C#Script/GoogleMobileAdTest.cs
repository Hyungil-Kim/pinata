using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using GoogleMobileAds.Api;

public static class GoogleMobileAdTest
{
   
    public static readonly string interstitial1Id = "ca-app-pub-1195551850458243/6952860588";
    public static readonly string interstitial2Id = "ca-app-pub-1195551850458243/2828377777";
    public static readonly string reward1Id = "ca-app-pub-1195551850458243/1700533901";
    public static readonly string reward2Id = "ca-app-pub-1195551850458243/8264537283";

    public static InterstitialAd interstitial;
    public static InterstitialAd interstitial2;
    public static RewardedAd rewardedAd;
    public static RewardedAd rewardedAd2;
    public static GameManager gameManager;
    private static int plusGold;
    public static bool initon;
    public static void Init()
    {
        List<string> deviceIds = new List<string>();
        deviceIds.Add("79B24A9C85DAEAD01457D44C1B601FF4");
        RequestConfiguration requestConfiguration = new RequestConfiguration
            .Builder()
            .SetTestDeviceIds(deviceIds)
            .build();
        MobileAds.SetRequestConfiguration(requestConfiguration);
        MobileAds.Initialize(initStatus => {
            RequestInterstitial();
            RequestInterstitial2();
            RequestReward();
            RequestReward2();
            initon = true;
        });
    }
    
    public static void FindTag()
	{
        gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
    }
    public static void RequestInterstitial()
    {
        if (interstitial != null)
        {
            interstitial.Destroy();
        }
        interstitial = new InterstitialAd(interstitial1Id);
        interstitial.OnAdClosed += HandleOnAdClosed;
        AdRequest request = new AdRequest.Builder().Build();
        interstitial.LoadAd(request);
    }
    public static void RequestInterstitial2()
    {
        if (interstitial2 != null)
        {
            interstitial2.Destroy();
        }
        interstitial2 = new InterstitialAd(interstitial2Id);
        interstitial2.OnAdClosed += HandleOnAdClosed2;
        AdRequest request = new AdRequest.Builder().Build();
        interstitial2.LoadAd(request);
    }
    public static void HandleOnAdClosed(object sender, EventArgs args)
    {
        RequestInterstitial();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
    public static void HandleOnAdClosed2(object sender, EventArgs args)
    {
        RequestInterstitial2();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public static void OnclickInterstitial() 
    {
        if (interstitial.IsLoaded())
        {
            interstitial.Show();
        }
    }
   
    public static void OnclickInterstitial2()
    {
        if (interstitial2.IsLoaded())
        {
            interstitial2.Show();
        }
   
    }
    public static void RequestReward()
    {
        if (rewardedAd != null)
        {
            rewardedAd.Destroy();
        }
        rewardedAd = new RewardedAd(reward1Id);
        rewardedAd.OnAdClosed += HandleRewardedOnAdClosed;
        rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;
        AdRequest request = new AdRequest.Builder().Build();
        rewardedAd.LoadAd(request);
    }
    public static void RequestReward2()
    {
        if (rewardedAd2 != null)
        {
            rewardedAd2.Destroy();
        }
        rewardedAd2 = new RewardedAd(reward2Id);
        rewardedAd2.OnAdClosed += HandleRewardedOnAdClosed2;
        rewardedAd2.OnUserEarnedReward += HandleUserEarnedReward2;
        AdRequest request = new AdRequest.Builder().Build();
        rewardedAd2.LoadAd(request);
    }
    public static void HandleRewardedOnAdClosed(object sender, EventArgs args)
    {
        RequestReward();
    }
    public static void HandleRewardedOnAdClosed2(object sender, EventArgs args)
    {
        RequestReward2();
        if (SceneManager.sceneCountInBuildSettings == SceneManager.GetActiveScene().buildIndex + 1)
        {
            SceneManager.LoadScene(0);
        }
		else
		{
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
		}
    }
    public static void OnClickReward()
    {
        if (rewardedAd.IsLoaded())
        {
            rewardedAd.Show();
        }
    
    }
    public static void OnClickReward2()
    {
        if (rewardedAd2.IsLoaded())
        {
            plusGold = gameManager.earnGold;
            rewardedAd2.Show();
        }
  
    }
    public static  void HandleUserEarnedReward(object sender, Reward args)
    {
        int amount = (int)args.Amount;
        gameManager.savegold += 50;
    }
    public static void HandleUserEarnedReward2(object sender, Reward args)
    {
        int amount = (int)args.Amount;
         gameManager.savegold += plusGold;
        gameManager.Save();
    }
}