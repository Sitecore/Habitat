using System.Collections.Generic;
using Sitecore.Foundation.Common.Specflow.Steps;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Habitat.Website.Specflow.Infrastructure
{
  [Binding]
  public class WffmTransform
  {
    [StepArgumentTransformation]
    public IEnumerable<WffmModel> WffmModel(Table fields)
    {
      return fields.CreateSet<WffmModel>();
    } 
  }
}