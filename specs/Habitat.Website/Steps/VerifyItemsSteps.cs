using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Habitat.Website.Specflow.Infrastructure;
using Sitecore.Foundation.Common.Specflow.Infrastructure;
using Sitecore.Foundation.Common.Specflow.Service_References.UtfService;
using TechTalk.SpecFlow;

namespace Habitat.Website.Specflow.Steps
{
  [Binding]
  public class VerifyItemsSteps: WebsiteStepsBase
  {
    [Then(@"Items contain MVC controls")]
    public void ThenItemsContainMvcControls(IEnumerable<WffmModel> fields )
    {
      foreach (var field in fields)
      {
        var itemPath = field.ItemPath;
        var formPath = $"{ItemService.WebFormsRootPath}/{field.AllowedControls}";
        var itemId = ContextExtensions.UtfService.ItemExistsByPath(formPath, Database.Master);
        var fieldValue = ContextExtensions.UtfService.GetItemFieldValue(itemPath, "Allowed Controls", Database.Master);
        fieldValue.Contains(itemId).Should().BeTrue();
      }
    }
  }
}
