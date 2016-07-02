namespace Sitecore.Feature.Metadata.Specflow.Steps
{
  using Sitecore.Foundation.Common.Specflow.Extensions.Infrastructure;
  using Sitecore.Foundation.Common.Specflow.Infrastructure;
  using TechTalk.SpecFlow;

  [Binding]
  internal class MetadataNavigationSteps : Steps
  {
    [Then(@"Habitat website is opened on Getting Started page")]
    public void ThenHabitatWebsiteIsOpenedOnGettingStartedPage()
    {
      this.CommonLocators.NavigateToPage(BaseSettings.GettingStartedPageUrl);
    }

    public CommonLocators CommonLocators => new CommonLocators(this.FeatureContext);
  }
}