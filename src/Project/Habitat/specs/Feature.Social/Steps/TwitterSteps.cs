using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using OpenQA.Selenium;
using Sitecore.Feature.Social.Specflow.Infrastructure;
using Sitecore.Foundation.Common.Specflow.Infrastructure;
using Sitecore.Foundation.Common.Specflow.Steps;
using TechTalk.SpecFlow;

namespace Sitecore.Feature.Social.Specflow.Steps
{
  [Binding]
  public class TwitterSteps
  {
    private readonly SocialLocators _locators;

    public TwitterSteps(SocialLocators locators)
    {
      _locators = locators;
    }
    [Then(@"Block with following title is present on the page")]
    public void ThenBlockWithFollowingTitleIsPresentOnThePage(Table table)
    {
      var text = table.Rows.Select(x => x.Values.First());
      text.All(t => _locators.TwitterPlaceholder.Any(x => x.GetAttribute("innerText").Contains(t))).Should().BeTrue();
    }

  }
}
