namespace Habitat.Website.Specflow.Steps
{
  using System.Collections.Generic;
  using FluentAssertions;
  using Habitat.Website.Specflow.Infrastructure;
  using Sitecore.Foundation.Common.Specflow.Infrastructure;
  using TechTalk.SpecFlow;

  [Binding]
  public class VerifyItemsSteps
  {
    [Then(@"Items contain MVC controls")]
    public void ThenItemsContainMvcControls(IEnumerable<WffmModel> fields)
    {
      foreach (var field in fields)
      {
        var itemPath = field.ItemPath;
        var formPath = $"{ItemService.WebFormsRootPath}/{field.AllowedControls}";
        var itemId = ContextExtensions.UtfService.ItemExistsByPath(formPath);
        var fieldValue = ContextExtensions.UtfService.GetItemFieldValue(itemPath, "Allowed Controls");
        fieldValue.Contains(itemId).Should().BeTrue();
      }
    }
  }
}