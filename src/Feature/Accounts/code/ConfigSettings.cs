namespace Sitecore.Feature.Accounts
{
  using Sitecore.Configuration;
  using Sitecore.Data;

  public class ConfigSettings
  {
    public static ID LoginGoalId => new ID(Settings.GetSetting("Sitecore.Feature.Accounts.LoginGoalId", "{66722F52-2D13-4DCC-90FC-EA7117CF2298}"));

    public static ID RegistrationGoalId => new ID(Settings.GetSetting("Sitecore.Feature.Accounts.RegistrationGoalId", "{8FFB183B-DA1A-4C74-8F3A-9729E9FCFF6A}"));
  }
}