using System;
using UnityEngine;
using UnityEngine.Advertisements;

namespace KoroGames.Ads.UnityAds
{


    public class UnityAdInter : UnityAdPlayer, IAdInterstitial, IUnityAdsShowListener
    {
        public override AdType AdPlayerType => AdType.interstitial;

        public void Init()
        {
            Advertisement.Load(AdUnitID, this);
            IsLoading = true;
        }

        public bool TryCallInterstitial(AdRequest adRequest)
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