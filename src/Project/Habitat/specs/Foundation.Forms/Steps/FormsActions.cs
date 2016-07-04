using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Foundation.Common.Specflow.Infrastructure;
using TechTalk.SpecFlow;

namespace Sitecore.Foundation.Forms.Specflow.Steps
{
  [Binding]
  public class FormsActions:FormStepsBase 
  {
    [When(@"Actor enteres following data into Leave an Email form fields")]
    public void WhenActorEnteresFollowingDataIntoLeaveAnEmailFormFields(Table table)
    {
      var row = table.Rows.First();
      foreach (var key in row.Keys)
      {
        var text = row[key];
        FormLocators.LeaveEmailFormField.SendKeys(text);
      }
    }

    [When(@"Actor clicks Submit button on Leave an Email form")]
    public void WhenActorClicksSubmitButtonOnLeaveAnEmailForm()
    {
      FormLocators.SubmitButton.Click();
    }


  }
}
