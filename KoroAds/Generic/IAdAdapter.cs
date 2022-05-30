namespace KoroGames.Ads
{

    public interface IAdAdapter
    {
        void Init(IAdAnalytic adAnalytic);

        IAdRewarded AdRewarded { get; }
        IAdInterstitial AdInterstitial { get; }
        IAdBanner AdBanner { get; }
    }

    public interface IAdInterstitial
    {
        void Init();
        IAdAnalytic Analytic { get; set; }
        System.Action OnAdLoad { get; set; }
        bool TryCallInterstitial(AdRequest adRequest);
    }

    public interface IAdRewarded
    {
        void Init();
        IAdAnalytic Analytic { get; set; }
        System.Action OnAdLoad { get; set; }
        bool TryCallRewarded(AdRequest adRequest);
    }


    public interface IAdBanner
    {
        void Init();
        bool IsOpen { get; }
        void ShowBanner();
        void HideBanner();
    }

}