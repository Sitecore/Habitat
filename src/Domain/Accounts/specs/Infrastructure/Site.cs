using System.Collections.Generic;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Habitat.Accounts.Specflow.Infrastructure
{
    public class Site
    {
        public IWebElement UserIcon => Driver.FindElement(By.CssSelector(".fa-user"));

        public IWebDriver Driver => FeatureContext.Current.Get<IWebDriver>();

        public IWebElement PageTitle => Driver.FindElement(By.CssSelector(".section-title"));

        public IWebElement SubmitButton => Driver.FindElement(By.CssSelector("input[type=submit]"));

        public IEnumerable<IWebElement> FormFields
            =>
                Driver.FindElements(
                    By.CssSelector(
                        ".page-layout input[type=text].form-control, .page-layout input[type=password].form-control"));

        public IEnumerable<IWebElement> DropDownButtons
            =>
                Driver.FindElements(By.CssSelector(".dropdown-menu input[type=submit]"));

        public IEnumerable<IWebElement> AccountErrorMessages
            =>
                Driver.FindElements(By.CssSelector(".field-validation-error.help-block"));

        public IEnumerable<IWebElement> LoginFormFields
            =>
                Driver.FindElements(By.CssSelector("#loginEmail, #loginPassword"));

        public IWebElement LoginPageTitle => Driver.FindElement(By.CssSelector("#myModalLabel"));

        public IEnumerable<IWebElement> LoginFormButtons => Driver.FindElements(By.CssSelector(".btn.btn-default, .btn.btn-primary"));

        public IEnumerable<IWebElement> LoginFormErrorMessages
            =>
                Driver.FindElements(By.CssSelector(".field-validation-error"));
    }
}