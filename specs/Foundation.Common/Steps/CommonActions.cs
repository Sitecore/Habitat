namespace Sitecore.Foundation.Common.Specflow.Steps
{
  using System.Linq;
  using Sitecore.Foundation.Common.Specflow.Extensions;
  using Sitecore.Foundation.Common.Specflow.Infrastructure;
  using TechTalk.SpecFlow;

  [Binding]
  public class CommonActions
  {
    private readonly FeatureContext featureContext;
    private readonly CommonLocators locators;

    public CommonActions(FeatureContext featureContext)
    {
      this.featureContext = featureContext;
      this.locators = new CommonLocators(featureContext);
    }

    [Given(@"Actor clicks (.*) button")]
    [When(@"Actor clicks (.*) button")]
    public void ActorClicksSubmitTypeButton(string btn)
    {
      this.locators.SubmitButton.Click();
    }

    

    [When(@"Actor expands (.*) header on xDB panel")]
    public void WhenActorExpandsHeaderOnXdbPanel(string header)
    {
      locators.XdBpanelHeader.First(el => el.Text.Equals(header)).Click();
    }

    [When(@"Actor deletes all browser cookies")]
    public void WhenActorDeletesAllBrowserCookies()
    {
      this.featureContext.Driver().Manage().Cookies.DeleteAllCookies();
    }

    [When(@"User selects DANSK from the list")]
    public void WhenUserSelectsDanskFromTheList()
    {
      this.locators.GlobeIconList.First(el => el.Text.Contains("dansk")).Click();
    }
  }
}