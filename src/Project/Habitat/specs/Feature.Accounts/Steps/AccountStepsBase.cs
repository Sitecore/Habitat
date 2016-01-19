namespace Sitecore.Feature.Accounts.Specflow.Steps
{
  using Sitecore.Feature.Accounts.Specflow.Infrastructure;
  using Sitecore.Foundation.Common.Specflow.Steps;
  using TechTalk.SpecFlow;

  [Binding]
  public class AccountStepsBase : StepsBase
  {
    public Site Site => new Site();

    public SiteNavigation SiteNavigation => new SiteNavigation();

    public AccountSettings Settings => new AccountSettings();
  }
}