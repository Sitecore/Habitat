namespace Sitecore.Feature.Demo.Models.Repository
{
  using Sitecore.Analytics;
  using Sitecore.Analytics.Tracking;
  using Sitecore.CES.DeviceDetection;
  using Sitecore.Globalization;

  internal class LocationRepository : Location
  {
    public Location GetCurrent()
    {
      return !Tracker.Current.Interaction.HasGeoIpData ? null : CreateLocation(Tracker.Current.Interaction.GeoData);
    }

    private Location CreateLocation(ContactLocation geoData)
    {
      if (geoData.Latitude == null || geoData.Longitude == null)
        return null;
      return new Location()
            {
              City = geoData.City,
              Country = geoData.Country
            };
    }
  }
}