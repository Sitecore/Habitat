using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using OpenQA.Selenium;
using Sitecore.Foundation.Common.Specflow.Extensions.Infrastructure;
using Sitecore.Foundation.Common.Specflow.Infrastructure;
using TechTalk.SpecFlow;

namespace Sitecore.Foundation.Common.Specflow.Steps
{
  [Binding]
  public class CommonResults : StepsBase
  {
    [Then(@"Habitat website is opened on Main Page (.*)")]
    [Then(@"Page URL ends on (.*)")]
    public void ThenPageUrlEndsOnExpected(string urlEnding)
    {
      if (urlEnding == "BaseUrl")
      {
        Driver.Url.EndsWith(BaseSettings.BaseUrl + "/en").Should().BeTrue();
      }
      else
      {
        Driver.Url.EndsWith(urlEnding).Should().BeTrue();
      }
    }

    [Then(@"Page URL not ends on (.*)")]
    public void ThenPageUrlNotEndsOn(string urlEnding)
    {
      Driver.Url.EndsWith(urlEnding).Should().BeFalse();
    }

    [Then(@"Demo site title equals to (.*)")]
    public void ThenDemoSiteTitleEqualsTo(string title)
    {
      CommonLocators.DemoSiteLogo.GetAttribute("title").Equals(title);
    }


    [Then(@"URl contains (.*) site url")]
    public void ThenURlContainsDemoSite(string site)
    {
      Driver.Url.Equals(BaseSettings.DemoSiteUrl);
    }

    [Then(@"User icon presents on Personal Information header section")]
    public void ThenUserIconPresentsOnPersonalInformationHeaderSection()
    {
      CommonLocators.UserIconOnPersonalInformation.Should().NotBeNull();
    }


    [Then(@"Personal Information header contains (.*) label")]
    public void ThenPersonalInformationHeaderContainsLable(string label)
    {
      CommonLocators.MediaTitleOnPersonalInformation.First(el => el.GetAttribute("innerText").Contains(label)).Should().NotBeNull();
    }

    [Then(@"Identification secret icon presents")]
    public void ThenIdentificationSecterIconPresents()
    {
      CommonLocators.IdentificationUknownStatusIcon.Should().NotBeNull();
    }

    [Then(@"Identification known icon presents")]
    public void ThenIdentificationKnownIconPresents()
    {
      CommonLocators.IdentificationKnownStatusIcon.Should().NotBeNull();
    }


    [Then(@"xDB Panel Body text contains")]
    public void ThenXdbPanelBodyTextContains(Table table)
    {
      var bodyText = table.Rows.Select(x => x.Values.First());
      bodyText.All(t => CommonLocators.XdBpanelMediaBody.Any(x => x.GetAttribute("innerText").Contains(t))).Should().BeTrue();
    }


    [When(@"Actor clicks (.*) button on xDB panel")]
    public void WhenActorClicksButtonOnXdbPanel(string button)
    {
      CommonLocators.ManageXdBpanelButtons.First(el => el.Text.Contains(button)).Click();
    }

    [Then(@"there is no Globe icon in the Main menu on the top of the page")]
    public void ThenThereIsNoGlobeIconInTheMainMenuOnTheTopOfThePage()
    {
      CommonLocators.GlobeIcon.FindElements(By.CssSelector(".fa.fa-globe")).Should().BeEmpty();
    }

    [Then(@"Globe icon was hided")]
    public void ThenGlobeIconWasHided()
    {
      CommonLocators.GlobeIconExists().Should().BeFalse();
    }

    [Then(@"Following items available in the list")]
    public void ThenFollowingItemsAvailableInTheList(Table table)
    {
      var globeList = table.Rows.Select(x => x.Values.First());
      globeList.All(z => CommonLocators.GlobeIconList.Any(x => x.Text == z)).Should().BeTrue();
    }

    [Then(@"The following tag is present")]
    public void ThenTheFollowingTagIsPresent(Table table)
    {
      var contentText = table.Rows.Select(x => x.Values.First());
      contentText.All(z => CommonLocators.MetakeywordTag.Any(x => x.GetAttribute("content").Contains(z))).Should().BeTrue();

    }


    [Then(@"Following items present in datasource tree")]
    public void ThenFollowingItemsPresentInTree(Table table)
    {
      CommonLocators.NavigateToExperienceEditorDialogWindow();
      var values = table.Rows.Select(x => x.Values.First());
      values.All(
        v =>
          CommonLocators.TwitterTreeContent.Any(x => x.Text.Equals(v, StringComparison.InvariantCultureIgnoreCase)))
        .Should()
        .BeTrue();
      CommonLocators.NavigateFromExperienceEditorDialogWindow();
    }


  }
}
