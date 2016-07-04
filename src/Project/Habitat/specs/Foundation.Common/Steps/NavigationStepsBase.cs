namespace Sitecore.Foundation.Common.Specflow.Steps
{
  using System;
  using System.Linq;
  using OpenQA.Selenium;
  using Sitecore.Foundation.Common.Specflow.Extensions;
  using Sitecore.Foundation.Common.Specflow.Extensions.Infrastructure;
  using Sitecore.Foundation.Common.Specflow.Infrastructure;
  using TechTalk.SpecFlow;

  [Binding]
  public class NavigationStepsBase
  {
    private readonly FeatureContext featureContext;
    private readonly CommonLocators commonLocators;

    public NavigationStepsBase(FeatureContext featureContext)
    {
      this.featureContext = featureContext;
      this.commonLocators = new CommonLocators(featureContext);
    }

    [Given(@"Habitat website is opened on Main Page")]
    [When(@"Habitat website is opened on Main Page")]
    public void GivenHabitatWebsiteIsOpenedOnMainPage()
    {
      this.commonLocators.NavigateToPage(BaseSettings.BaseUrl);
    }

    [When(@"User clicks Globe icon")]
    public void WhenUserClicksGlobeIcon()
    {
      this.commonLocators.GlobeIcon.Click();
    }


    [When(@"Actor selects (.*) slidebar")]
    public void WhenActorSelectsSlidebar(string button)
    {
      this.commonLocators.OpenXdbSlidebar.First(el => el.GetAttribute("title").Contains(button)).Click();
    }

    [Given(@"Habitat website is opened on Forms page")]
    public void GivenHabitatWebsiteIsOpenedOnFormsPage()
    {
      this.commonLocators.NavigateToPage(BaseSettings.FormsPageUrl);
    }

    [When(@"User opens Experience Editor")]
    public void WhenUserOpensExperienceEditor()
    {
      this.commonLocators.ExperianceEditor(BaseSettings.ExperianceEditorUrl);
    }

    [When(@"User selects Twitter placeholder")]
    public void WhenUserSelectsTwitterPlaceholder()
    {
      this.commonLocators.WaitRibbonPreLoadingIndicatorInvisible();

      var js = this.Driver as IJavaScriptExecutor;
      js.ExecuteScript(@"document.querySelector('.well.bg-dark.scEnabledChrome').click()");
    }

    [When(@"User selects Page Header Media Carousel placeholder")]
    public void WhenUserSelectsPageHeaderMediaCarouselPlaceholder()
    {
      this.commonLocators.WaitRibbonPreLoadingIndicatorInvisible();

      var js = this.Driver as IJavaScriptExecutor;
      js.ExecuteScript(@"document.querySelector('.jumbotron.jumbotron-xl.bg-media').click()");
    }


    [When(@"User clicks (.*) button on scChromeToolbar undefined")]
    public void WhenUserClicksButtonOn(string button)
    {
      var js = this.Driver as IJavaScriptExecutor;
      js.ExecuteScript($@"document.querySelectorAll("".scChromeToolbar.undefined a.scChromeCommand[title = '{button}']"")[0].click()");
      //CommonLocators.DatasourceCommand.First(el =>el.Displayed).Click();
    }

    public IWebDriver Driver => this.featureContext.Driver();

    [Given(@"Experience Editor is opened on Social Page")]
    public void GivenExperienceEditorIsOpenedOnSocialPage()
    {
      this.commonLocators.NavigateToPage(BaseSettings.SocialPageExperienceEditorUrl);
      this.commonLocators.SitecoreLoginFields.First(x => x.GetAttribute("name").Equals("UserName", StringComparison.InvariantCultureIgnoreCase)).SendKeys(BaseSettings.UserNameOnUi);
      this.commonLocators.SitecoreLoginFields.First(x => x.GetAttribute("name").Equals("Password", StringComparison.CurrentCultureIgnoreCase)).SendKeys(BaseSettings.Password);
      this.commonLocators.SubmitButton.Click();
    }

    [Given(@"Experience Editor is opened on Main Page")]
    public void GivenExperienceEditorIsOpenedOnMainPage()
    {
      this.commonLocators.NavigateToPage(BaseSettings.MainPageExperienceEditorUrl);
      this.commonLocators.SitecoreLoginFields.First(x => x.GetAttribute("name").Equals("UserName", StringComparison.InvariantCultureIgnoreCase)).SendKeys(BaseSettings.UserNameOnUi);
      this.commonLocators.SitecoreLoginFields.First(x => x.GetAttribute("name").Equals("Password", StringComparison.CurrentCultureIgnoreCase)).SendKeys(BaseSettings.Password);
      this.commonLocators.SubmitButton.Click();
    }
  }
}