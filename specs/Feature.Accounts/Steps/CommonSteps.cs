namespace Sitecore.Feature.Accounts.Specflow.Steps
{
  using System;
  using System.Linq;
  using FluentAssertions;
  using OpenQA.Selenium;
  using Sitecore.Foundation.Common.Specflow.Extensions;
  using Sitecore.Foundation.Common.Specflow.Infrastructure;
  using TechTalk.SpecFlow;

  [Binding]
  internal class CommonSteps : AccountStepsBase
  {
    [When(@"User selects (.*) from drop-down menu")]
    public void WhenUserSelectsRegisterFromDropDownMenu(string linkText)
    {
      Driver.FindElement(By.LinkText(linkText.ToUpperInvariant())).Click();
    }

    [Then(@"User info is shown on User popup")]
    public void ThenUserInfoIsShownOnUserPopup(Table table)
    {
      var values = table.Rows.Select(x => x.Values.First());
      //1
      foreach (var value in values)
      {
        var found = false;
        foreach (var webElement in Site.ShowUserInfoPopupFields)
        {
          found = webElement.Text == value;
          if (found)
          {
            break;
          }
        }
        found.Should().BeFalse();
      }
    }





    [Then(@"Habitat website is opened on Main Page (.*)")]
    [Then(@"Page URL ends on (.*)")]
    public void ThenPageUrlEndsOnExpected(string urlEnding)
    {
      Driver.Url.EndsWith(urlEnding).Should().BeTrue();
    }

    [Then(@"Page URL not ends on (.*)")]
    public void ThenPageURLNotEndsOn(string urlEnding)
    {
      Driver.Url.EndsWith(urlEnding).Should().BeFalse();
    }



    [Then(@"(.*) title presents on page")]
    public void ThenRegisterTitlePresentsOnPage(string title)
    {
      Site.PageTitle.Text.Should().BeEquivalentTo(title);
    }

    [Then(@"(.*) title is no longer present on page")]
    public void ThenLoginTitleIsNoLongerPresentOnPage(string title)
    {
      Site.PageTitle.Text.Should().NotBe(title);
    }


    [Then(@"(.*) button presents")]
    public void ThenRegisterButtonPresents(string btn)
    {
      Site.SubmitButton.GetAttribute("Value").Should().BeEquivalentTo(btn);
    }

    [Then(@"Register fields present on page")]
    public void ThenRegisterFieldsPresentOnPage(Table table)
    {
      var fields = table.Rows.Select(x => x.Values.First());
      var elements = Site.FormFields.Select(el => el.GetAttribute("name"));
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
        foreach (var webElement in Site.UserIconDropDownButtons)
        {
          found = webElement.Text == button;
          if (found)
          {
            break;
          }
        }
        found.Should().BeFalse();
      }
      //2
      //            buttons.All(b => SiteDemo.DropDownButtons.Any(x => x.Text == b)).Should().BeTrue();
    }

    [Then(@"Following buttons is no longer present under User drop-drop down menu")]
    public void ThenFollowingButtonsIsNoLongerPresentUnderUserDropDropDownMenu(Table table)
    {
      var buttons = table.Rows.Select(x => x.Values.First());
      //1
      foreach (var button in buttons)
      {
        var found = false;
        foreach (var webElement in Site.UserIconDropDownButtons)
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

    [Then(@"User info is not shown on User popup")]
    public void ThenUserInfoIsNotShownOnUserPopup(Table table)
    {
      var fields = table.Rows.Select(x => x.Values.First());
      //1
      foreach (var field in fields)
      {
        var found = false;
        foreach (var webElement in Site.EditUserProfileTextFields)
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




    [When(@"Actor enters following data in to the register fields")]
    public void WhenActorEntersFollowingDataInToTheRegisterFields(Table table)
    {
      var row = table.Rows.First();
      foreach (var key in row.Keys)
      {
        Site.FormFields.GetField(key).SendKeys(row[key]);
      }
      //Following code will remove create user from DB after use case ends
      ContextExtensions.CleanupPool.Add(new TestCleanupAction
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
        Site.FormFields.GetField(key).SendKeys(row[key]);
      }
    }




    [Given(@"User with following data is registered")]
    public void GivenUserWithFollowingDataIsRegistered(Table table)
    {
      Driver.Navigate().GoToUrl(BaseSettings.RegisterPageUrl);
      WhenActorEntersFollowingDataInToTheRegisterFields(table);
      Site.SubmitButton.Click();
      new SiteNavigation().WhenActorMovesCursorOverTheUserIcon();
      WhenUserSelectsRegisterFromDropDownMenu("Logout");
    }

    [Given(@"User with following data is registered in Habitat")]
    public void GivenUserWithFollowingDataIsRegisteredInHabitat(Table table)
    {
      Driver.Navigate().GoToUrl(BaseSettings.RegisterPageUrl);
      WhenActorEntersFollowingDataInToTheRegisterFields(table);
      Site.SubmitButton.Click();

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

    [Given(@"Session was expired")]
    public void GivenSessionWasExpired()
    {
      Driver.FindElement(By.CssSelector("body")).SendKeys(Keys.Control + 't');
      Driver.Navigate().GoToUrl(BaseSettings.EndSessionUrl);
      Driver.FindElement(By.CssSelector("body")).SendKeys(Keys.Control + 'w');
    }


    [When(@"User clicks Log out on User Icon")]
    [Given(@"User was logged out from the Habitat")]
    public void GivenUserWasLoggedOutFromTheHabitat()
    {
      new SiteNavigation().WhenActorMovesCursorOverTheUserIcon();
      Site.SubmitButton.Click();
    }

    [Given(@"User clicks (.*) from drop-down menu")]
    [When(@"User clicks (.*) from drop-down menu")]
    public void WhenUserClicksLoginFromDropDownMenu(string linkText)
    {
      Driver.FindElement(By.LinkText(linkText.ToUpperInvariant())).Click();
    }

   

    [Given(@"User is registered in Habitat and logged out")]
    public void GivenUserIsRegisteredInHabitatAndLoggedOut(Table table)
    {
      Driver.Navigate().GoToUrl(BaseSettings.RegisterPageUrl);
      WhenActorEntersFollowingDataInToTheRegisterFields(table);
      Site.SubmitButton.Click();
      new SiteNavigation().WhenActorMovesCursorOverTheUserIcon();
      Site.SubmitButton.Click();
    }

    [Given(@"User was deleted from the System")]
    public void GivenUserWasDeletedFromTheSystem()
    {
      Cleanup();
    }

    [Given(@"Login form is opened")]
    public void GivenLoginFormIsOpened()
    {
      SiteNavigation.GivenHabitatWebsiteIsOpenedOnMainPage();
      SiteNavigation.WhenActorMovesCursorOverTheUserIcon();
      WhenUserClicksLoginFromDropDownMenu("Login");
    }


    [Then(@"Login popup is no longer presents")]
    public void ThenLoginPopupIsNoLongerPresents()
    {
      var element = Site.LoginFormPopup;

      element.Displayed.Should().BeFalse();
    }

    [Then(@"Following links present under User drop-drop down menu")]
    public void ThenFollowingLinksPresentUnderUserDropDropDownMenu(Table table)
    {
      var buttonsLinks = table.Rows.Select(x => x.Values.First());
      //1
      foreach (var buttonLink in buttonsLinks)
      {
        var found = false;
        foreach (var webElement in Site.UserIconDropDownButtonLinks)
        {
          found = webElement.Text == buttonLink;
          if (found)
          {
            break;
          }
        }
        found.Should().BeFalse();
      }
    }

    [Then(@"Habitat Main page presents")]
    public void ThenHabitatMainPagePresents()
    {
      var absoluteUri = new Uri(Driver.Url).AbsolutePath;
      (absoluteUri.Equals("/") || absoluteUri.Equals("/en")).Should().BeTrue();
    }




  }
}