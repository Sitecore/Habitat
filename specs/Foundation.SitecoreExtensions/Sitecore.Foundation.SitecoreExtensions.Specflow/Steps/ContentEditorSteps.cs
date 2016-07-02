using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Sitecore.Foundation.Common.Specflow.Infrastructure;
using Sitecore.Foundation.Common.Specflow.UtfService;
using TechTalk.SpecFlow;

namespace Sitecore.Foundation.SitecoreExtensions.Specflow.Steps
{
  [Binding]
  class ContentEditorSteps : TechTalk.SpecFlow.Steps
  {
    [Then(@"Following items are present under (.*) item in (.*) db")]
    public void ThenFollowingCustomHtmlProfilesArePresentUnderItemInCoreDb(string parentPath, string db, Table table)
    {
      var children = table.Rows.SelectMany(x => x.Values);
      Database database;
      Enum.TryParse(db, true, out database);
      var actualChildren = ContextExtensions.UtfService.GetChildren(parentPath, database, true)
        //get last part of path separated by '\'
        .Select(x => x.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries).Last());

      children.All(child => actualChildren.Contains(child)).Should().BeTrue();
      ;

    }

    [Then(@"Page Content has correct Rich Text Editor sources")]
    public void ThenPageContentHasCorrectRichTextEditorSources(IEnumerable<ItemFieldDefinition> fields)
    {
      foreach (var item in fields)
      {
        ContextExtensions.UtfService.GetItemFieldValue(item.ItemPath, item.FieldName)
          .Should()
          .Be(item.FieldValue);


      }
    }


  }
}
