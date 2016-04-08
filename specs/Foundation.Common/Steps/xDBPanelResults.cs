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
  public class xDBPanelResults : StepsBase
  {
    [Then(@"Outcomes contains following values")]
    public void ThenContainsFollowingValues(Table table)
    {
      var text = table.Rows.Select(x => x.Values.First());
      text.All(t => CommonLocators.OnsideBehaviorData.Any(el => el.Text == t)).Should().BeTrue();
    }
  }
}
