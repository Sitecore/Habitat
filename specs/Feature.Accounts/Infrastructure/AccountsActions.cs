using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using OpenQA.Selenium;
using Sitecore.Feature.Accounts.Specflow.Steps;
using Sitecore.Foundation.Common.Specflow.Infrastructure;
using TechTalk.SpecFlow;

namespace Sitecore.Feature.Accounts.Specflow.Infrastructure
{
  [Binding]
  public static class AccountsActions
  {
    public static IWebDriver Driver => FeatureContext.Current.Get<IWebDriver>();

    public static void OpenUserDialog()
    {
      new CommonLocators().UserIcon.Click(); 
    }
  }
}
