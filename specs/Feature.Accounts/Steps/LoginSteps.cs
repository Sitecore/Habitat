﻿using System.Collections.Generic;
using Sitecore.Feature.Accounts.Specflow.Infrastructure;
using Sitecore.Foundation.Common.Specflow.Infrastructure;

namespace Sitecore.Feature.Accounts.Specflow.Steps
{
  using System;
  using System.Linq;
  using FluentAssertions;
  using Sitecore.Foundation.Common.Specflow.Extensions;
  using TechTalk.SpecFlow;

  internal class LoginSteps : AccountStepsBase 
  {
    [Then(@"(.*) title presents on Login form")]
    public void ThenLoginTitlePresentsOnLoginForm(string title)
    {
      Site.LoginFormTitle.Text.ShouldBeEquivalentTo(title);
    }

    [Then(@"(.*) title presents on Login page")]
    public void ThenLoginTitlePresentsOnLoginPage(string title)
    {
      Site.LoginPageTitle.Text.ShouldBeEquivalentTo(title);
    }


    [Then(@"Following fields present on User form")]
    public void ThenFollowingFieldsPresentOnLoginForm(Table table)
    {
      var fields = table.Rows.Select(x => x.Values.First());
      var elements = Site.LoginFormFields.Select(el => el.GetAttribute("name"));
      elements.Should().Contain(fields);
    }


    [Given(@"Actor clicks Login button on User form")]
    [When(@"Actor clicks (.*) button on User form")]
    public void WhenUserClicksLoginButtonOnLoginForm(string btn)
    {
      var elements = CommonLocators.UserIconButtons.First(el => el.Text.Equals(btn, StringComparison.InvariantCultureIgnoreCase));
      elements.Click();
    }

    [Then(@"System shows following error message for the Login form")]
    public void ThenSystemShowsFollowingErrorMessageForTheLoginForm(Table table)
    {
      var textMessages = table.Rows.Select(x => x.Values.First());
      textMessages.All(m => AccountLocators.AccountErrorMessages.Any(x => x.Text == m)).Should().BeTrue();
    }

    [When(@"Actor enteres following data into Login form fields")]
    public void WhenActorEnteresFollowingDataIntoFields(Table table)
    {
      var row = table.Rows.First();
      foreach (var key in row.Keys)
      {
        var text = row[key];
        Site.LoginFormFields.GetField(key).SendKeys(text);
      }
    }

    

    [Then(@"Following fields present on Login page")]
    public void ThenFollowingFieldsPresentOnLoginPage(Table table)
    {
      var fields = table.Rows.Select(x => x.Values.First());
      var elements = Site.LoginPageFields.Select(el => el.GetAttribute("name"));
      elements.Should().Contain(fields);
    }

    [Then(@"Following buttons present on Login Page")]
    public void ThenFollowingButtonsPresentOnLoginPage(Table table)
    {
      var buttons = table.Rows.Select(x => x.Values.First());
      var elements = CommonLocators.LoginPageButtons.Select(el => el.GetAttribute("value"));
      elements.Should().Contain(buttons);
    }

    [Then(@"Following links present on Login Page")]
    public void ThenFollowingLinksPresentOnLoginPage(Table table)
    {
      var links = table.Rows.Select(x => x.Values.First());
      var elements = CommonLocators.LoginPageLinks.Select(el => el.Text);
      links.All(x => elements.Contains(x, StringComparer.InvariantCultureIgnoreCase)).Should().BeTrue();
    }

    [When(@"Actor clicks (.*) link")]
    public void WhenActorClicksForgotYourPasswordLink(string btnLink)
    {
      var forgotPasslink = CommonLocators.LoginPageLinks.First(el => el.Text.Equals(btnLink, StringComparison.InvariantCultureIgnoreCase));
      forgotPasslink.Click();
    }


    [When(@"User clicks (.*) button on Login page")]
    public void WhenUserClicksLoginButtonOnLoginPage(string btn)
    {
      var elements = CommonLocators.LoginPageButtons.First(el => el.GetAttribute("value").Contains(btn));
      elements.Click();
    }

    [Then(@"System shows following error message for the Login page")]
    public void ThenSystemShowsFollowingErrorMessageForTheLoginPage(Table table)
    {
      var textMessages = table.Rows.Select(x => x.Values.First());
      textMessages.All(m => AccountLocators.AccountErrorMessages.Any(x => x.Text == m));
    }

    [When(@"Actor enteres following data into Login page fields")]
    public void WhenActorEnteresFollowingDataIntoLoginPageFields(Table table)
    {
      var row = table.Rows.First();
      foreach (var key in row.Keys)
      {
        Site.LoginPageFields.GetField(key).SendKeys(row[key]);
      }
    }
    [Given(@"User was (.*) to Habitat")]
    public void GivenUserWasLoginToHabitat(string btn, Table table)
    {
      SiteBase.NavigateToPage(BaseSettings.LoginPageUrl);

      var row = table.Rows.First();
      foreach (var key in row.Keys)
      {
        Site.LoginPageFields.GetField(key).SendKeys(row[key]);
      }

      var elements = CommonLocators.LoginPageButtons.First(el => el.GetAttribute("value").Contains(btn));
      elements.Click();
    }


    [Given(@"Actor selects User icon on Navigation bar")]
    [When(@"Actor selects User icon on Navigation bar")]
    public void WhenActorSelectsUserIconOnNavigationBar()
    {
      AccountsActions.OpenUserDialog();
    }



  }
}