namespace Sitecore.Foundation.Common.Specflow.Steps
{
  using System.Collections.Generic;
  using Sitecore.Foundation.Common.Specflow.Infrastructure;
  using TechTalk.SpecFlow;
  using TechTalk.SpecFlow.Assist;

  public class Transforms
  {
    [StepArgumentTransformation]
    public IEnumerable<ItemFieldDefinition> ItemsTransform(Table itemFields)
    {
      return itemFields.CreateSet<ItemFieldDefinition>();
    }
  }
}
