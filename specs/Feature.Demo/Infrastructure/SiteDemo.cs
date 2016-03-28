using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Sitecore.Feature.Demo.Specflow.Infrastructure
{
  public class SiteDemo
  {
    public IWebDriver Driver => FeatureContext.Current.Get<IWebDriver>();

    public IEnumerable<IWebElement> DemoSiteButton
      => Driver.FindElements(By.CssSelector(".jsb>center>input"));

    public IWebElement GoogleSearchFieldMockup
      =>Driver.FindElement(By.CssSelector("#lst-ib"));

    public IEnumerable<IWebElement> HabitatOnGoogleResults
      => Driver.FindElements(By.CssSelector("#vs0p1, #vs0p2, #vs0p3, #vs0p4"));

    public IEnumerable<IWebElement> TrackingInfoCampaignList
      => Driver.FindElements(By.CssSelector("#visitDetailsCampaign li"));
  }
}
