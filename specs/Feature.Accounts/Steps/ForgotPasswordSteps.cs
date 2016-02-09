namespace Sitecore.Feature.Accounts.Specflow.Steps
{
  using System;
  using System.Linq;
  using FluentAssertions;
  using TechTalk.SpecFlow;

  internal class ForgotPasswordSteps : AccountStepsBase
  {
    [Then(@"(.*) title presents on ForgotPassword page")]
    public void ThenPasswordResetTitlePresentsOnForgotPasswordPage(string title)
    {
      Site.PageTitle.Text.Should().BeEquivalentTo(title);
    }

    [Then(@"Forgot password form contains message to user")]
    public void ThenForgotPasswordFormContainsMessageToUser(Table table)
    {
      var fields = table.Rows.Select(x => x.Values.First());
      var elements = Site.PageHelpBlock.Select(el => el.Text);
      elements.Should().Contain(fields);
    }

    [Then(@"System shows following error message for the E-mail field")]
    public void ThenSystemShowsFollowingErrorMessageForTheEMailField(Table table)
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

    [When(@"Actor clicks (.*) button on Reset Password page")]
    public void WhenActorClicksResetPasswordButtonOnResetPasswordPage(string btn)
    {
      var button = Site.LoginPageButtons.First(el => el.Text.Equals(btn, StringComparison.CurrentCultureIgnoreCase) || el.GetAttribute("value").Equals(btn, StringComparison.CurrentCultureIgnoreCase));
      button.Click();
    }

    [When(@"Actor enters following data into E-mail field")]
    public void WhenActorEntersFollowingDataIntoEMailField(Table table)
    {
      var row = table.Rows.First();
      foreach (var key in row.Keys)
      {
        Site.RegisterEmail.SendKeys(row[key]);
      }
    }

    [Then(@"Systen shows following Alert message")]
    public void ThenSystenShowsFollowingAlertMessage(Table table)
    {
      var alertMessage = table.Rows.Select(el => el.Values.First());
      Site.PageAlertSuccessfullInfo.Text.Should().Contain(alertMessage.First());
    }

    [Then(@"Then Following buttons is no longer present on Forgot Password page")]
    public void ThenThenFollowingButtonsIsNoLongerPresentOnForgotPasswordPage(Table table)
    {
      var buttons = table.Rows.Select(x => x.Values.First());
      //1
      foreach (var button in buttons)
      {
        var found = false;
        foreach (var webElement in Site.LoginPageButtons)
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
        foreach (var webElement in Site.LoginFormFields)
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