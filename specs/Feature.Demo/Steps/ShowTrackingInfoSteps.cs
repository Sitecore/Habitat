using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using TechTalk.SpecFlow;

namespace Sitecore.Feature.Demo.Specflow.Steps
{
  public class ShowTrackingInfoSteps: DemoStepsBase
  {
    [Then(@"Campaign presents on Campaign section of the Show Tracking Info")]
    public void ThenCampaignPresentsOnCampaignSectionOfTheShowTrackingInfo(Table table)
    {
      var campaigns = table.Rows.Select(v => v.Values.First());
      var elements = SiteDemo.TrackingInfoCampaignList;
      
      elements.Should().Contain(campaigns);

    }

  }
}
