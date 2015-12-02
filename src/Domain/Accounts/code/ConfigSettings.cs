namespace Habitat.Accounts
{
  using Sitecore.Configuration;
  using Sitecore.Data;

  public class ConfigSettings
  {
    public static ID LoginGoalId => new ID(Settings.GetSetting("Habitat.Feature.Accounts.LoginGoalId", "{66722F52-2D13-4DCC-90FC-EA7117CF2298}"));

    public static ID RegisterGoalId => new ID(Settings.GetSetting("Habitat.Feature.Accounts.RegisterGoalId", "{8FFB183B-DA1A-4C74-8F3A-9729E9FCFF6A}"));
  }
}