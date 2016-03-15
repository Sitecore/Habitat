namespace Sitecore.Feature.Demo.Models
{
  using System.Linq;
  using Sitecore.Configuration;
  using Sitecore.Data.Items;
  using Sitecore.Foundation.SitecoreExtensions.Extensions;
  using Sitecore.Text;
  using static Sitecore.Configuration.Factory;

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
      var replacer = GetMasterVariablesReplacer();
      using (new ReplacerContextSwitcher(GetReplacementTokens()))
        return replacer.Replace(content, Item);
    }

    private string[] GetReplacementTokens()
    {
      return Item.Children.Where(i => i.IsDerived(Templates.Token.ID)).SelectMany(i => new [] {$"${i.Name}", i[Templates.Token.Fields.TokenValue]}).ToArray();
    }
  }
}