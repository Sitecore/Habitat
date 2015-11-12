using System.Linq;
using FluentAssertions;
using Habitat.Accounts.Specflow.Infrastructure;
using TechTalk.SpecFlow;

namespace Habitat.Accounts.Specflow.Steps
{
    [Binding]
    internal class RegisterPage : StepsBase
    {
        [Given(@"Habitat website is opened on Register page")]
        public void GivenHabitatWebsiteIsOpenedOnRegisterPage()
        {
            Driver.Navigate().GoToUrl(Settings.RegisterPageUrl);
        }

      

        [When(@"Actor clicks (.*) button")]
        public void WhenActorClicksRegisterButton(string btn)
        {
            Site.SubmitButton.Click();
        }

        [Then(@"System shows following message for the Email field")]
        public void ThenSystemShowsFollowingMessageForTheEmailField(Table table)
        {
            var textMessages = table.Rows.Select(x => x.Values.First());

            foreach (var textMessage in textMessages)
            {
                var found = false;
                foreach (var webElement in Site.AccountErrorMessages)
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
       


    }
}