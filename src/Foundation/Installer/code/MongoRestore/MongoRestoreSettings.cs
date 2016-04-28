namespace Sitecore.Foundation.Installer.MongoRestore
{
  using Sitecore.Configuration;
  using Sitecore.Xdb.Configuration;

  public static class MongoRestoreSettings
  {
    public static bool RestoreMongoDatabases => XdbSettings.Enabled && Settings.GetBoolSetting("Sitecore.Foundation.Installer.RestoreMongo", true);

    public const string RestoredDbTokenCollection = "RestoredDbToken";
  }
}