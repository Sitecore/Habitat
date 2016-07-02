using OpenQA.Selenium.Interactions;
using Sitecore.Foundation.Common.Specflow.Infrastructure;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Sitecore.Foundation.Common.Specflow.Steps
{
  

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

    public static CommonLocators CommonLocators => new CommonLocators();

   
    

    public static void DeleteAllBrowserCookies()
    {
      Driver.Manage().Cookies.DeleteAllCookies();
    }


  }
}