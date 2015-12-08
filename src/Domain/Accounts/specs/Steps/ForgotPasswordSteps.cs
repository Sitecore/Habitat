using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using FluentAssertions;

namespace Habitat.Accounts.Specflow.Steps
{
  using System.Security.Cryptography;
  using FluentAssertions;
  using Habitat.Accounts.Specflow.Infrastructure;

  class ForgotPasswordSteps : StepsBase
  {
    [Then(@"(.*) title presents on ForgotPassword page")]
    public void ThenPasswordResetTitlePresentsOnForgotPasswordPage(string title)
    {
      this.Site.PageTitle.Text.Should().BeEquivalentTo(title);
    }

    [Then(@"Forgot password form contains message to user")]
    public void ThenForgotPasswordFormContainsMessageToUser(Table table)
    {
      var fields = table.Rows.Select(x => x.Values.First());
      var elements = this.Site.PageHelpBlock.Select(el => el.Text);
      elements.Should().Contain(fields);
    }

    [Then(@"System shows following error message for the E-mail field")]
    public void ThenSystemShowsFollowingErrorMessageForTheE_MailField(Table table)
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

    [When(@"Actor clicks (.*) button on Reset Password page")]
    public void WhenActorClicksResetPasswordButtonOnResetPasswordPage(string btn)
    {
      var button = this.Site.LoginPageButtons.First(el => el.Text.Equals(btn, StringComparison.CurrentCultureIgnoreCase) || el.GetAttribute("value").Equals(btn, StringComparison.CurrentCultureIgnoreCase));
      button.Click();
    }

    [When(@"Actor enters following data into E-mail field")]
    public void WhenActorEntersFollowingDataIntoE_MailField(Table table)
    {
      var row = table.Rows.First();
      foreach (var key in row.Keys)
      {
        this.Site.RegisterEmail.SendKeys(row[key]);
      }
    }

    [Then(@"Systen shows following Alert message")]
    public void ThenSystenShowsFollowingAlertMessage(Table table)
    {
      var alertMessage = table.Rows.Select(el => el.Values.First());
      this.Site.PageAlertInfo.Text.Should().Contain(alertMessage.First());

    }

    [Then(@"Then Following buttons is no longer present on Forgot Password page")]
    public void ThenThenFollowingButtonsIsNoLongerPresentOnForgotPasswordPage(Table table)
    {
      var buttons = table.Rows.Select(x => x.Values.First());
      //1
      foreach (var button in buttons)
      {
        var found = false;
        foreach (var webElement in this.Site.LoginPageButtons)
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

    [Then(@"Following fields is no longer present on Forgot Password page")]
    public void ThenFollowingFieldsIsNoLongerPresentOnForgotPasswordPage(Table table)
    {
      var fields = table.Rows.Select(x => x.Values.First());
      //1
      foreach (var field in fields)
      {
        var found = false;
        foreach (var webElement in this.Site.LoginFormFields)
        {
          found = webElement.Text == field;
          if (found)
          {
            break;
          }
        }
        found.Should().BeFalse();
      }
    }
  }
}
