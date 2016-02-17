using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Foundation.Common.Specflow.Infrastructure;
using Sitecore.Foundation.Common.Specflow.Steps;
using TechTalk.SpecFlow;

namespace Sitecore.Feature.Demo.Specflow.Steps
{
  using OpenQA.Selenium;
  using Sitecore.Foundation.Common.Specflow.Extensions;
  using TechTalk.SpecFlow;
  class DemoNavigationSteps:DemoStepsBase
  {
    [Given(@"Habitat website is opened on Main Page")]
    public void GivenHabitatWebsiteIsOpenedOnMainPage()
    {
      Driver.Navigate().GoToUrl(BaseSettings.BaseUrl);
    }

    [When(@"Actor navigates to (.*) site")]
    public void WhenActorNavigatesToDemoSite(string link)
    {
      SiteBase.SiteSwitcherIcon.MoveToElement();

#warning hack for selenium hover behavoiur
      var dropdown = SiteBase.SiteSwitcherIcon.FindElement(By.XPath("../../ul"));
      var js = Driver as IJavaScriptExecutor;
      js?.ExecuteScript("arguments[0].style.display='block'", dropdown);

      var element = SiteBase.SiteSwitcherIconDropDownChildElements.First(el => el.Text.Equals(link, StringComparison.InvariantCultureIgnoreCase));
      element.Click();
    }

  }
}
