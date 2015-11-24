namespace Habitat.Accounts.Specflow.Steps
{
  using System;
  using System.Linq;
  using FluentAssertions;
  using Habitat.Accounts.Specflow.Infrastructure;
  using OpenQA.Selenium;
  using TechTalk.SpecFlow;

  [Binding]
  internal class CommonSteps : StepsBase
  {
    [When(@"User selects (.*) from drop-down menu")]
    public void WhenUserSelectsRegisterFromDropDownMenu(string linkText)
    {
      Driver.FindElement(By.LinkText(linkText.ToUpperInvariant())).Click();
    }

    [Then(@"Habitat website is opened on Main Page (.*)")]
    [Then(@"Page URL ends on (.*)")]
    public void ThenPageUrlEndsOnExpected(string urlEnding)
    {
      Driver.Url.EndsWith(urlEnding).Should().BeTrue();
    }

    [Then(@"(.*) title presents on page")]
    public void ThenRegisterTitlePresentsOnPage(string title)
    {
      this.Site.PageTitle.Text.Should().BeEquivalentTo(title);
    }

    [Then(@"(.*) button presents")]
    public void ThenRegisterButtonPresents(string btn)
    {
      this.Site.SubmitButton.Text.Should().BeEquivalentTo(btn);
    }

    [Then(@"Register fields present on page")]
    public void ThenRegisterFieldsPresentOnPage(Table table)
    {
      var fields = table.Rows.Select(x => x.Values.First());
      var elements = this.Site.FormFields.Select(el => el.GetAttribute("name"));
      elements.Should().Contain(fields);
    }

    [Then(@"Following buttons present under User drop-drop down menu")]
    public void ThenFollowingButtonsPresentUnderUserDropDropDownMenu(Table table)
    {
      var buttons = table.Rows.Select(x => x.Values.First());
      //1
      foreach (var button in buttons)
      {
        var found = false;
        foreach (var webElement in this.Site.DropDownButtons)
        {
          found = webElement.Text == button;
          if (found)
          {
            break;
          }
        }
        found.Should().BeTrue();
      }
      //2
      //            buttons.All(b => Site.DropDownButtons.Any(x => x.Text == b)).Should().BeTrue();
    }

    [Then(@"Following buttons is no longer present under User drop-drop down menu")]
    public void ThenFollowingButtonsIsNoLongerPresentUnderUserDropDropDownMenu(Table table)
    {
      var buttons = table.Rows.Select(x => x.Values.First());
      //1
      foreach (var button in buttons)
      {
        var found = false;
        foreach (var webElement in this.Site.DropDownButtons)
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

    [When(@"Actor enters following data in to the register fields")]
    public void WhenActorEntersFollowingDataInToTheRegisterFields(Table table)
    {
      var row = table.Rows.First();
      foreach (var key in row.Keys)
      {
        this.Site.FormFields.GetField(key).SendKeys(row[key]);
      }
    }

    [Given(@"User with following data is registered")]
    public void GivenUserWithFollowingDataIsRegistered(Table table)
    {
      Driver.Navigate().GoToUrl(Settings.RegisterPageUrl);
      this.WhenActorEntersFollowingDataInToTheRegisterFields(table);
      this.Site.SubmitButton.Click();
      new SiteNavigation().WhenActorMovesCursorOverTheUserIcon();
      this.WhenUserSelectsRegisterFromDropDownMenu("Logout");
    }

    [Given(@"User with following data is registered in Habitat")]
    public void GivenUserWithFollowingDataIsRegisteredInHabitat(Table table)
    {
      Driver.Navigate().GoToUrl(Settings.RegisterPageUrl);
      this.WhenActorEntersFollowingDataInToTheRegisterFields(table);
      this.Site.SubmitButton.Click();

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

    [Given(@"User was logged out from the Habitat")]
    public void GivenUserWasLoggedOutFromTheHabitat()
    {
      new SiteNavigation().WhenActorMovesCursorOverTheUserIcon();
      this.Site.SubmitButton.Click();
    }

    [Given(@"User clicks (.*) from drop-down menu")]
    [When(@"User clicks (.*) from drop-down menu")]
    public void WhenUserClicksLoginFromDropDownMenu(string linkText)
    {
      Driver.FindElement(By.LinkText(linkText.ToUpperInvariant())).Click();
    }

    [AfterScenario]
    public void Cleanup()
    {
      ContextExtensions.CleanupPool.ForEach(this.CleanupExecute);
    }

    private void CleanupExecute(TestCleanupAction payload)
    {
      if (payload.ActionType == ActionType.RemoveUser)
      {
        ContextExtensions.HelperService.DeleteUser(payload.Payload);
      }

      throw new NotSupportedException($"Action type '{payload.ActionType}' is not supported");
    }
  }

  internal enum ActionType
  {
    RemoveUser
  }
}