using System.Collections.Generic;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Sitecore.Feature.Specflow.Infrastructure
{
  [Binding]
  public class PersonLocators
  {
    public static IWebDriver Driver => FeatureContext.Current.Get<IWebDriver>();

    public IEnumerable<IWebElement> EmployeesPerson
      => Driver.FindElements(By.CssSelector(".mosaic-overlay"));
  }
}
