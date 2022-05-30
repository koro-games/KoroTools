using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

namespace KoroGames.Ads.UnityAds
{
    public class UnityAdsAdapter : MonoBehaviour, IAdAdapter, IUnityAdsInitializationListener
    {
        private const string c_gameId = "4579174";

        private const string c_rewardId = "Rewarded_Android";
        private const string c_interId = "Interstitial_Android";



        public IAdRewarded AdRewarded { get; set; }
        public IAdInterstitial AdInterstitial { get; set; }
        public IAdBanner AdBanner { get; set; }

        public void Init(IAdAnalytic adAnalytic)
        {
            if (Advertisement.isSupported)
            {
                Advertisement.Initialize(c_gameId, true, this);
            }

            AdInterstitial = new UnityAdInter() { AdUnitID = c_interId, Analytic = adAnalytic };
            AdRewarded = new UnityAdReward() { AdUnitID = c_rewardId, Analytic = adAnalytic };
        }

        public void OnInitializationComplete()
        {
            AdInterstitial.Init();
            AdRewarded.Init();
            //AdBanner.Init();
        }

        public void OnInitializationFailed(UnityAdsInitializationError error, string message)
        {

        }
    }


}