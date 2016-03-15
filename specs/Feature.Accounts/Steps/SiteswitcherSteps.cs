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
      values.All(v => CommonLocators.SiteSwitcherIconDropDownChildElements.Any(x => x.Text == v)).Should().BeTrue();
    }

    [Then(@"(.*) value is selected by default")]
    public void ThenValueIsSelectedByDefault(string value)
    {
      CommonLocators.DropDownActiveValues.Any(v =>v.Text.Equals(value, StringComparison.InvariantCultureIgnoreCase)).Should().BeTrue();
    }

    [When(@"Actor selects (.*) from siteswitcher combo-box")]
    public void WhenActorSelectsFromSiteswitcherComboBox(string link)
    {
      var element = CommonLocators.SiteSwitcherIconDropDownChildElements.First(el => el.Text.Equals(link, StringComparison.InvariantCultureIgnoreCase));
      element.Click();
    }





  }
}
