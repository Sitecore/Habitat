namespace Sitecore.Foundation.Common.Specflow.Steps
{
  using System.Collections.Generic;
  using Sitecore.Foundation.Common.Specflow.Infrastructure;
  using TechTalk.SpecFlow;
  using TechTalk.SpecFlow.Assist;

  [Binding]
  public class Transforms
  {
    [StepArgumentTransformation]
    public IEnumerable<ItemFieldDefinition> ItemsTransform(Table itemFields)
    {
      return itemFields.CreateSet<ItemFieldDefinition>();
    }

    [StepArgumentTransformation]
    public IEnumerable<ControlProperties> ControlPropertiesTransform(Table itemFields)
    {
      return itemFields.CreateSet<ControlProperties>();
    }

    [StepArgumentTransformation]
    public IEnumerable<LanguageModel> LangugageModelTransform(Table itemFields)
    {
      return itemFields.CreateSet<LanguageModel>();
    }
  }
}
