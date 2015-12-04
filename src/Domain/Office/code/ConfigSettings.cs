namespace Habitat.Office
{
  using Sitecore.Configuration;

  public class ConfigSettings
  {
    public static string GoogleMapApiKey => Settings.GetSetting("Habitat.Feature.Office.GoogleMapApiKey", "");
  }
}