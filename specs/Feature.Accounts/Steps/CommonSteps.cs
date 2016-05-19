using Sitecore.Feature.Accounts.Specflow.Infrastructure;

namespace Sitecore.Feature.Accounts.Specflow.Steps
{
    using System;
    using System.Linq;
    using FluentAssertions;
    using OpenQA.Selenium;
    using Sitecore.Foundation.Common.Specflow.Extensions;
    using Sitecore.Foundation.Common.Specflow.Infrastructure;
    using TechTalk.SpecFlow;

    [Binding]
    internal class CommonSteps : AccountStepsBase
    {
        [When(@"User selects (.*) from drop-down menu")]
        public void WhenUserSelectsRegisterFromDropDownMenu(string linkText)
        {
            Driver.FindElement(By.LinkText(linkText.ToUpperInvariant())).Click();
        }

        [Then(@"User info is shown on User popup")]
        public void ThenUserInfoIsShownOnUserPopup(Table table)
        {
            var values = table.Rows.Select(x => x.Values.First());
            values.All(v => Site.ShowUserInfoPopupFields.Any(x => x.Text == v)).Should().BeTrue();
        }
        [Then(@"(.*) title presents on page")]
        public void ThenRegisterTitlePresentsOnPage(string title)
        {
            Site.PageTitle.Text.Should().BeEquivalentTo(title);
        }

        [Then(@"(.*) title is no longer present on page")]
        public void ThenLoginTitleIsNoLongerPresentOnPage(string title)
        {
            Site.PageTitle.Text.Should().NotBe(title);
        }


        [Then(@"(.*) button presents")]
        public void ThenRegisterButtonPresents(string btn)
        {
            SiteBase.SubmitButton.GetAttribute("Value").Should().BeEquivalentTo(btn);
        }

        [Then(@"Register fields present on page")]
        public void ThenRegisterFieldsPresentOnPage(Table table)
        {
            var fields = table.Rows.Select(x => x.Values.First());
            var elements = CommonLocators.RegisterPageFields.Select(el => el.GetAttribute("name"));
            elements.Should().Contain(fields);
        }

        [Then(@"Following buttons present under User icon")]
        public void ThenFollowingButtonsPresentUnderUserDropDropDownMenu(Table table)
        {
            var buttons = table.Rows.Select(x => x.Values.First());
            buttons.All(b => CommonLocators.UserIconButtons.Any(x => x.Text == b)).Should().BeTrue();

        }

        [Then(@"Following buttons is no longer present under User icon")]
        public void ThenFollowingButtonsIsNoLongerPresentUnderUserDropDropDownMenu(Table table)
        {
            var buttons = table.Rows.Select(x => x.Values.First());
            buttons.All(b => CommonLocators.UserIconButtons.Any(x => x.Text == b)).Should().BeFalse();
        }

        [Then(@"User info is not shown on User popup")]
        public void ThenUserInfoIsNotShownOnUserPopup(Table table)
        {
            var fields = table.Rows.Select(x => x.Values.First());
            fields.All(f => Site.EditUserProfileTextFields.Any(x => x.Text == f)).Should().BeFalse();
        }




        [When(@"Actor enters following data in to the register fields")]
        public void WhenActorEntersFollowingDataInToTheRegisterFields(Table table)
        {
            var row = table.Rows.First();
            foreach (var key in row.Keys)
            {
                CommonLocators.RegisterPageFields.GetField(key).SendKeys(row[key]);
            }
            //Following code will remove create user from DB after use case ends
            ContextExtensions.CleanupPool.Add(new TestCleanupAction
            {
                ActionType = ActionType.RemoveUser,
                Payload = "extranet\\" + row["Email"]
            });
        }
        [When(@"Actor enters following data in to the register fields with misssed email")]
        public void WhenActorEntersFollowingDataInToTheRegisterFieldsWithMisssedEmail(Table table)
        {
            var row = table.Rows.First();
            foreach (var key in row.Keys)
            {
                CommonLocators.RegisterPageFields.GetField(key).SendKeys(row[key]);
            }
        }
        [Given(@"User with following data is registered")]
        public void GivenUserWithFollowingDataIsRegistered(Table table)
        {
            Driver.Navigate().GoToUrl(BaseSettings.RegisterPageUrl);
            WhenActorEntersFollowingDataInToTheRegisterFields(table);
            SiteBase.SubmitButton.Click();
            //TODO: change with click user item
            //new SiteNavigationSteps().WhenActorMovesCursorOverTheUserIcon();
            WhenUserSelectsRegisterFromDropDownMenu("Logout");
        }

        [Given(@"User with following data is registered in Habitat")]
        public void GivenUserWithFollowingDataIsRegisteredInHabitat(Table table)
        {
            Driver.Navigate().GoToUrl(BaseSettings.RegisterPageUrl);
            WhenActorEntersFollowingDataInToTheRegisterFields(table);
            SiteBase.SubmitButton.Click();

            table.Rows
              .Select(row => row["Email"]).ToList()
              .ForEach(email =>
              {
                  ContextExtensions.CleanupPool.Add(new TestCleanupAction
                  {
                      ActionType = ActionType.RemoveUser,
                      Payload = email
                  });
              });
        }

        [Given(@"Session was expired")]
        public void GivenSessionWasExpired()
        {
            Driver.FindElement(By.CssSelector("body")).SendKeys(Keys.Control + 't');
            Driver.Navigate().GoToUrl(BaseSettings.EndSessionUrl);
            Driver.FindElement(By.CssSelector("body")).SendKeys(Keys.Control + 'w');
        }


        [When(@"User clicks Log out on User Icon")]
        [Given(@"User was logged out from the Habitat")]
        public void GivenUserWasLoggedOutFromTheHabitat()
        {
            //TODO: change with click user item
            //new SiteNavigationSteps().WhenActorMovesCursorOverTheUserIcon();
            SiteBase.SubmitButton.Click();
        }


        [Then(@"Login drop-down popup is no longer presents")]
        public void ThenLoginPopupIsNoLongerPresents()
        {
            var element = Site.UserFormDropDownPopup;

            element.Displayed.Should().BeFalse();
        }

        [Then(@"Following links present under User popup")]
        public void ThenFollowingLinksPresentUnderUserDropDropDownMenu(Table table)
        {
            var buttonsLinks = table.Rows.Select(x => x.Values.First());
            buttonsLinks.All(l => CommonLocators.UserIconDropDownButtonLinks.Any(x => x.Text == l)).Should().BeTrue();
        }

        [Then(@"Habitat Main page presents")]
        public void ThenHabitatMainPagePresents()
        {
            var absoluteUri = new Uri(Driver.Url).AbsolutePath;
            (absoluteUri.Equals("/") || absoluteUri.Equals("/en")).Should().BeTrue();
        }

        [Then(@"Following links is no longer present under User popup")]
        public void ThenFollowingLinksIsNoLongerPresentUnderUserPopup(Table table)
        {
            var buttonsLinks = table.Rows.Select(x => x.Values.First());
            buttonsLinks.All(l => CommonLocators.UserIconDropDownButtonLinks.Any(x => x.Text == l)).Should().BeFalse();
        }



    }
}