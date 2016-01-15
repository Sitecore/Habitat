namespace Habitat.Accounts.Specflow.Infrastructure
{
  using System.Collections.Generic;
  using System.Linq;
  using OpenQA.Selenium;
  using TechTalk.SpecFlow;

  public class Site
  {
    public IWebElement UserIcon => Driver.FindElement(By.CssSelector(".fa-user"));

    public IWebDriver Driver => FeatureContext.Current.Get<IWebDriver>();

    public IWebElement PageTitle => Driver.FindElement(By.CssSelector(".section-title"));

    public IWebElement SubmitButton => Driver.FindElement(By.CssSelector("input[type=submit]"));

    public IWebElement SubmitLink => Driver.FindElement(By.CssSelector("a.btn.btn-link"));

    public IEnumerable<IWebElement> FormFields
      =>
        Driver.FindElements(
          By.CssSelector(
            ".page-layout input[type=text].form-control, .page-layout input[type=password].form-control"));

    public IEnumerable<IWebElement> UserIconDropDownButtons
      =>
        Driver.FindElements(By.CssSelector(".dropdown-menu input[type=submit]"));

    public IEnumerable<IWebElement> AccountErrorMessages
      =>
        Driver.FindElements(By.CssSelector(".field-validation-error.help-block"));

    public IEnumerable<IWebElement> LoginFormFields
      =>
        Driver.FindElements(By.CssSelector("#popupLoginEmail, #popupLoginPassword"));

    public IEnumerable<IWebElement> LoginPageFields
      => Driver.FindElements(By.CssSelector("#loginEmail, #loginPassword"));

    public IWebElement LoginFormTitle => Driver.FindElement(By.CssSelector("#myModalLabel"));

    public IEnumerable<IWebElement> LoginFormButtons => Driver.FindElements(By.CssSelector(".btn.btn-default, .btn.btn-primary"));

    public IEnumerable<IWebElement> LoginFormErrorMessages
      =>
        Driver.FindElements(By.CssSelector(".field-validation-error"));

    public IWebElement LoginFormPopup
      => Driver.FindElement(By.CssSelector(".modal-header"));

    public IWebElement LoginPageTitle
      => Driver.FindElement(By.CssSelector(".section-title h1"));

    public IEnumerable<IWebElement> LoginPageContainer
      => Driver.FindElements(By.CssSelector(".col-sm-4 form input"));

    public IEnumerable<IWebElement> LoginPageButtons
      => Driver.FindElements(By.CssSelector(".btn.btn-default")).Where(el => el.Displayed).ToList();

    public IEnumerable<IWebElement> LoginPageLinks
      => Driver.WaitUntilElementsPresent(By.CssSelector("a.btn.btn-link")).Where(el => el.Displayed).ToList();

    public IEnumerable<IWebElement> PageErrorMessages
      =>
        Driver.FindElements(By.CssSelector(".field-validation-error.help-block"));

    public IEnumerable<IWebElement> UserIconDropDownButtonLinks
      => Driver.FindElements(By.CssSelector(".btn.btn-default"));

    public IEnumerable<IWebElement> PageHelpBlock
      => Driver.FindElements(By.CssSelector(".help-block"));

    public IWebElement PageAlertInfo
      => Driver.WaitUntilElementPresent(By.CssSelector("div.alert.alert-info"));

    public IWebElement RegisterEmail
      => Driver.FindElement(By.Id("registerEmail"));

    public IWebElement ShowUserInfoPopupFields
      => Driver.FindElement(By.XPath(".//*[@id='primary-navigation']/ul/li[7]/ul/li[1]/div"));
  }
}