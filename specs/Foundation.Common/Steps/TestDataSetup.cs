using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Foundation.Common.Specflow.Extensions;
using Sitecore.Foundation.Common.Specflow.Infrastructure;
using TechTalk.SpecFlow;

namespace Sitecore.Foundation.Common.Specflow.Steps
{
  [Binding]
  class TestDataSetup: StepsBase
  {
    [Given(@"User is registered in Habitat and logged out")]
    public void GivenUserIsRegisteredInHabitatAndLoggedOut(Table table)
    {
      //Go to Register page
      SiteBase.NavigateToPage(BaseSettings.RegisterPageUrl);
      //Enter data to the fields
      var row = table.Rows.First();
      foreach (var key in row.Keys)
      {
        CommonLocators.RegisterPageFields.GetField(key).SendKeys(row[key]);
      }
      //Following code will remove create user from DB after use case ends
      ContextExtensions.CleanupPool.Add(new TestCleanupAction
      {
        ActionType = ActionType.RemoveUser,
        Payload = "extranet\\" + row["Email"]
      });
      //Click Register button
      SiteBase.SubmitButton.Click(); 
      //Actor selects user dialog
      CommonActions.OpenUserDialog();
      //Actor clicks Logout button
      var btn = "Logout";
      var elements = CommonLocators.UserIconButtons.First(el => el.Text.Equals(btn, StringComparison.InvariantCultureIgnoreCase));
      elements.Click();
    }

    [Given(@"User was deleted from the System")]
    public void GivenUserWasDeletedFromTheSystem()
    {
      Cleanup();
    }
  }
}
