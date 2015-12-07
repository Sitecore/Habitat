namespace Sitecore.Feature.Maps
{
  using Sitecore.Configuration;

  public class ConfigSettings
  {
    public static string GoogleMapApiKey => Settings.GetSetting("Sitecore.Feature.Maps.GoogleMapApiKey", "");
  }
}