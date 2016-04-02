﻿using Sitecore.Foundation.Common.Specflow.Infrastructure;

namespace Sitecore.Feature.Accounts.Specflow.Steps
{
  using OpenQA.Selenium;
  using Sitecore.Foundation.Common.Specflow.Extensions;
  using TechTalk.SpecFlow;

  public class SiteNavigationSteps : AccountStepsBase
  {
    

    [When(@"Actor opens Habitat website on Login page")]
    [Given(@"Habitat website is opened on Login page")]
    [When(@"Actor navigates to Login page")]
    public void WhenActorNavigatesToLoginPage()
    {
      SiteBase.NavigateToPage(BaseSettings.LoginPageUrl);
    }

    [Given(@"Habitat website is opened on Forgot Password page")]
    public void GivenHabitatWebsiteIsOpenedOnForgotPasswordPage()
    {
      SiteBase.NavigateToPage(BaseSettings.ForgotPasswordPageUrl);
    }

    [When(@"Actor moves on siteswitcher combo-box")]
    public void WhenActorMovesOnSiteswitcherCombo_Box()
    {
      SiteBase.SiteSwitcherIcon.MoveToElement();

#warning hack for selenium hover behavoiur
      var dropdown = SiteBase.SiteSwitcherIcon.FindElement(By.XPath("../../ul"));
      var js = Driver as IJavaScriptExecutor;
      js?.ExecuteScript("arguments[0].style.display='block'", dropdown);
    }


  }
}