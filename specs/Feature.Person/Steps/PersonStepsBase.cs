namespace Sitecore.Feature.Specflow.Steps
{
  using System.Security.Claims;
  using Sitecore.Feature.Specflow.Infrastructure;
  using Sitecore.Foundation.Common.Specflow.Infrastructure;
  using TechTalk.SpecFlow;

  [Binding, Scope(Tag = "UI")]
  public class PersonStepsBase : Steps
  {
    public CommonLocators SiteBase => new CommonLocators(FeatureContext);
    public PersonLocators PersonLocators => new PersonLocators();
  }
}