using System;
using OpenQA.Selenium.Support.UI;

namespace Sitecore.Feature.Accounts.Specflow.Infrastructure
{
  using System.Collections.Generic;
  using System.Linq;
  using System.Net.Mime;
  using OpenQA.Selenium;
  using Sitecore.Foundation.Common.Specflow.Extensions;
  using TechTalk.SpecFlow;

  public class AccountLocators
  {
    public static IWebDriver Driver => FeatureContext.Current.Get<IWebDriver>();

    public IWebElement PageTitle => Driver.FindElement(By.CssSelector(".container h1"));

    public IEnumerable<IWebElement> SubmitButtons => Driver.FindElements(By.CssSelector("input[type=submit]"));

    public IWebElement SubmitLink => Driver.FindElement(By.CssSelector("a.btn.btn-link"));

    public IEnumerable<IWebElement> FormFields
      =>
        Driver.FindElements(
          By.CssSelector(
            ".page-layout input[type=text].form-control, .page-layout input[type=password].form-control"));

    

    public static IEnumerable<IWebElement> AccountErrorMessages
      =>
        Driver.FindElements(By.CssSelector(".field-validation-error.help-block, .field-validation-error.alert.alert-danger"));

    public IEnumerable<IWebElement> LoginFormFields
      =>
        Driver.FindElements(By.CssSelector("#popupLoginEmail, #popupLoginPassword"));

    public IEnumerable<IWebElement> LoginPageFields
      => Driver.FindElements(By.CssSelector("#loginEmail, #loginPassword"));
   public IEnumerable<IWebElement> LoginPageFieldsBackend
  => Driver.FindElements(By.CssSelector("#UserName, #Password"));

        public IWebElement LoginFormTitle => Driver.FindElement(By.CssSelector(".dropdown-menu h4"));

    public IWebElement UserFormDropDownPopup
      => Driver.FindElement(By.CssSelector(".dropdown-menu"));

    public IWebElement LoginPageTitle
      => Driver.FindElement(By.CssSelector(".section-title h1"));

    public IEnumerable<IWebElement> LoginPageContainer
      => Driver.FindElements(By.CssSelector(".col-sm-4 form input"));
        
    public IEnumerable<IWebElement> PageHelpBlock
      => Driver.FindElements(By.CssSelector(".help-block"));

    public IWebElement PageAlertInfo
      => Driver.WaitUntilElementPresent(By.CssSelector("div.alert.alert-info"));

    public IWebElement PageAlertSuccessfullInfo
      => Driver.WaitUntilElementPresent(By.CssSelector(".alert.alert-success"));

    public IWebElement RegisterEmail
      => Driver.FindElement(By.Id("registerEmail"));

    public IEnumerable<IWebElement> ShowUserInfoPopupFields
      => Driver.FindElements(By.CssSelector(".control-label, .form-control-static"));

    public IEnumerable<IWebElement> EditUserProfileTextFields
      => Driver.FindElements(By.CssSelector("#firstName, #lastName, #phoneNumber"));

    public IEnumerable<IWebElement> EditUserProfileCheckBoxFields
      => Driver.FindElements(By.CssSelector("#interests"));

    public IWebElement InterestsDropDownElement
      => Driver.FindElement(By.CssSelector("#interests"));

    public IEnumerable<IWebElement> SiteSwitcherIconDropDownLinks
      => Driver.FindElements(By.XPath(".//*[@id='primary-navigation']/ul/li[6]/ul"));

    
  }
}