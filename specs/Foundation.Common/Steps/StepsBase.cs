using OpenQA.Selenium.Interactions;

namespace Sitecore.Foundation.Common.Specflow.Steps
{
  using OpenQA.Selenium;
  using TechTalk.SpecFlow;

  public class StepsBase
  {
    public static IWebDriver Driver
    {
      get
      {
        return FeatureContext.Current.Get<IWebDriver>();
      }
      set
      {
        FeatureContext.Current.Set(value);
      }
    }

    public static Actions DriverActions
    {
      get { return FeatureContext.Current.Get<Actions>(); }

      set { FeatureContext.Current.Set(value); }
    }

    public void Cleanup()
    {
      new CommonGlobalSteps().Cleanup();
    }

  }
}