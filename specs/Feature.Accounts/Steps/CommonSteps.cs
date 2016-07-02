namespace Sitecore.Feature.Accounts.Specflow.Steps
{
  using System;
  using System.Linq;
  using FluentAssertions;
  using OpenQA.Selenium;
  using Sitecore.Foundation.Common.Specflow.Extensions;
  using Sitecore.Foundation.Common.Specflow.Extensions.Infrastructure;
  using Sitecore.Foundation.Common.Specflow.Infrastructure;
  using Sitecore.Foundation.Common.Specflow.Steps;
  using TechTalk.SpecFlow;

  [Binding]
  internal class CommonSteps : AccountStepsBase
  {
    private readonly CleanupPool cleanupPool;

    public CommonSteps(CleanupPool cleanupPool)
    {
      this.cleanupPool = cleanupPool;
    }

    [When(@"User selects (.*) from drop-down menu")]
    public void WhenUserSelectsRegisterFromDropDownMenu(string linkText)
    {
      SiteBase.Driver.FindElement(By.LinkText(linkText.ToUpperInvariant())).Click();
    }

    [Then(@"User info is shown on User popup")]
    public void ThenUserInfoIsShownOnUserPopup(Table table)
    {
      var values = table.Rows.Select(x => x.Values.First());
      values.All(v => this.Site.ShowUserInfoPopupFields.Any(x => x.Text == v)).Should().BeTrue();
    }

    [Then(@"(.*) title presents on page")]
    public void ThenRegisterTitlePresentsOnPage(string title)
    {
      this.Site.PageTitle.Text.Contains(title).Should().BeTrue();
    }

    [Then(@"(.*) title is no longer present on page")]
    public void ThenLoginTitleIsNoLongerPresentOnPage(string title)
    {
      this.Site.PageTitle.Text.Should().NotBe(title);
    }


    [Then(@"(.*) button presents")]
    public void ThenRegisterButtonPresents(string btn)
    {
      this.SiteBase.SubmitButton.GetAttribute("Value").Should().BeEquivalentTo(btn);
    }

    [Then(@"Register fields present on page")]
    public void ThenRegisterFieldsPresentOnPage(Table table)
    {
      var fields = table.Rows.Select(x => x.Values.First());
      var elements = SiteBase.RegisterPageFields.Select(el => el.GetAttribute("name"));
      elements.Should().Contain(fields);
    }

    [Then(@"Following buttons present under User icon")]
    public void ThenFollowingButtonsPresentUnderUserDropDropDownMenu(Table table)
    {
      var buttons = table.Rows.Select(x => x.Values.First());
      buttons.All(b => SiteBase.UserIconButtons.Any(x => x.Text == b)).Should().BeTrue();
    }

    [Then(@"Following buttons is no longer present under User icon")]
    public void ThenFollowingButtonsIsNoLongerPresentUnderUserDropDropDownMenu(Table table)
    {
      var buttons = table.Rows.Select(x => x.Values.First());
      buttons.All(b => SiteBase.UserIconButtons.Any(x => x.Text == b)).Should().BeFalse();
    }

    [Then(@"User info is not shown on User popup")]
    public void ThenUserInfoIsNotShownOnUserPopup(Table table)
    {
      var fields = table.Rows.Select(x => x.Values.First());
      fields.All(f => this.Site.EditUserProfileTextFields.Any(x => x.Text == f)).Should().BeFalse();
    }


    [When(@"Actor enters following data in to the register fields")]
    public void WhenActorEntersFollowingDataInToTheRegisterFields(Table table)
    {
      var row = table.Rows.First();
      foreach (var key in row.Keys)
      {
        SiteBase.RegisterPageFields.GetField(key).SendKeys(row[key]);
      }
      //Following code will remove create user from DB after use case ends
      cleanupPool.Add(new TestCleanupAction
      {
        ActionType = ActionType.RemoveUser,
        Payload = "extranet\\" + row["Email"]
      });
    }

    [When(@"Actor enters following data in to the register fields with misssed email")]
    public void WhenActorEntersFollowingDataInToTheRegisterFieldsWithMisssedEmail(Table table)
    {
      var row = table.Rows.First();
      foreach (var key in row.Keys)
      {
        SiteBase.RegisterPageFields.GetField(key).SendKeys(row[key]);
      }
    }

    [Given(@"User with following data is registered")]
    public void GivenUserWithFollowingDataIsRegistered(Table table)
    {
      SiteBase.Driver.Navigate().GoToUrl(BaseSettings.RegisterPageUrl);
      this.WhenActorEntersFollowingDataInToTheRegisterFields(table);
      this.SiteBase.SubmitButton.Click();
      //TODO: change with click user item
      //new SiteNavigationSteps().WhenActorMovesCursorOverTheUserIcon();
      this.WhenUserSelectsRegisterFromDropDownMenu("Logout");
    }

    [Given(@"User with following data is registered in Habitat")]
    public void GivenUserWithFollowingDataIsRegisteredInHabitat(Table table)
    {
      SiteBase.Driver.Navigate().GoToUrl(BaseSettings.RegisterPageUrl);
      this.WhenActorEntersFollowingDataInToTheRegisterFields(table);
      this.SiteBase.SubmitButton.Click();

      table.Rows
        .Select(row => row["Email"]).ToList()
        .ForEach(email =>
        {
          this.cleanupPool.Add(new TestCleanupAction
          {
            ActionType = ActionType.RemoveUser,
            Payload = email
          });
        });
    }

    [Given(@"Session was expired")]
    public void GivenSessionWasExpired()
    {
      SiteBase.Driver.FindElement(By.CssSelector("body")).SendKeys(Keys.Control + 't');
      SiteBase.Driver.Navigate().GoToUrl(BaseSettings.EndSessionUrl);
      SiteBase.Driver.FindElement(By.CssSelector("body")).SendKeys(Keys.Control + 'w');
    }


    [When(@"User clicks Log out on User Icon")]
    [Given(@"User was logged out from the Habitat")]
    public void GivenUserWasLoggedOutFromTheHabitat()
    {
      //TODO: change with click user item
      //new SiteNavigationSteps().WhenActorMovesCursorOverTheUserIcon();
      this.SiteBase.SubmitButton.Click();
    }


    [Then(@"Login drop-down popup is no longer presents")]
    public void ThenLoginPopupIsNoLongerPresents()
    {
      var element = this.Site.UserFormDropDownPopup;

      element.Displayed.Should().BeFalse();
    }

    [Then(@"Following links present under User popup")]
    public void ThenFollowingLinksPresentUnderUserDropDropDownMenu(Table table)
    {
      var buttonsLinks = table.Rows.Select(x => x.Values.First());
      buttonsLinks.All(l => SiteBase.UserIconDropDownButtonLinks.Any(x => x.Text == l)).Should().BeTrue();
    }

    [Then(@"Habitat Main page presents")]
    public void ThenHabitatMainPagePresents()
    {
      var absoluteUri = new Uri(SiteBase.Driver.Url).AbsolutePath;
      (absoluteUri.Equals("/") || absoluteUri.Equals("/en")).Should().BeTrue();
    }

    [Then(@"Following links is no longer present under User popup")]
    public void ThenFollowingLinksIsNoLongerPresentUnderUserPopup(Table table)
    {
      var buttonsLinks = table.Rows.Select(x => x.Values.First());
      buttonsLinks.All(l => SiteBase.UserIconDropDownButtonLinks.Any(x => x.Text == l)).Should().BeFalse();
    }
  }
}