using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Foundation.Common.Specflow.Extensions.Infrastructure;
using TechTalk.SpecFlow;

namespace Sitecore.Feature.Metadata.Specflow.Steps
{
  class MetadataNavigationSteps:MetadataSetpsBase
  {
    [Then(@"Habitat website is opened on Getting Started page")]
    public void ThenHabitatWebsiteIsOpenedOnGettingStartedPage()
    {
      CommonLocators.NavigateToPage(BaseSettings.GettingStartedPageUrl);
    }

  }
}
