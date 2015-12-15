using System;
using System.Linq;
using FluentAssertions;
using Habitat.Accounts.Specflow.Infrastructure;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Habitat.Accounts.Specflow.Steps
{
    internal class LoginSteps : StepsBase
    {
        [Then(@"(.*) title presents on Login form")]
        public void ThenLoginTitlePresentsOnLoginForm(string title)
        {
            this.Site.LoginPageTitle.Text.ShouldBeEquivalentTo(title);
        }

        [Then(@"Following fields present on Login form")]
        public void ThenFollowingFieldsPresentOnLoginForm(Table table)
        {
            var fields = table.Rows.Select(x => x.Values.First());
            var elements = this.Site.LoginFormFields.Select(el => el.GetAttribute("name"));
            elements.Should().Contain(fields);
        }

        [Then(@"Following buttons present on Login Form")]
        public void ThenFollowingButtonsPresentOnLoginForm(Table table)
        {
            var buttons = table.Rows.Select(x => x.Values.First());
            var elements = this.Site.LoginFormButtons.Select(el => el.GetAttribute("text"));
        }

        [When(@"User clicks (.*) button on Login form")]
        public void WhenUserClicksLoginButtonOnLoginForm(string btn)
        {
            var elements = this.Site.LoginFormButtons.First(el => el.Text.Equals(btn, StringComparison.InvariantCultureIgnoreCase));
            elements.Click();
        }

        [Then(@"System shows following error message for the Login form")]
        public void ThenSystemShowsFollowingErrorMessageForTheLoginForm(Table table)
        {
            var textMessages = table.Rows.Select(x => x.Values.First());

            foreach (var textMessage in textMessages)
            {
                var found = false;
                foreach (var webElement in this.Site.LoginFormErrorMessages)
                {
                    found = webElement.Text == textMessage;
                    if (found)
                    {
                        break;
                    }
                }
                found.Should().BeTrue();
            }
        }

        [When(@"Actor enteres following data into fields")]
        public void WhenActorEnteresFollowingDataIntoFields(Table table)
        {
            var row = table.Rows.First();
            foreach (var key in row.Keys)
            {
                this.Site.LoginFormFields.GetField(key).SendKeys(row[key]);
            }
        }

        [When(@"Actor enteres following data into Login form fields")]
        public void WhenActorEnteresFollowingDataIntoLoginFormFields(Table table)
        {
            var row = table.Rows.First();
            foreach (var key in row.Keys)
            {
                this.Site.LoginFormFields.GetField(key).SendKeys(row[key]);
            }
        }


    }
}