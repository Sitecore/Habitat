namespace Sitecore.Foundation.Assets.Services
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using System.Web;
  using Sitecore.Foundation.Assets.Models;
  using Sitecore.Foundation.Assets.Repositories;
  using Sitecore.Text;

  /// <summary>
  ///   A service which helps add the required JavaScript at the end of a page, and CSS at the top of a page.
  ///   In component based architecture it ensures references and inline scripts are only added once.
  /// </summary>
  public class RenderAssetsService
  {
    private static RenderAssetsService _current;
    public static RenderAssetsService Current => _current ?? (_current = new RenderAssetsService());

    public HtmlString RenderScript(ScriptLocation location)
    {
      var sb = new StringBuilder();
      var assets = AssetRepository.Current.Items.Where(asset => (asset.Type == AssetType.JavaScript || asset.Type == AssetType.Raw) && asset.Location == location && this.IsForContextSite(asset));
      foreach (var item in assets)
      {
        if (!string.IsNullOrEmpty(item.Content))
        {
          sb.AppendFormat("<script src=\"{0}\"></script>", item.Content).AppendLine();
        }
        else if (!string.IsNullOrEmpty(item.Inline))
        {
          if (item.Type == AssetType.Raw)
          {
            sb.AppendLine(HttpUtility.HtmlDecode(item.Inline));
          }
          else
          {
            sb.AppendLine("<script>\njQuery(document).ready(function() {");
            sb.AppendLine(item.Inline);
            sb.AppendLine("});\n</script>");
          }
        }
      }
      return new HtmlString(sb.ToString());
    }

    public HtmlString RenderStyles()
    {
      var sb = new StringBuilder();
      foreach (var item in AssetRepository.Current.Items.Where(asset => asset.Type == AssetType.Css && this.IsForContextSite(asset)))
      {
        if (!string.IsNullOrEmpty(item.Content))
        {
          sb.AppendFormat("<link href=\"{0}\" rel=\"stylesheet\" />", item.Content).AppendLine();
        }
        else if (!string.IsNullOrEmpty(item.Inline))
        {
          sb.AppendLine("<style type=\"text/css\">");
          sb.AppendLine(item.Inline);
          sb.AppendLine("</style>");
        }
      }

      return new HtmlString(sb.ToString());
    }

    private bool IsForContextSite(Asset asset)
    {
      if (asset.Site == null)
        return true;

      foreach (var part in asset.Site.Split(new[] {'|'}, StringSplitOptions.RemoveEmptyEntries))
      {
        var siteWildcard = part.Trim().ToLowerInvariant();
        if (siteWildcard == "*" || Context.Site.Name.Equals(siteWildcard, StringComparison.InvariantCultureIgnoreCase))
          return true;
      }
      return false;
    }

  }
}