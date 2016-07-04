using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using Sitecore.Foundation.Common.Specflow.Extensions;
using TechTalk.SpecFlow;

namespace Sitecore.Foundation.Forms.Specflow.Infrostructure
{
  public class FormLocators
  {
    public static IWebDriver Driver => FeatureContext.Current.Get<IWebDriver>();

    public IWebElement LeaveEmailFormField
      => Driver.FindElement(By.CssSelector("#wffm3ac88418720c4072876e261d634c6e0e_Sections_0__Fields_0__Value"));

    public IWebElement SubmitButton
      => Driver.WaitUntilElementsPresent(By.CssSelector(".btn.btn-default")).First(el =>el.GetAttribute("type").Equals("submit"));

  }
}
