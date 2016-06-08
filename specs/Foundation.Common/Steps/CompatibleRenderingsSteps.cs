using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Sitecore.Foundation.Common.Specflow.Infrastructure;
using Sitecore.Foundation.Common.Specflow.Service_References.UtfService;
using TechTalk.SpecFlow;

namespace Sitecore.Foundation.Common.Specflow.Steps
{
  [Binding]
  public class CompatibleRenderingsSteps: StepsBase
  {
    [Then(@"(.*) module contains incompatible renderings")]
    public void ThenModuleContainsIncompatibleRenderings(string module, Table table)
    {
      var items = table.Rows.Select(x => x.Values.First());

      foreach (var item in items)
      {
        var renderingPath = ItemService.GetModuleRenderingPath(module, item);
        var itemFieldValue = ContextExtensions.UtfService.GetItemFieldValue(renderingPath, "Compatible Renderings", Database.Master);
        itemFieldValue.Should().BeEmpty();
      }

      
    }

  }
}
