using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Sitecore.Feature.Accounts.Specflow.Infrastructure;
using Sitecore.Foundation.Common.Specflow.Infrastructure;
using TechTalk.SpecFlow;

namespace Sitecore.Feature.Accounts.Specflow.Steps
{
  class ExperienceProfileSteps : AccountStepsBase
  {
    [Then(@"Profile Activity Goals section for (.*) contains")]
    public void ThenProfileActivityGoalsSectionForKovSitecore_NetContains(string userName, Table table)
    {
      var contactID = GetContactId(userName);
      var queryUrl = BaseSettings.BaseUrl + $"/sitecore/api/ao/proxy/contacts/{contactID}/intel/goals";

      var entities = GetAnalytycsEntities<SearchEntity>(queryUrl);
      foreach (var row in table.Rows)
      {
        var goalName = row[0];
        entities.Data.Dataset.Goals.Select(x => x.GoalDisplayName)
          .Should().Contain(goalName);
      }
    }
  }
}
