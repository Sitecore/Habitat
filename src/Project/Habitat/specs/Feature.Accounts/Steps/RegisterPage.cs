namespace Sitecore.Feature.Accounts.Specflow.Steps
{
  using System;
  using System.Linq;
  using System.Net;
  using System.Runtime.Serialization.Json;
  using FluentAssertions;
  using Newtonsoft.Json;
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
        var c = new WebClient();
        c.Headers.Add(HttpRequestHeader.Accept, "application/json");
        var searchResult = JsonConvert.DeserializeObject<SearchEntity>(c.DownloadString(Settings.SearchContactUrl + row["email"]));
        var contactID = searchResult.Data.ContactSearchResults.First(x => x.PreferredEmailAddress == row["email"]).ContactId;
        var outcomeUrl =  Settings.BaseUrl + "/sitecore/api/ao/proxy/contacts/{contactID}/intel/outcome-detail";

        var outcomes = JsonConvert.DeserializeObject<SearchEntity>(c.DownloadString(outcomeUrl));
        outcomes.Data.Dataset.OutcomeDetail.Any(x => x.OutcomeDefinitionDisplayName == row["Outcome value"]).Should().BeTrue();

      }
     
    }

    public class SearchEntity
    {
      public Data Data { get; set; }

    }

  }

  internal class Data
  {
    public  ContactSearchResult[] ContactSearchResults { get; set; }
    public  Dataset Dataset { get; set; }
  }

  internal class OutcomeDetail
  {
    public Guid ContactID { get; set; }
    public string OutcomeDefinitionDisplayName { get; set; }
    public string OutcomeCategoryDisplayName { get; set; }

  }

  internal class Dataset
  {
    public OutcomeDetail[] OutcomeDetail { get; set; }
  }

  internal class ContactSearchResult
  {
    public Guid ContactId { get; set; }
    public string PreferredEmailAddress { get; set; }
  }
}