namespace Sitecore.Foundation.Common.Specflow.Steps
{
  using System;
  using System.Linq;
  using FluentAssertions;
  using OpenQA.Selenium;
  using Sitecore.Foundation.Common.Specflow.Extensions;
  using Sitecore.Foundation.Common.Specflow.Extensions.Infrastructure;
  using Sitecore.Foundation.Common.Specflow.Infrastructure;
  using TechTalk.SpecFlow;

  [Binding]
  public class CommonResults
  {
    private readonly FeatureContext featureContext;
    private readonly CommonLocators commonLocators;

    public CommonResults(FeatureContext featureContext)
    {
      this.featureContext = featureContext;
      this.commonLocators = new CommonLocators(featureContext);
    }

    [Then(@"Habitat website is opened on Main Page (.*)")]
    [Then(@"Page URL ends on (.*)")]
    public void ThenPageUrlEndsOnExpected(string urlEnding)
    {
      if (urlEnding == "BaseUrl")
      {
        this.Driver.Url.EndsWith(BaseSettings.BaseUrl + "/en").Should().BeTrue();
      }
      else
      {
        this.Driver.Url.EndsWith(urlEnding).Should().BeTrue();
      }
    }

    public IWebDriver Driver => this.featureContext.Driver();

    [Then(@"Page URL not ends on (.*)")]
    public void ThenPageUrlNotEndsOn(string urlEnding)
    {
      this.Driver.Url.EndsWith(urlEnding).Should().BeFalse();
    }

    [Then(@"Demo site title equals to (.*)")]
    public void ThenDemoSiteTitleEqualsTo(string title)
    {
      this.commonLocators.DemoSiteLogo.GetAttribute("title").Equals(title);
    }


    [Then(@"URl contains (.*) site url")]
    public void ThenURlContainsDemoSite(string site)
    {
      this.Driver.Url.Equals(BaseSettings.DemoSiteUrl);
    }

    [Then(@"User icon presents on Personal Information header section")]
    public void ThenUserIconPresentsOnPersonalInformationHeaderSection()
    {
      this.commonLocators.UserIconOnPersonalInformation.Should().NotBeNull();
    }


    [Then(@"Personal Information header contains (.*) label")]
    public void ThenPersonalInformationHeaderContainsLable(string label)
    {
      this.commonLocators.MediaTitleOnPersonalInformation.First(el => el.GetAttribute("innerText").Contains(label)).Should().NotBeNull();
    }

    [Then(@"Identification secret icon presents")]
    public void ThenIdentificationSecterIconPresents()
    {
      this.commonLocators.IdentificationUknownStatusIcon.Should().NotBeNull();
    }

    [Then(@"Identification known icon presents")]
    public void ThenIdentificationKnownIconPresents()
    {
      this.commonLocators.IdentificationKnownStatusIcon.Should().NotBeNull();
    }


    [Then(@"xDB Panel Body text contains")]
    public void ThenXdbPanelBodyTextContains(Table table)
    {
      var bodyText = table.Rows.Select(x => x.Values.First());
      bodyText.All(t => this.commonLocators.XdBpanelMediaBody.Any(x => x.GetAttribute("innerText").Contains(t))).Should().BeTrue();
    }


    [When(@"Actor clicks (.*) button on xDB panel")]
    public void WhenActorClicksButtonOnXdbPanel(string button)
    {
      this.commonLocators.ManageXdBpanelButtons.First(el => el.Text.Contains(button)).Click();
    }

    [Then(@"Globe icon was hided")]
    [Then(@"there is no Globe icon in the Main menu on the top of the page")]
    public void ThenGlobeIconWasHided()
    {
      this.commonLocators.GlobeIconExists().Should().BeFalse();
    }

    [Then(@"Following items available in the list")]
    public void ThenFollowingItemsAvailableInTheList(Table table)
    {
      var globeList = table.Rows.Select(x => x.Values.First());
      globeList.All(z => this.commonLocators.GlobeIconList.Any(x => x.Text == z)).Should().BeTrue();
    }

    [Then(@"The following tag is present")]
    public void ThenTheFollowingTagIsPresent(Table table)
    {
      var contentText = table.Rows.Select(x => x.Values.First());
      var actualTags = this.commonLocators.MetakeywordTag.Select(x => x.GetAttribute("content"));
      contentText.All(z => actualTags.Any(x => x.Contains(z)))
        .Should()
        .BeTrue($"page should contain following tags: {string.Join("|", contentText)} but contains only {string.Join("|", actualTags)}");
    }


    [Then(@"Following items present in datasource tree")]
    public void ThenFollowingItemsPresentInTree(Table table)
    {
      this.commonLocators.NavigateToExperienceEditorDialogWindow();
      var values = table.Rows.Select(x => x.Values.First());
      values.All(
        v => this.commonLocators.TwitterTreeContent.Any(x => x.Text.Equals(v, StringComparison.InvariantCultureIgnoreCase)))
        .Should()
        .BeTrue();
      this.commonLocators.NavigateFromExperienceEditorDialogWindow();
    }

    [Then(@"TwitterFeed component contains (.*) tweets")]
    public void ThenTwitterFeedComponentContainsTweets(int tweetsNumber)
    {
      this.commonLocators.NavigateToTwitterFeedFrame();
      var twitterFeedMassive = commonLocators.TwitterFeedLine;
      twitterFeedMassive.Count().Equals(tweetsNumber).Should().BeTrue();
      this.commonLocators.NavigateFromTwitterFeedFrame();
    }



  }
}