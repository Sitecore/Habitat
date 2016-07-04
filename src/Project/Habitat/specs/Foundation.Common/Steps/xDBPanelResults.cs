using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using TechTalk.SpecFlow;

namespace Sitecore.Foundation.Common.Specflow.Steps
{
  using Sitecore.Foundation.Common.Specflow.Infrastructure;

  [Binding]
  public class XDbPanelResults
  {
    private readonly FeatureContext featureContext;
    private readonly CommonLocators commonLocators;

    public XDbPanelResults(FeatureContext featureContext)
    {
      this.featureContext = featureContext;
      this.commonLocators = new CommonLocators(featureContext);
    }
    [Then(@"Outcomes contains following values")]
    public void ThenContainsFollowingValues(Table table)
    {
      var expectedBehaviorData = table.Rows.SelectMany(x => x.Values);
      var behaviorData = this.commonLocators.OnsideBehaviorData.Select(x=>x.Text);
      expectedBehaviorData.All(t => behaviorData.Any(el => el == t)).Should().BeTrue($"because behavior data should contain {string.Join("|", expectedBehaviorData)} but contains {string.Join("|", behaviorData)}");
    }
  }
}
