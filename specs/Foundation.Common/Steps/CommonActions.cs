using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using Sitecore.Foundation.Common.Specflow.Infrastructure;
using TechTalk.SpecFlow;

namespace Sitecore.Foundation.Common.Specflow.Steps
{
  [Binding]
  public class CommonActions:StepsBase
  {
    [Given(@"Actor clicks (.*) button")]
    [When(@"Actor clicks (.*) button")]
    public void ActorClicksSubmitTypeButton(string btn)
    {
      SiteBase.SubmitButton.Click();
    }

    public static void OpenUserDialog()
    {
      new CommonLocators().UserIcon.Click();
    }

    [When(@"Actor expands (.*) header on xDB panel")]
    public static void WhenActorExpandsHeaderOnXDBPanel(string header)
    {
      CommonLocators.XDBpanelHeader.First(el => el.Text.Equals(header)).Click();
    }

    [When(@"Actor deletes all browser cookies")]
    public void WhenActorDeletesAllBrowserCookies()
    {
      StepsBase.DeleteAllBrowserCookies();
    }


  }
}
