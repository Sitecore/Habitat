using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using Sitecore.Foundation.Common.Specflow.Extensions;
using TechTalk.SpecFlow;

namespace Sitecore.Feature.Social.Specflow.Infrastructure
{
  public class SocialLocators
  {
    private readonly FeatureContext featureContext;

    public SocialLocators(FeatureContext featureContext)
    {
      this.featureContext = featureContext;
    }

    public IWebDriver Driver => this.featureContext.Driver();

    public IEnumerable<IWebElement> TwitterPlaceholder
      => Driver.FindElements(By.CssSelector("div .col-md-4 .bg-dark"));
  }
}
