using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sitecore.Feature.Demo.Pipelines
{
    using System.Net;
    using Sitecore.Analytics;
    using Sitecore.Analytics.Model;
    using Sitecore.Analytics.Pipelines.ParseReferrer;
    using Sitecore.Feature.Demo.Controllers;
    using Sitecore.Feature.Demo.Models;
    using Sitecore.Feature.Demo.Services;
    using Sitecore.Foundation.SitecoreExtensions.Extensions;
    using Sitecore.Pipelines;

    public class FakeTrackerData
    {
        public DemoStateService DemoStateService { get; }

        public FakeTrackerData(DemoStateService demoStateService)
        {
            this.DemoStateService = demoStateService;
        }

        private string FAKE_TRACKER_DATA = "Sitecore.Feature.Demo.FakeTrackerData";

        public void GetFakeTrackerData(PipelineArgs args)
        {
            if (!DemoStateService.IsDemoEnabled)
                return;
            var trackerData = CreateTrackerData();
            if (trackerData == null)
                return;
            HttpContext.Current.Session[FAKE_TRACKER_DATA] = trackerData;
        }

        private static TrackerData CreateTrackerData()
        {
            if (Sitecore.Context.Item == null || !Sitecore.Context.Item.IsDerived(Templates.DemoContent.ID))
                return null;

            var demoContent = new DemoContent(Sitecore.Context.Item);
            var trackerData = new TrackerData();

            InitializeReferrer(demoContent, trackerData);
            InitializeIpAddress(demoContent, trackerData);
            InitializeGeoData(demoContent, trackerData);
            return trackerData;
        }

        private static void InitializeGeoData(DemoContent demoContent, TrackerData trackerData)
        {
            trackerData.GeoData = demoContent.GeoData;
        }

        private static void InitializeIpAddress(DemoContent demoContent, TrackerData trackerData)
        {
            IPAddress address;
            if (!IPAddress.TryParse(demoContent.IpAddress, out address))
                address = null;
            trackerData.IpAddress = address;
        }

        private static void InitializeReferrer(DemoContent demoContent, TrackerData trackerData)
        {
            Uri referrerUri;
            if (!Uri.TryCreate(demoContent.Referrer, UriKind.Absolute, out referrerUri))
                referrerUri = null;
            trackerData.Referrer = referrerUri;
        }

        public void SetFakeTrackerData(PipelineArgs args)
        {
            if (!Tracker.IsActive || !this.DemoStateService.IsDemoEnabled)
                return;

            if (!(HttpContext.Current.Session[FAKE_TRACKER_DATA] is TrackerData))
                return;
            var trackerData = (TrackerData)HttpContext.Current.Session[this.FAKE_TRACKER_DATA];
            SetReferrer(trackerData);
            SetIpAddress(trackerData);
            SetLocation(trackerData);

            HttpContext.Current.Session.Remove(FAKE_TRACKER_DATA);
        }

        private void SetLocation(TrackerData trackerData)
        {
            if (trackerData.GeoData == null)
                return;

            if (Tracker.Current.Interaction.HasGeoIpData && Tracker.Current.Interaction.GeoData.Latitude.HasValue)
                return;
            Tracker.Current.Interaction.SetGeoData(trackerData.GeoData);
            Tracker.Current.Interaction.UpdateLocationReference();
        }

        private void SetIpAddress(TrackerData trackerData)
        {
            if (trackerData.IpAddress == null)
                return;

            if (Tracker.Current.Interaction.Ip != null && Tracker.Current.Interaction.HasGeoIpData && Tracker.Current.Interaction.GeoData.Latitude.HasValue)
                return;
            Tracker.Current.Interaction.Ip = trackerData.IpAddress.GetAddressBytes();
            Tracker.Current.Interaction.UpdateGeoIpData();
        }

        private void SetReferrer(TrackerData trackerData)
        {
            if (trackerData.Referrer == null)
                return;

            //Only set the referrer if it hasn't been set previously - or if the previous referrer is the current site
            if (Tracker.Current.Interaction.Referrer != null && !HttpContext.Current.Request.Url.Host.Equals(Tracker.Current.Interaction.ReferringSite, StringComparison.InvariantCultureIgnoreCase))
            {
                return;
            }

            Tracker.Current.Interaction.Referrer = trackerData.Referrer.ToString();
            Tracker.Current.Interaction.ReferringSite = trackerData.Referrer.Host;
            var args = new ParseReferrerArgs
            {
                UrlReferrer = trackerData.Referrer,
                Interaction = Tracker.Current.Interaction
            };
            ParseReferrerPipeline.Run(args);
        }

        private class TrackerData
        {
            public Uri Referrer { get; set; }
            public IPAddress IpAddress { get; set; }
            public WhoIsInformation GeoData { get; set; }
        }

    }
}