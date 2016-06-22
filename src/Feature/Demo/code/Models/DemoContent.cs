namespace Sitecore.Feature.Demo.Models
{
  using System.Linq;
  using Sitecore.Data.Items;
  using Sitecore.Foundation.SitecoreExtensions.Extensions;
  using Sitecore.Text;
  using static Configuration.Factory;

  public class DemoContent
  {
    public Item Item { get; set; }

    public DemoContent(Item item)
    {
      this.Item = item;
    }

    public string HtmlContent
    {
      get
      {
        var content = this.Item[Templates.DemoContent.Fields.HTMLContent.ToString()];
        return this.ReplaceTokens(content);
      }
    }

    private string ReplaceTokens(string content)
    {
      var replacer = GetMasterVariablesReplacer();
      using (new ReplacerContextSwitcher(this.GetReplacementTokens()))
        return replacer.Replace(content, this.Item);
    }

    private string[] GetReplacementTokens()
    {
      return this.Item.Children.Where(i => i.IsDerived(Templates.Token.ID)).SelectMany(i => new[] {$"${i.Name}", i[Templates.Token.Fields.TokenValue]}).ToArray();
    }
  }
}