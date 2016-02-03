namespace Sitecore.Feature.Accounts.Specflow.Steps
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Net;
  using System.Runtime.Serialization.Json;
  using System.Threading;
  using FluentAssertions;
  using Newtonsoft.Json;
  using Sitecore.Feature.Accounts.Specflow.Infrastructure;
  using Sitecore.Foundation.Common.Specflow.Infrastructure;
  using TechTalk.SpecFlow;

  [Binding]
  internal class RegisterPage : AccountStepsBase
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


    [Then(@"User Outcome contains value")]
    public void ThenUserOutcomeContainsValue(Table table)
    {
      foreach (var row in table.Rows)
      {
        var email = row["email"];

        var contactID = GetContactId(email);
        var queryUrl = Settings.BaseUrl + $"/sitecore/api/ao/proxy/contacts/{contactID}/intel/outcome-detail";
        var outcomes = GetAnalytycsEntities<SearchEntity>(queryUrl);
        var expectedOutcome = row["Outcome value"];
        outcomes.Data.Dataset.OutcomeDetail
          .Any(x => x.OutcomeDefinitionDisplayName == expectedOutcome)
          .Should().Be(!string.IsNullOrEmpty(expectedOutcome));
      }

    }

    


    [Then(@"Following User info presents")]
    public void ThenFollowingUserInfoPresents(Table table)
    {



      foreach (var row in table.Rows)
      {
        var email = row["email"];
        var contactID = GetContactId(email);
        var queryUrl = Settings.BaseUrl + $"/sitecore/api/ao/proxy/contacts/{contactID}";

        var contact = GetAnalytycsEntities<ContactEntity>(queryUrl);


        var expectedFirstName = row["First Name"];
        contact.FirstName.Should().Be(expectedFirstName);

        var expectedLastName = row["Last Name"];
        contact.SurName.Should().Be(expectedLastName);

        var expectedPhoneNumber = row["Phone number"];


      }

    }




    [When(@"Wating for timeout (.*) s")]
    public void WhenWatingForTimeoutS(int seconds)
    {
      Thread.Sleep(TimeSpan.FromSeconds(seconds));
    }

    [Given(@"Outcome set to item field")]
    public void GivenOutcomeSetToItemField(IEnumerable<ItemFieldDefinition> table)
    {
      foreach (var item in table)
      {
        item.FieldValue = ResolveItemId(item.FieldValue);
      }
    }

    private string ResolveItemId(string fieldValue)
    {
      switch (fieldValue)
      {
        case "Outcomes/Sales Lead":
          return "{C2D9DFBC-E465-45FD-BA21-0A06EBE942D6}";


        default:
          return fieldValue;
      }
    }
  }


}