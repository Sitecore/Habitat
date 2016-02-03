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
    public void Cleanup()
    {
      new CommonGlobalSteps().Cleanup();
    }

  }
}