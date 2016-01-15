namespace Sitecore.Foundation.Testing.Specflow.Steps
{
  using System.Collections.Generic;
  using Sitecore.Foundation.Testing.Specflow.Infrastructure;
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
