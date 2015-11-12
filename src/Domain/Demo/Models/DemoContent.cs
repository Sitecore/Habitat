using System;
using System.Collections.Generic;
using System.Linq;
using Habitat.Framework.SitecoreExtensions.Extensions;
using Sitecore.Configuration;
using Sitecore.Data.Items;
using Sitecore.Text;

namespace Habitat.Demo.Models
{
  public class DemoContent
  {
    public Item Item { get; set; }

    public DemoContent(Item item)
    {
      Item = item;
    }

    public string HtmlContent
    {
      get
      {
        var content = Item[Templates.DemoContent.Fields.HTMLContent.ToString()];
        return ReplaceTokens(content);
      }
    }

    private string ReplaceTokens(string content)
    {
      var replacer = Factory.GetMasterVariablesReplacer();
      using (new ReplacerContextSwitcher(GetReplacementTokens()))
        return replacer.Replace(content, Item);
    }

    private string[] GetReplacementTokens()
    {
      return Item.Children.Where(i => i.IsDerived(Templates.Token.ID)).SelectMany(i => new [] {$"${i.Name}", i[Templates.Token.Fields.TokenValue]}).ToArray();
    }
  }
}