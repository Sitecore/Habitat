using System.Linq;
using FluentAssertions;
using OpenQA.Selenium.Support.UI;
using Sitecore.Foundation.Common.Specflow.Extensions;
using Sitecore.Foundation.Common.Specflow.Infrastructure;
using TechTalk.SpecFlow;

namespace Sitecore.Feature.Accounts.Specflow.Steps
{
  internal class EditUserProfile : AccountStepsBase
  {
    [When(@"Edit profile page is opened")]
    [Given(@"Edit profile page is opened")]
    public void GivenEditProfilePageIsOpened()
    {
      Driver.Navigate().GoToUrl(BaseSettings.EditUserProfileUrl);
    }


    [When(@"User inputs data in to the fields")]
    public void WhenUserInputsDataInToTheFields(Table table)
    {
      var row = table.Rows.First();
      foreach (var key in row.Keys)
      {
        Site.EditUserProfileTextFields.GetField(key).Clear();
        Site.EditUserProfileTextFields.GetField(key).SendKeys(row[key]);
      }
    }

    [When(@"User updates data in to the fields")]
    public void WhenUserUpdatesDataInToTheFields(Table table)
    {
      var row = table.Rows.First();
      foreach (var key in row.Keys)
      {
        Site.EditUserProfileTextFields.GetField(key).Clear();
        Site.EditUserProfileTextFields.GetField(key).SendKeys(row[key]);
      }
    }


    [Given(@"User clicks (.*) button on Edit User Profile page")]
    [When(@"User clicks (.*) button on Edit User Profile page")]
    public void WhenUserClicksUpdateButtonOnEditUserProfilePage(string btn)
    {
      Site.SubmitButtons.Single(x => x.GetAttribute("value") == btn).Click();
    }

    [Given(@"Following fields were updated in User Profile")]
    public void GivenFollowingFieldsWereUpdatedInUserProfile(Table table)
    {
      GivenEditProfilePageIsOpened();
      var row = table.Rows.First();
      foreach (var key in row.Keys)
      {
        Site.EditUserProfileTextFields.GetField(key).SendKeys(row[key]);
      }
    }

    [When(@"User updates following fields in User Profile")]
    public void WhenUserUpdatesFollowingFieldsInUserProfile(Table table)
    {
      GivenEditProfilePageIsOpened();
      var row = table.Rows.First();
      foreach (var key in row.Keys)
      {
        Site.EditUserProfileTextFields.GetField(key).Clear();
        Site.EditUserProfileTextFields.GetField(key).SendKeys(row[key]);
      }
    }

    [When(@"User selects (.*) from Interests drop-down list")]
    public void WhenUserSelectsFromInterestsDrop_DownList(string interest)
    {
      var dropDownListBox = Site.InterestsDropDownElement;
      var clickThis = new SelectElement(dropDownListBox);
      clickThis.SelectByText(interest);
    }


    [Given(@"User inputs data on User Profile page and clicks (.*) button")]
    public void GivenUserInputsDataOnUserProfilePage(string btn, Table table)
    {
      GivenEditProfilePageIsOpened();
      var row = table.Rows.First();
      var keysArray = row.Keys.ToArray();
      for (var i = 0; i < 3; i++)
      {
        Site.EditUserProfileTextFields.GetField(keysArray[i]).Clear();
        Site.EditUserProfileTextFields.GetField(keysArray[i]).SendKeys(row[i]);
      }

      var dropDownListBox = Site.InterestsDropDownElement;
      var clickThis = new SelectElement(dropDownListBox);
      var interest = row["Interests"];
      clickThis.SelectByText(interest);

      Site.SubmitButtons.Single(x => x.GetAttribute("value") == btn).Click();
    }

    [Given(@"All User profile fields are empty")]
    public void GivenAllUserProfileFieldsAreEmpty(Table table)
    {
      var row = table.Rows.First();
      var keysArray = row.Keys.ToArray();
      for (var i = 0; i < 3; i++)
      {
        Site.EditUserProfileTextFields.GetField(keysArray[i]).Clear();
      }

      var dropDownListBox = Site.InterestsDropDownElement;
      var clickThis = new SelectElement(dropDownListBox);

      clickThis.AllSelectedOptions.Clear();


      var btn = "Update";
      Site.SubmitButtons.Single(x => x.GetAttribute("value") == btn).Click();

      //      var interest = row["Interests"];
      //      clickThis.SelectByText(interest);
    }


    [Then(@"Following User info presents for (.*) in User Profile")]
    public void ThenFollowingUserInfoPresentsInUserProfile(string userName, Table table)
    {
      var profileProperties =
        ContextExtensions.HelperService.GetUserProperties($"extranet\\{userName}")
          .ToDictionary(x => x.Key, v => v.Value);
      foreach (var row in table.Rows)
      {
        foreach (var key in row.Keys)
        {
          if (row[key] == "@empty")
          {
            profileProperties[key].Should().BeNullOrEmpty();
          }
          else
          {
            profileProperties[key].Should().Be(row[key]);
          }
        }
      }
    }

    [Then(@"System shows following error message for the Edit Profile")]
    public void ThenSystemShowsFollowingErrorMessageForTheEditProfile(Table table)
    {
      var textMessages = table.Rows.Select(x => x.Values.First());

      foreach (var textMessage in textMessages)
      {
        var found = false;
        foreach (var webElement in Site.PageErrorMessages)
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