using System;
using UnityEngine;
using UnityEngine.Advertisements;
using KoroGames.Ads;


namespace KoroGames.Ads.UnityAds
{
    public class UnityAdReward : UnityAdPlayer, IAdRewarded, IUnityAdsLoadListener, IUnityAdsShowListener
    {
        public override AdType AdPlayerType => AdType.rewarded;

        public void Init()
        {
            Advertisement.Load(AdUnitID, this);
            IsLoading = true;
        }

        public bool TryCallRewarded(AdRequest adRequest)
        {
            CurrentAd = adRequest;
            if (!IsLoading)
            {
                Advertisement.Show(AdUnitID, this);
                return true;
            }
            return false;
        }
    }
}