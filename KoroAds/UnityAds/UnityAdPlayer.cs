using System;
using UnityEngine;
using UnityEngine.Advertisements;
using KoroGames.Ads;

namespace KoroGames.Ads.UnityAds
{
    public abstract class UnityAdPlayer : IUnityAdsLoadListener
    {

        public string AdUnitID;
        public AdRequest CurrentAd { get; set; }
        public Action OnAdLoad { get; set; }
        public IAdAnalytic Analytic { get; set; }

        public abstract AdType AdPlayerType { get; }
        public bool IsLoading { get; set; }



        public bool IsAdNull() => CurrentAd == null;
        public bool IsNotMyPlacementID(string placement) => !AdUnitID.Equals(placement);

        public void OnUnityAdsAdLoaded(string placementId)
        {
            OnAdLoad?.Invoke();
            IsLoading = false;
            OnAdLoad = null;
        }
        public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
        {
            if (IsNotMyPlacementID(placementId) || IsAdNull()) return;

            Analytic.VideoAdsWatch(AdType.interstitial, CurrentAd.PlacementName, AdResult.empty, new AdRevenue(0, "USD"));

            CurrentAd.OnClose?.Invoke();
            CurrentAd.OnReward?.Invoke();

            CurrentAd = null;
            Advertisement.Load(AdUnitID, this);
            IsLoading = true;
        }

        public void OnUnityAdsShowClick(string placementId) { }

        public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
        {
            if (IsNotMyPlacementID(placementId) || IsAdNull()) return;

            Analytic.VideoAdsWatch(AdType.interstitial, CurrentAd.PlacementName, AdResult.watched, new AdRevenue(0, "USD"));

            CurrentAd.OnClose?.Invoke();
            CurrentAd.OnReward?.Invoke();

            CurrentAd = null;
            Advertisement.Load(AdUnitID, this);
            IsLoading = true;

        }



        public void OnUnityAdsShowStart(string placementId)
        {
            if (IsNotMyPlacementID(placementId) || IsAdNull()) return;
            CurrentAd.OnDisplay?.Invoke();
            IsLoading = false;
            Analytic.VideoAdsStarted(AdPlayerType, CurrentAd.PlacementName, AdResult.watched);
        }

        public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
        {
            if (IsNotMyPlacementID(placementId) || IsAdNull()) return;

            Analytic.VideoAdsWatch(AdType.interstitial, CurrentAd.PlacementName, AdResult.empty, new AdRevenue(0, "USD"));

            CurrentAd.OnClose?.Invoke();
            CurrentAd.OnReward?.Invoke();

            CurrentAd = null;
            Advertisement.Load(AdUnitID, this);
            IsLoading = true;
        }
    }


}