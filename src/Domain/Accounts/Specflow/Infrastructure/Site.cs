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
    }
}