// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using GoogleMobileAds;
// using GoogleMobileAds.Api;
// using System;

// public class AdsManager : MonoBehaviour
// {
//     public static AdsManager instance;
//     public bool isRewardedAdReady = false;
//     int count;

//     void Awake()
//     {
//         if (instance != null && instance != this)
//         {
//             Destroy(gameObject);
//         }
//         else
//         {
//             instance = this;
           
//         }

//         // Initialize the Google Mobile Ads SDK.
//         MobileAds.Initialize((InitializationStatus initStatus) =>
//         {
//             // This callback is called once the MobileAds SDK is initialized.
//             LoadInterstitialAd();
//             LoadRewardedAd();
//         });
//     }

//     // These ad units are configured to always serve test ads.
// #if UNITY_ANDROID
//     private string _adInterstitialUnitId = "ca-app-pub-3940256099942544/1033173712";
// #elif UNITY_IPHONE
//     private string _adInterstitialUnitId = "ca-app-pub-3940256099942544/4411468910";
// #else
//     private string _adInterstitialUnitId = "unused";
// #endif

//     private InterstitialAd _interstitialAd;

//     /// <summary>
//     /// Loads the interstitial ad.
//     /// </summary>
//     public void LoadInterstitialAd()
//     {

//         // Clean up the old ad before loading a new one.
//         if (_interstitialAd != null)
//         {
//             _interstitialAd.Destroy();
//             _interstitialAd = null;
//         }

//         Debug.Log("Loading the interstitial ad.");

//         // Create the ad request.
//         var adRequest = new AdRequest();

//         // Send the request to load the ad.
//         InterstitialAd.Load(_adInterstitialUnitId, adRequest,
//             (InterstitialAd ad, LoadAdError error) =>
//             {
//                 if (error != null || ad == null)
//                 {
//                     Debug.LogError("Interstitial ad failed to load: " + error);
                    
//                     return;
//                 }

//                 Debug.Log("Interstitial ad loaded successfully.");
//                 _interstitialAd = ad;
//                 LoadRewardedAd();
//                 RegisterEventHandlers(_interstitialAd);
//             });
//     }

//     /// <summary>
//     /// Shows the interstitial ad.
//     /// </summary>
//     public void ShowInterstitialAd()
//     {
//         if (_interstitialAd != null && _interstitialAd.CanShowAd())
//         {
//             Debug.Log("Showing interstitial ad.");
//             _interstitialAd.Show();
//         }
//         else
//         {
//             Debug.LogError("Interstitial ad cannot be shown.");
//             LoadInterstitialAd();
//         }
//     }

//     private void RegisterEventHandlers(InterstitialAd interstitialAd)
//     {
//         interstitialAd.OnAdFullScreenContentClosed += () =>
//         {
//             Debug.Log("Interstitial ad closed. Reloading...");
//             LoadInterstitialAd();
//         };

//         interstitialAd.OnAdFullScreenContentFailed += (AdError error) =>
//         {
//             Debug.LogError("Interstitial ad failed to open: " + error);
//             LoadInterstitialAd();
//         };
//     }

//     // These ad units are configured to always serve test ads.
// #if UNITY_ANDROID
//     private string _adRewardedUnitId = "ca-app-pub-3940256099942544/5224354917";
// #elif UNITY_IPHONE
//     private string _adRewardedUnitId = "ca-app-pub-3940256099942544/1712485313";
// #else
//     private string _adRewardedUnitId = "unused";
// #endif

//     private RewardedAd _rewardedAd;

//     /// <summary>
//     /// Loads the rewarded ad.
//     /// </summary>
//     public void LoadRewardedAd()
//     {
//         if (_rewardedAd != null)
//         {
//             _rewardedAd.Destroy();
//             _rewardedAd = null;
//         }

//         Debug.Log("Loading the rewarded ad.");

//         // Create the ad request.
//         var adRequest = new AdRequest();

//         // Send the request to load the ad.
//         RewardedAd.Load(_adRewardedUnitId, adRequest,
//             (RewardedAd ad, LoadAdError error) =>
//             {
//                 if (error != null || ad == null)
//                 {
//                     Debug.LogError("Rewarded ad failed to load: " + error);
//                     isRewardedAdReady = false;
//                     return;
//                 }

//                 Debug.Log("Rewarded ad loaded successfully.");
//                 _rewardedAd = ad;
//                 isRewardedAdReady = true;
//                 RegisterEventHandlers(_rewardedAd);
//             });
//     }

//     public void ShowRewardedAd()
//     {
//         if (!isRewardedAdReady) return;

//         if (_rewardedAd != null && _rewardedAd.CanShowAd())
//         {
//             _rewardedAd.Show((Reward reward) =>
//             {
//                 Debug.Log($"Rewarded ad rewarded the user. Type: {reward.Type}, amount: {reward.Amount}");
//                 isRewardedAdReady = false;
//             });
//         }
//         else
//         {
//             Debug.LogError("Rewarded ad is not ready.");
//             LoadRewardedAd();
//         }
//     }

//     private void RegisterEventHandlers(RewardedAd ad)
//     {
//         ad.OnAdFullScreenContentClosed += () =>
//         {
//             Debug.Log("Rewarded ad closed. Reloading...");
            
//         };

//         ad.OnAdFullScreenContentFailed += (AdError error) =>
//         {
//             Debug.LogError("Rewarded ad failed to open: " + error);
//             LoadRewardedAd();
//         };
//     }
// }
