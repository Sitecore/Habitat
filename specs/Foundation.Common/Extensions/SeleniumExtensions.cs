using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace Sitecore.Foundation.Common.Specflow.Extensions
{
  

  public static class SeleniumExtensions
  {
    public static void MoveToElement(this IWebElement element)
    {
      Actions action = new Actions(element.GetDriver());
      action.MoveToElement(element).Perform();
    }
    public static IWebDriver GetDriver(this IWebElement element)
    {
      return element.GetType().GetProperty("WrappedDriver").GetValue(element) as IWebDriver;
    }

    public static IWebElement GetField(this IEnumerable<IWebElement> elements, string formField)
    {
            return elements.First(x => x.GetAttribute("name") == formField);
       
    }
    public static IWebElement WaitUntilElementPresent(this IWebDriver driver, By selector)
    {
      var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
      wait.Until(ExpectedConditions.ElementExists(selector));

      return driver.FindElement(selector);
    }

    public static IEnumerable<IWebElement> WaitUntilElementsPresent(this IWebDriver driver, By selector)
    {
      var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
      wait.Until(ExpectedConditions.ElementExists(selector));

      return driver.FindElements(selector);
    }

    public static IEnumerable<IWebElement> WaitUntilElementsInvisible(this IWebDriver driver, By selector)
    {
      var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
      wait.Until(ExpectedConditions.InvisibilityOfElementLocated(selector));

      return driver.FindElements(selector);
    }

    public static IEnumerable<IWebElement> WaitUntilElementsVisible(this IWebDriver driver, By selector)
    {
      var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
      wait.Until(ExpectedConditions.ElementIsVisible(selector));

      return driver.FindElements(selector);
    }
  }
}
