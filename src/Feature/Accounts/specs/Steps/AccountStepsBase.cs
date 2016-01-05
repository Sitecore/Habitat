namespace Sitecore.Feature.Accounts.Specflow.Steps
{
  using Common.Specflow.Steps;
  using Habitat.Accounts.Specflow.Infrastructure;
  using TechTalk.SpecFlow;

  [Binding]
  public class AccountStepsBase : StepsBase
  {
    public Site Site => new Site();

    public SiteNavigation SiteNavigation => new SiteNavigation();

    public AccountSettings Settings => new AccountSettings();
  }
}