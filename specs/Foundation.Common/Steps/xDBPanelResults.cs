using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using TechTalk.SpecFlow;

namespace Sitecore.Foundation.Common.Specflow.Steps
{
  [Binding]
  public class XDbPanelResults : StepsBase
  {
    [Then(@"Outcomes contains following values")]
    public void ThenContainsFollowingValues(Table table)
    {
      var expectedBehaviorData = table.Rows.SelectMany(x => x.Values);
      var behaviorData = CommonLocators.OnsideBehaviorData.Select(x=>x.Text);
      expectedBehaviorData.All(t => behaviorData.Any(el => el == t)).Should().BeTrue($"because behavior data should contain {string.Join("|", expectedBehaviorData)} but contains {string.Join("|", behaviorData)}");
    }
  }
}
