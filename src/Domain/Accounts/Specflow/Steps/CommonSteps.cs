using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Habitat.Accounts.Specflow.Infrastructure;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;
using TechTalk.SpecFlow;
using Xunit;

namespace Habitat.Accounts.Specflow.Steps
{
    [Binding]
    public class CommonSteps : StepsBase
    {
        [Given(@"Habitat website is opened on Main Page")]

        public void GivenHabitatWebsiteIsOpenedOnMainPage()
        {
            Driver.Navigate().GoToUrl(Settings.BaseUrl);
        }

        [When(@"Actor moves cursor over the User icon")]
        public void WhenActorMovesCursorOverTheUserIcon()
        {
            Site.UserIcon.MoveToElement();

#warning hack for selenium hover behavoiur
            var dropdown = Site.UserIcon.FindElement(By.XPath("../../ul"));
            var js = Driver as IJavaScriptExecutor;
            js?.ExecuteScript("arguments[0].style.display='block'", dropdown);
        }

        [When(@"User selects (.*) from drop-down menu")]
        public void WhenUserSelectsREGISTERFromDrop_DownMenu(string linkText)
        {
            Driver.FindElement(By.LinkText(linkText.ToUpperInvariant())).Click();
        }

        [Then(@"Habitat website is opened on Main Page (.*)")]
        [Then(@"Page URL ends on (.*)")]
        public void ThenPageUrlEndsOnExpected(string urlEnding)
        {
            Driver.Url.EndsWith(urlEnding).Should().BeTrue();
        }

        [Then(@"(.*) title presents on page")]
        public void ThenRegisterTitlePresentsOnPage(string title)
        {
            Site.PageTitle.Text.Should().BeEquivalentTo(title);

        }

        [Then(@"(.*) button presents")]
        public void ThenRegisterButtonPresents(string btn)
        {
            Site.SubmitButton.Text.Should().BeEquivalentTo(btn);
        }

        [Then(@"Register fields present on page")]
        public void ThenRegisterFieldsPresentOnPage(Table table)
        {
            var fields = table.Rows.Select(x => x.Values.First());
            var elements = Site.FormFields.Select(el => el.GetAttribute("name"));
            elements.Should().Contain(fields);
        }

        [Then(@"Following buttons present under User drop-drop down menu")]
        public void ThenFollowingButtonsPresentUnderUserDrop_DropDownMenu(Table table)
        {

            var buttons = table.Rows.Select(x => x.Values.First());
            //1
            foreach (var button in buttons)
            {
                var found = false;
                foreach (var webElement in Site.DropDownButtons)
                {
                    found = webElement.Text == button;
                    if (found)
                    {
                        break;
                    }
                }
                found.Should().BeTrue();
            }
            //2
//            buttons.All(b => Site.DropDownButtons.Any(x => x.Text == b)).Should().BeTrue();

            
        }
        [Then(@"Following buttons is no longer present under User drop-drop down menu")]
        public void ThenFollowingButtonsIsNoLongerPresentUnderUserDrop_DropDownMenu(Table table)
        {
            var buttons = table.Rows.Select(x => x.Values.First());
            //1
            foreach (var button in buttons)
            {
                var found = false;
                foreach (var webElement in Site.DropDownButtons)
                {
                    found = webElement.Text == button;
                    if (found)
                    {
                        break;
                    }
                }
                found.Should().BeFalse();
            }


        }

        [When(@"Actor enters following data in to the register fields")]
        public void WhenActorEntersFollowingDataInToTheRegisterFields(Table table)
        {
            var row = table.Rows.First();
            foreach (var key in row.Keys)
            {
                Site.FormFields.GetField(key).SendKeys(row[key]);
            }
        }
        [Given(@"User with following data is registered")]
        public void GivenUserWithFollowingDataIsRegistered(Table table)
        {
            Driver.Navigate().GoToUrl(Settings.RegisterPageUrl);
            WhenActorEntersFollowingDataInToTheRegisterFields(table);
            Site.SubmitButton.Click();
            WhenActorMovesCursorOverTheUserIcon();
            WhenUserSelectsREGISTERFromDrop_DownMenu("Logout");
        }


    }
}
