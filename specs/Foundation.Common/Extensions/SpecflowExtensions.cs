namespace Sitecore.Foundation.Common.Specflow.Extensions
{
  using OpenQA.Selenium;
  using OpenQA.Selenium.Firefox;
  using TechTalk.SpecFlow;

  public static class SpecflowExtensions
  {
    public static T GetOrAdd<T>(this ScenarioContext scenarioContext) where T : new()
    {
      if (!scenarioContext.ContainsKey(typeof(T).FullName))
      {
        scenarioContext.Set(new T());
      }

      return scenarioContext.Get<T>();
    }


    public static IWebDriver Driver(this FeatureContext scenarioContext)
    {
      if (!scenarioContext.ContainsKey(typeof(IWebDriver).FullName))
      {
        scenarioContext.Set((IWebDriver)new FirefoxDriver());
      }

      return scenarioContext.Get<IWebDriver>();
    }

  }
}