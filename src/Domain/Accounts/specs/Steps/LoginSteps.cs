using System;
using System.Linq;
using FluentAssertions;
using Habitat.Accounts.Specflow.Infrastructure;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Habitat.Accounts.Specflow.Steps
{
  using System.Net.Mime;

  internal class LoginSteps : StepsBase
    {
        [Then(@"(.*) title presents on Login form")]
        public void ThenLoginTitlePresentsOnLoginForm(string title)
        {
            this.Site.LoginFormTitle.Text.ShouldBeEquivalentTo(title);
        }

      [Then(@"(.*) title presents on Login page")]
      public void ThenLoginTitlePresentsOnLoginPage(string title)
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
            elements.Should().Contain(buttons);
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

        [Then(@"Following fields present on Login page")]
        public void ThenFollowingFieldsPresentOnLoginPage(Table table)
        {
            var fields = table.Rows.Select(x => x.Values.First());
            var elements = this.Site.LoginPageFields.Select(el => el.GetAttribute("name"));
            elements.Should().Contain(fields);
        }

        [Then(@"Following buttons present on Login Page")]
        public void ThenFollowingButtonsPresentOnLoginPage(Table table)
        {
            var buttons = table.Rows.Select(x => x.Values.First());
            var elements = this.Site.LoginPageButtons.Select(el => el.GetAttribute("value"));
            elements.Should().Contain(buttons);
        }

        [Then(@"Following links present on Login Page")]
        public void ThenFollowingLinksPresentOnLoginPage(Table table)
        {
            var links = table.Rows.Select(x => x.Values.First());
            var elements = this.Site.LoginPageLinks.Select(el => el.Text);
          links.All(x => elements.Contains(x, StringComparer.InvariantCultureIgnoreCase)).Should().BeTrue();
        }

        [When(@"Actor clicks (.*) link")]
        public void WhenActorClicksForgotYourPasswordLink(string btnLink)
        {
          var forgotPasslink = this.Site.LoginPageLinks.First(el => el.Text.Equals(btnLink, StringComparison.InvariantCultureIgnoreCase));
          forgotPasslink.Click();
          
        }



    [When(@"User clicks (.*) button on Login page")]
        public void WhenUserClicksLoginButtonOnLoginPage(string btn)
        {
            var elements = this.Site.LoginPageButtons.First(el => el.GetAttribute("value").Contains(btn));
            elements.Click();
        }

        [Then(@"System shows following error message for the Login page")]
            public void ThenSystemShowsFollowingErrorMessageForTheLoginPage(Table table)
        {
            var textMessages = table.Rows.Select(x => x.Values.First());

            foreach (var textMessage in textMessages)
            {
                var found = false;
                foreach (var webElement in this.Site.PageErrorMessages)
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

    [When(@"Actor enteres following data into Login page fields")]
    public void WhenActorEnteresFollowingDataIntoLoginPageFields(Table table)
    {
      var row = table.Rows.First();
      foreach (var key in row.Keys)
      {
        this.Site.LoginPageFields.GetField(key).SendKeys(row[key]);
      }
    }



  }
}