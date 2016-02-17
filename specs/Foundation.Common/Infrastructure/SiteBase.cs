using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using TechTalk.SpecFlow;

namespace Sitecore.Foundation.Common.Specflow.Infrastructure
{
  public class SiteBase
  {
    public IWebElement UserIcon => Driver.FindElement(By.CssSelector(".fa-user"));

    public IWebDriver Driver => FeatureContext.Current.Get<IWebDriver>();

    public IWebElement SiteSwitcherIcon
      => Driver.FindElement(By.CssSelector(".fa.fa-home"));

    public IEnumerable<IWebElement> SiteSwitcherIconDropDownChildElements
    {
      get
      {
        var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
        var locator = By.XPath(".//*[@id='primary-navigation']/ul/li[6]/ul/li");
        return wait.Until(ExpectedConditions.VisibilityOfAllElementsLocatedBy(locator));
      }
    }

    public IWebElement DemoSiteLogo
      => Driver.FindElement(By.CssSelector("#hplogo"));
  }
}
