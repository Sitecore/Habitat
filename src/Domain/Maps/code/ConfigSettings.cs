namespace Habitat.Maps
{
  using Sitecore.Configuration;

  public class ConfigSettings
  {
    public static string GoogleMapApiKey => Settings.GetSetting("Habitat.Feature.Maps.GoogleMapApiKey", "");
  }
}