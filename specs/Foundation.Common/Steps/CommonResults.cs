using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Sitecore.Foundation.Common.Specflow.Infrastructure;
using TechTalk.SpecFlow;

namespace Sitecore.Foundation.Common.Specflow.Steps
{
  [Binding]
  public class CommonResults:StepsBase
  {
    [Then(@"Habitat website is opened on Main Page (.*)")]
    [Then(@"Page URL ends on (.*)")]
    public void ThenPageUrlEndsOnExpected(string urlEnding)
    {
      if (urlEnding == "BaseUrl")
      {
        Driver.Url.EndsWith(BaseSettings.BaseUrl + "/en" ).Should().BeTrue();
      }
      else
      {
        Driver.Url.EndsWith(urlEnding).Should().BeTrue();
      }     
    }

    [Then(@"Page URL not ends on (.*)")]
    public void ThenPageURLNotEndsOn(string urlEnding)
    {
      Driver.Url.EndsWith(urlEnding).Should().BeFalse();
    }

    [Then(@"Demo site title equals to (.*)")]
    public void ThenDemoSiteTitleEqualsTo(string title)
    {
      SiteBase.DemoSiteLogo.GetAttribute("title").Equals(title);
    }


    [Then(@"URl contains (.*) site url")]
    public void ThenURlContainsDemoSite(string site)
    {
      Driver.Url.Equals(BaseSettings.DemoSiteURL);
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
    public void ThenXDBPanelBodyTextContains(Table table)
    {
      var bodyText = table.Rows.Select(x => x.Values.First());
      bodyText.All(t => CommonLocators.XDBpanelMediaBody.Any(x => x.GetAttribute("innerText").Contains(t))).Should().BeTrue();
    }


    [When(@"Actor clicks (.*) button on xDB panel")]
    public void WhenActorClicksButtonOnXDBPanel(string button)
    {
      CommonLocators.ManageXDBpanelButtons.First(el => el.Text.Contains(button)).Click();
    }


  }
}
