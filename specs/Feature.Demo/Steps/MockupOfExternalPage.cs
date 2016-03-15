using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Sitecore.Foundation.Common.Specflow.Infrastructure;
using Sitecore.Foundation.Common.Specflow.Steps;
using TechTalk.SpecFlow;
using OpenQA.Selenium.Support.UI;

namespace Sitecore.Feature.Demo.Specflow.Steps
{
  public class MockupOfExternalPage:DemoStepsBase
  {




    [Then(@"Following buttons are present on the Demo site page")]
    public void ThenFollowingButtonsArePresentOnTheDemoSitePage(Table table)
    {
      var buttons = table.Rows.Select(x => x.Values.First());
      var elements = SiteDemo.DemoSiteButton.Select(el =>el.GetAttribute("value"));
      elements.Should().Contain(buttons);
    }

    [Given(@"Mockup of Google page is opened")]
    public void GivenMockupOfGooglePageIsOpened()
    {
      SiteBase.NavigateToPage(BaseSettings.DemoSiteURL);
    }

    [When(@"Actor enters (.*) text in to search field")]
    public void WhenActorEntersTextInToSearchField(string text)
    {
      SiteDemo.GoogleSearchFieldMockup.SendKeys(text);
    }

    //TODO: Re-work 
/*    [When(@"Actor clicks (.*) button")]
    public void WhenActorClicksGoogleSearchButton(string button)
    {
      var element = SiteDemo.DemoSiteButton.Single(el =>el.GetAttribute("value") == button);
      element.Click();
    }*/

    [When(@"Actor clicks (.*)‎ link")]
    public void WhenActorClicksSitecoreHabitat_FlexibilitySimplicityExtensibilityLink(string link)
    {
      var element = SiteDemo.HabitatOnGoogleResults.Single(el => el.Text == link);
      element.Click();
    }


    [Then(@"Search results contains following sitelink")]
    public void ThenSearchResultsContainsFollowingSitelink(Table table)
    {
      var links = table.Rows.Select(x => x.Values.First());
      var elements = SiteDemo.HabitatOnGoogleResults.Select(el => el.Text);
      elements.Should().Contain(el=>links.Any(l=>l.Equals(el, StringComparison.InvariantCulture)));
    }

    [Then(@"Page url contains Campaign ID")]
    public void ThenPageUrlContainsCampaignId()
    {
      Driver.Url.Equals(Foundation.Common.Specflow.Infrastructure.BaseSettings.DemoSiteCampaignUrl);
    }


  }
}
