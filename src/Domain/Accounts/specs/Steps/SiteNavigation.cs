using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Habitat.Accounts.Specflow.Infrastructure;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Habitat.Accounts.Specflow.Steps
{
    class SiteNavigation: StepsBase
    {
        [Given(@"Habitat website is opened on Main Page")]

        public void GivenHabitatWebsiteIsOpenedOnMainPage()
        {
            Driver.Navigate().GoToUrl(Settings.BaseUrl);
        }
        [Given(@"Actor moves cursor over the User icon")]
        [When(@"Actor moves cursor over the User icon")]
        public void WhenActorMovesCursorOverTheUserIcon()
        {
            Site.UserIcon.MoveToElement();

#warning hack for selenium hover behavoiur
            var dropdown = Site.UserIcon.FindElement(By.XPath("../../ul"));
            var js = Driver as IJavaScriptExecutor;
            js?.ExecuteScript("arguments[0].style.display='block'", dropdown);
        }
    }
}
