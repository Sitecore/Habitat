namespace Sitecore.Feature.Demo.Repositories
{
  using Sitecore.Analytics;
  using Sitecore.Analytics.Tracking;
  using Sitecore.Feature.Demo.Models;

  internal class LocationRepository : Location
  {
    public Location GetCurrent()
    {
      return !Tracker.Current.Interaction.HasGeoIpData ? null : this.CreateLocation(Tracker.Current.Interaction.GeoData);
    }

    private Location CreateLocation(ContactLocation geoData)
    {
      if (geoData.Latitude == null || geoData.Longitude == null)
        return null;
      return new Location
             {
               City = geoData.City,
               Country = geoData.Country
             };
    }
  }
}