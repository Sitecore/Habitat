using Sitecore.Feature.Accounts.Specflow.Infrastructure;
using Sitecore.Foundation.Common.Specflow.Infrastructure;

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
      textMessages.All(m => AccountLocators.AccountErrorMessages.Any(x => x.Text == m)).Should().BeTrue();
    }

    [When(@"Actor clicks (.*) button on Reset Password page")]
    public void WhenActorClicksResetPasswordButtonOnResetPasswordPage(string btn)
    {
      var button = CommonLocators.LoginPageButtons.First(el => el.GetAttribute("value").Equals(btn, StringComparison.CurrentCultureIgnoreCase));
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

    [Then(@"Following buttons present on Forgot Password page")]
    public void ThenFollowingButtonsPresentOnForgotPasswordPage(Table table)
    {
      var buttons = table.Rows.Select(x => x.Values.First());
      buttons.All(b => CommonLocators.LoginPageButtons.Any(x => x.GetAttribute("value") == b)).Should().BeTrue();
    }


    [Then(@"Following buttons is no longer present on Forgot Password page")]
    public void ThenThenFollowingButtonsIsNoLongerPresentOnForgotPasswordPage(Table table)
    {
      var buttons = table.Rows.Select(x => x.Values.First());
      buttons.All(b => CommonLocators.LoginPageButtons.Any(x => x.GetAttribute("value") == b)).Should().BeFalse();
    }

    [Then(@"Following fields is no longer present on Forgot Password page")]
    public void ThenFollowingFieldsIsNoLongerPresentOnForgotPasswordPage(Table table)
    {
      var fields = table.Rows.Select(x => x.Values.First());
      fields.All(f => Site.LoginFormFields.Any(x => x.Text == f)).Should().BeFalse();
    }
  }
}