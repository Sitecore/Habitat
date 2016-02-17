using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Sitecore.Foundation.Common.Specflow.Infrastructure;
using Sitecore.Foundation.Common.Specflow.Steps;
using TechTalk.SpecFlow;

namespace Sitecore.Feature.Accounts.Specflow.Steps
{
  class SiteswitcherSteps: AccountStepsBase
  {
    [Then(@"System shows following avalilable sites")]
    public void ThenSystemShowsFollowingAvalilableSites(Table table)
    {
      var values = table.Rows.Select(x => x.Values.First());
      //1
      foreach (var value in values)
      {
        var found = false;
        foreach (var webElement in SiteBase.SiteSwitcherIconDropDownChildElements)
        {
          found = webElement.Text == value;
          if (found)
          {
            break;
          }
        }
        found.Should().BeFalse();
      }
    }

    [Then(@"(.*) value is selected by default")]
    public void ThenValueIsSelectedByDefault(string value)
    {
     
      var element = SiteBase.SiteSwitcherIconDropDownChildElements.First(el => el.Text.Equals(value, StringComparison.InvariantCultureIgnoreCase));
      element.GetAttribute("class").Should().Contain("active");
    }

    [When(@"Actor selects (.*) from siteswitcher combo-box")]
    public void WhenActorSelectsFromSiteswitcherComboBox(string link)
    {
      var element = SiteBase.SiteSwitcherIconDropDownChildElements.First(el => el.Text.Equals(link, StringComparison.InvariantCultureIgnoreCase));
      element.Click();
    }

    [Then(@"URl contains (.*) site url")]
    public void ThenURlContainsDemoSite(string site)
    {
      Driver.Url.Equals(BaseSettings.DemoSiteURL);
    }

    [Then(@"Demo site title equals to (.*)")]
    public void ThenDemoSiteTitleEqualsTo(string title)
    {
      SiteBase.DemoSiteLogo.GetAttribute("title").Equals(title);
    }


  }
}
