using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Foundation.Common.Specflow.Extensions;
using Sitecore.Foundation.Common.Specflow.Extensions.Infrastructure;
using Sitecore.Foundation.Common.Specflow.Infrastructure;
using TechTalk.SpecFlow;

namespace Sitecore.Foundation.Common.Specflow.Steps
{
  [Binding]
  class TestDataSetup:TechTalk.SpecFlow.Steps
  {
    private readonly CleanupPool cleanupPool;

    public TestDataSetup(CleanupPool cleanupPool)
    {
      this.cleanupPool = cleanupPool;
    }
    private  CommonLocators commonLocators => new CommonLocators(FeatureContext);

    [Given(@"User is registered in Habitat and logged out")]
    public void GivenUserIsRegisteredInHabitatAndLoggedOut(Table table)
    {
      //Go to Register page
      this.commonLocators.NavigateToPage(BaseSettings.RegisterPageUrl);
      //Enter data to the fields
      var row = table.Rows.First();
      foreach (var key in row.Keys)
      {
        this.commonLocators.RegisterPageFields.GetField(key).SendKeys(row[key]);
      }
      //Following code will remove create user from DB after use case ends

      cleanupPool.Add(new TestCleanupAction
      {
        ActionType = ActionType.RemoveUser,
        Payload = "extranet\\" + row["Email"]
      });
      //Click Register button
      this.commonLocators.SubmitButton.Click();
      //Actor selects user dialog
      this.commonLocators.UserIcon.Click();
      //Actor clicks Logout button
      var btn = "Logout";
      var elements = this.commonLocators.UserIconButtons.First(el => el.Text.Equals(btn, StringComparison.InvariantCultureIgnoreCase));
      elements.Click();
    }

    [Given(@"User was deleted from the System")]
    public void GivenUserWasDeletedFromTheSystem()
    {
      //will be cleaned up on scenario end by destroing disposable CleanupPool
    }
  }
}
