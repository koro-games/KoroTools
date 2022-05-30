using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace KoroGames.Ads.UnityAds
{
    public class AdsWork : MonoBehaviour
    {
        [SerializeField] private GameObject _loadingScreen;
        private IAdAdapter _adAdapter;
        private IAdInterstitial _adInterstitial;
        private IAdRewarded _adRewarded;
        private IAdBanner _adBanner;
        private IAdAnalytic _adAnalytic;
        private AdRequest _endLevelAd;


        [Range(0, 1f)] public float EndLevelAdsProbability;
        public bool UseInterstitial;
        public bool UseRewarded;
        public bool UseBanner;

        public bool NoAdOffer { get => PlayerPrefs.GetInt("NoAdOffer") == 1; set => PlayerPrefs.SetInt("NoAdOffer", value ? 1 : 0); }

        public static AdsWork Manager { get; private set; }

        public AdRequest CurrentAd
        {
            get => currentAd;
            set
            {
                if (currentAd == null || value == null)
                    currentAd = value;
                else
                    throw new Exception("Current ad request not null");
            }
        }
        private AdRequest currentAd;

        private void Awake()
        {

            if (PlayerPrefs.GetInt("NoAds") == 1)
            {
                OnActiveNoAD();
            }

            if (Manager != this && Manager != null)
            {
                Destroy(gameObject);
                return;
            }
            Manager = this;
            DontDestroyOnLoad(gameObject);

            _endLevelAd = new AdRequest("end_level_interstitial") { OnDisplay = () => _loadingScreen.SetActive(false) };

            _adAnalytic = GetComponent<IAdAnalytic>();
            _adAdapter = gameObject.AddComponent<UnityAdsAdapter>();
            _adAdapter.Init(_adAnalytic);

            _adInterstitial = _adAdapter.AdInterstitial;
            _adRewarded = _adAdapter.AdRewarded;
            _adBanner = _adAdapter.AdBanner;
        }

        public void CallEndLevelAd(Action OnClose)
        {
            if (UnityEngine.Random.value >= EndLevelAdsProbability)
            {
                OnClose?.Invoke();
                return;
            }

            _endLevelAd.OnClose = OnClose;
            _endLevelAd.OnClose += () => CurrentAd = null;
            _endLevelAd.OnClose += _endLevelAd.OnClose = null;
            _endLevelAd.OnNotWaited = _endLevelAd.OnClose;
            CallInterstitial(_endLevelAd);
        }

        public void CallInterstitial(AdRequest request)
        {
            if (CurrentAd != null) return;

            CurrentAd = request;
            if (!UseInterstitial || NoAdOffer)
            {
                request.OnClose.Invoke();
                CurrentAd = null;
                return;
            }

            if (!_adInterstitial.TryCallInterstitial(request))
            {
                _adAnalytic.VideoAdsAvailable(AdType.interstitial, request.PlacementName, AdResult.not_available);
                _loadingScreen.SetActive(true);
                _adInterstitial.OnAdLoad = () =>
                {
                    _adAnalytic.VideoAdsAvailable(AdType.interstitial, request.PlacementName, AdResult.waited);
                    _adInterstitial.TryCallInterstitial(request);
                    _loadingScreen.SetActive(false);
                };
                return;
            }
            _adAnalytic.VideoAdsAvailable(AdType.interstitial, request.PlacementName, AdResult.success);

        }

        public void CallReward(AdRequest request)
        {
            if (CurrentAd != null) return;

            CurrentAd = request;
            if (!UseRewarded)
            {
                request.OnClose?.Invoke();
                request.OnReward?.Invoke();
                CurrentAd = null;
                return;
            }
            request.OnClose += () => CurrentAd = null;

            if (!_adRewarded.TryCallRewarded(request))
            {
                _adAnalytic.VideoAdsAvailable(AdType.interstitial, request.PlacementName, AdResult.not_available);
                _loadingScreen.SetActive(true);
                _adRewarded.OnAdLoad = () =>
                {
                    _adAnalytic.VideoAdsAvailable(AdType.rewarded, request.PlacementName, AdResult.waited);
                    _adRewarded.TryCallRewarded(request);
                    _loadingScreen.SetActive(false);
                };
                return;
            }
            _adAnalytic.VideoAdsAvailable(AdType.rewarded, request.PlacementName, AdResult.success);
        }

        public void CallCloseAdsLoad()
        {
            if (CurrentAd != null && CurrentAd.OnNotWaited != null) CurrentAd.OnNotWaited();

            _adInterstitial.OnAdLoad = null;
            _adRewarded.OnAdLoad = null;
            _loadingScreen.SetActive(false);
            CurrentAd = null;
        }

        public void SetBannerStatus(bool status)
        {
            if (status && UseBanner && !NoAdOffer)
            {
                if (!_adBanner.IsOpen)
                    _adBanner.ShowBanner();
            }
            else
            {
                if (_adBanner.IsOpen)
                    _adBanner.HideBanner();
            }
        }

        public void OnActiveNoAD()
        {
            UseBanner = false;
            UseInterstitial = false;
        }
    }
}