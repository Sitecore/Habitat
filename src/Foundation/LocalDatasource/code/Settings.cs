namespace Sitecore.Foundation.LocalDatasource
{
    public static class Settings
  {
    private static string LocalDatasourceFolderNameSetting = "Foundation.LocalDatasource.LocalDatasourceFolderName";
    private static string LocalDatasourceFolderNameDefault = "_Local";
    private static string LocalDatasourceFolderTemplateSetting = "Foundation.LocalDatasource.LocalDatasourceFolderTemplate";

    public static string LocalDatasourceFolderName => Sitecore.Configuration.Settings.GetSetting(LocalDatasourceFolderNameSetting, LocalDatasourceFolderNameDefault);
    public static string LocalDatasourceFolderTemplate => Sitecore.Configuration.Settings.GetSetting(LocalDatasourceFolderTemplateSetting);
  }
}