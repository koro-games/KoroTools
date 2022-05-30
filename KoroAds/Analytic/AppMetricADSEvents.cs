using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KoroGames.Ads.Analytic
{
    

public interface IAdAnalytic
{
    bool ConnectionStatus { get; }

    void VideoAdsAvailable(AdType adType, string placement, AdResult result);
    void VideoAdsStarted(AdType adType, string placement, AdResult result);
    void VideoAdsWatch(AdType adType, string placement, AdResult result, AdRevenue revenueData);
}

public class AppMetricADSEvents : MonoBehaviour, IAdAnalytic
{
    public bool ConnectionStatus => Application.internetReachability != NetworkReachability.NotReachable;

    public void VideoAdsAvailable(AdType adType, string placement, AdResult result)
    {
#if !UNITY_EDITOR

        AppMetrica.Instance.ReportEvent("video_ads_available",
        new Dictionary<string, object>()
        {
            {"ad_type", adType.ToString()},
            {"placement", placement},
            {"result",result.ToString()},
            {"connection", ConnectionStatus}
        });
#endif
    }

    public void VideoAdsStarted(AdType adType, string placement, AdResult result)
    {
#if !UNITY_EDITOR

        AppMetrica.Instance.ReportEvent("video_ads_started",
        new Dictionary<string, object>()
        {
            {"ad_type", adType.ToString()},
            {"placement", placement},
            {"result",result.ToString()},
            {"connection", ConnectionStatus}
        });
#endif
    }

    public void VideoAdsWatch(AdType adType, string placement, AdResult result, AdRevenue revenuData)
    {
#if !UNITY_EDITOR

        AppMetrica.Instance.ReportEvent("video_ads_watch",
        new Dictionary<string, object>()
        {
            {"ad_type", adType.ToString()},
            {"placement", placement},
            {"result",result.ToString()},
            {"connection", ConnectionStatus}
        });

        if(revenuData.Revenue>0)
            AppMetrica.Instance.ReportRevenue(new YandexAppMetricaRevenue(revenuData.Revenue, revenuData.Currency));
#endif
    }
}

public struct AdRevenue
{
    public decimal Revenue;
    public string Currency;

    public AdRevenue(decimal revenue, string currency)
    {
        Revenue = revenue;
        Currency = currency;
    }
}


public enum AdType
{
    interstitial,
    rewarded,
    banner
}

public enum AdResult
{
    empty,
    success,
    not_available,
    waited,
    start,
    watched,
    clicked,
    canceled
}
}
