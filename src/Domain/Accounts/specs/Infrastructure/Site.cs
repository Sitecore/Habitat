using System.Collections.Generic;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Habitat.Accounts.Specflow.Infrastructure
{
  using System.Linq;

  public class Site
    {
        public IWebElement UserIcon => Driver.FindElement(By.CssSelector(".fa-user"));

        public IWebDriver Driver => FeatureContext.Current.Get<IWebDriver>();

        public IWebElement PageTitle => Driver.FindElement(By.CssSelector(".section-title"));

        public IWebElement SubmitButton => Driver.FindElement(By.CssSelector("input[type=submit]"));

        public IWebElement SubmitLink => this.Driver.FindElement(By.CssSelector("a.btn.btn-link"));

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
        => this.Driver.FindElements(By.CssSelector("#loginEmail, #loginPassword"));

         public IWebElement LoginFormTitle => Driver.FindElement(By.CssSelector("#myModalLabel"));

        public IEnumerable<IWebElement> LoginFormButtons => Driver.FindElements(By.CssSelector(".btn.btn-default, .btn.btn-primary"));

        public IEnumerable<IWebElement> LoginFormErrorMessages
            =>
                this.Driver.FindElements(By.CssSelector(".field-validation-error"));

        public IWebElement LoginFormPopup
            => this.Driver.FindElement(By.CssSelector(".modal-header"));

        public IWebElement LoginPageTitle 
            => this.Driver.FindElement(By.CssSelector(".section-title h1"));

        public IEnumerable<IWebElement> LoginPageContainer
            => this.Driver.FindElements(By.CssSelector(".col-sm-4 form input"));

        public IEnumerable<IWebElement> LoginPageButtons
            => this.Driver.FindElements(By.CssSelector(".btn.btn-default"));

      public IEnumerable<IWebElement> LoginPageLinks
        => this.Driver.FindElements(By.CssSelector("a.btn.btn-link")).Where(el=> el.Displayed);

      public IEnumerable<IWebElement> LoginPageErrorMessages
          =>
              this.Driver.FindElements(By.CssSelector(".field-validation-error.help-block"));

      public IEnumerable<IWebElement> UserIconDropDownButtonLinks
          => this.Driver.FindElements(By.CssSelector(".btn.btn-default"));

    }
}