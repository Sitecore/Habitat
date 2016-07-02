using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using Sitecore.Foundation.Common.Specflow.Extensions;
using Sitecore.Foundation.Common.Specflow.Extensions.Infrastructure;
using Sitecore.Foundation.Common.Specflow.Infrastructure;
using TechTalk.SpecFlow;

namespace Sitecore.Foundation.Common.Specflow.Steps
{
  [Binding]
  public class NavigationStepsBase : StepsBase
  {


    [Given(@"Habitat website is opened on Main Page")]
    [When(@"Habitat website is opened on Main Page")]
    public void GivenHabitatWebsiteIsOpenedOnMainPage()
    {
      CommonLocators.NavigateToPage(BaseSettings.BaseUrl);
    }

    [When(@"User clicks Globe icon")]
    public void WhenUserClicksGlobeIcon()
    {
      CommonLocators.GlobeIcon.Click();
    }





    [When(@"Actor selects (.*) slidebar")]
    public void WhenActorSelectsSlidebar(string button)
    {
      CommonLocators.OpenXdbSlidebar.First(el => el.GetAttribute("title").Contains(button)).Click();
    }

    [Given(@"Habitat website is opened on Forms page")]
    public void GivenHabitatWebsiteIsOpenedOnFormsPage()
    {
      CommonLocators.NavigateToPage(BaseSettings.FormsPageUrl);
    }

    [When(@"User opens Experience Editor")]
    public void WhenUserOpensExperienceEditor()
    {
      CommonLocators.ExperianceEditor(BaseSettings.ExperianceEditorUrl);
    }

    [When(@"User selects Twitter placeholder")]
    public void WhenUserSelectsTwitterPlaceholder()
    {
      CommonLocators.WaitRibbonPreLoadingIndicatorInvisible(); 
      
      var js = Driver as IJavaScriptExecutor;
      js.ExecuteScript(@"document.querySelector('.well.bg-dark.scEnabledChrome').click()");
    }

    [When(@"User selects Page Header Media Carousel placeholder")]
    public void WhenUserSelectsPageHeaderMediaCarouselPlaceholder()
    {
      CommonLocators.WaitRibbonPreLoadingIndicatorInvisible();

      var js = Driver as IJavaScriptExecutor;
      js.ExecuteScript(@"document.querySelector('.jumbotron.jumbotron-xl.bg-media').click()");
    }



    [When(@"User clicks (.*) button on scChromeToolbar undefined")]
    public void WhenUserClicksButtonOn(string button)
    {
      var js = Driver as IJavaScriptExecutor;
      js.ExecuteScript($@"document.querySelectorAll("".scChromeToolbar.undefined a.scChromeCommand[title = '{button}']"")[0].click()");
      //CommonLocators.DatasourceCommand.First(el =>el.Displayed).Click();
    }
      
    [Given(@"Experience Editor is opened on Social Page")]
    public void GivenExperienceEditorIsOpenedOnSocialPage()
    {
      CommonLocators.NavigateToPage(BaseSettings.SocialPageExperienceEditorUrl);
      CommonLocators.SitecoreLoginFields.First(x => x.GetAttribute("name").Equals("UserName", StringComparison.InvariantCultureIgnoreCase)).SendKeys(BaseSettings.UserNameOnUi);
      CommonLocators.SitecoreLoginFields.First(x => x.GetAttribute("name").Equals("Password", StringComparison.CurrentCultureIgnoreCase)).SendKeys(BaseSettings.Password);
      CommonLocators.SubmitButton.Click();
    }

    [Given(@"Experience Editor is opened on Main Page")]
    public void GivenExperienceEditorIsOpenedOnMainPage()
    {
      CommonLocators.NavigateToPage(BaseSettings.MainPageExperienceEditorUrl);
      CommonLocators.SitecoreLoginFields.First(x => x.GetAttribute("name").Equals("UserName", StringComparison.InvariantCultureIgnoreCase)).SendKeys(BaseSettings.UserNameOnUi);
      CommonLocators.SitecoreLoginFields.First(x => x.GetAttribute("name").Equals("Password", StringComparison.CurrentCultureIgnoreCase)).SendKeys(BaseSettings.Password);
      CommonLocators.SubmitButton.Click();
    }



  }
}
