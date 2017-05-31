namespace Sitecore.Feature.Demo.Repositories
{
    using System;
    using System.Globalization;
    using Sitecore.Analytics;
    using Sitecore.Analytics.Tracking;
    using Sitecore.Feature.Demo.Models;

    internal class LocationRepository
    {
        public Location GetCurrent()
        {
            return !Tracker.Current.Interaction.HasGeoIpData ? null : this.CreateLocation(Tracker.Current.Interaction.GeoData);
        }

        private Location CreateLocation(ContactLocation geoData)
        {
            if (geoData.Latitude == null || geoData.Longitude == null)
            {
                return null;
            }
            return new Location
            {
                BusinessName = geoData.BusinessName,
                Url = geoData.Url,
                City = geoData.City,
                Region = geoData.Region,
                Country = geoData.Country,
                Latitude = string.Format(CultureInfo.InvariantCulture, "{0:0.#######}", geoData.Latitude),
                Longitude = string.Format(CultureInfo.InvariantCulture, "{0:0.#######}", geoData.Longitude),
            };
        }
    }
}