using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.Feature.Accounts.Specflow.Steps
{
  using FluentAssertions;
  using Sitecore.Foundation.Common.Specflow.Extensions;
  using TechTalk.SpecFlow;

  class EditUserProfile: AccountStepsBase
  {
    [Given(@"Edit profile page is opened")]
    public void GivenEditProfilePageIsOpened()
    {
      Driver.Navigate().GoToUrl(Settings.EditUserProfileUrl);
    }



    [When(@"User inputs data in to the fields")]
    public void WhenUserInputsDataInToTheFields(Table table)
    {

      var row = table.Rows.First();
      foreach (var key in row.Keys)
      {
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
      }
    }






  }
}
