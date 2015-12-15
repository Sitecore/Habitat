namespace Sitecore.Foundation.Assets
{
  using System.Linq;
  using System.Text;
  using System.Web;
  using Sitecore.Foundation.Assets.Models;

  /// <summary>
  ///   A service which helps add the required JavaScript at the end of a page, and CSS at the top of a page.
  ///   In component based architecture it ensures references and inline scripts are only added once.
  /// </summary>
  public class RenderAssetsService
  {
    private static RenderAssetsService _current;
    public static RenderAssetsService Current => _current ?? (_current = new RenderAssetsService());

    /// <summary>
    ///   Renders the JavaScript requirements to the page
    /// </summary>
    /// <returns>The rendered JavaScript code</returns>
    public HtmlString RenderScript(ScriptLocation location)
    {
      var sb = new StringBuilder();
      var assets = AssetRepository.Current.Items.Where(x => (x.Type == AssetType.JavaScript || x.Type == AssetType.Raw) && x.Location == location);
      foreach (var item in assets)
      {
        if (!string.IsNullOrEmpty(item.File))
        {
          sb.AppendFormat("<script src=\"{0}\"></script>", item.File).AppendLine();
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

    /// <summary>
    ///   Renders the stylesheet requirements to the page
    /// </summary>
    /// <returns>The rendered style code</returns>
    public HtmlString RenderStyles()
    {
      var sb = new StringBuilder();
      foreach (var item in AssetRepository.Current.Items.Where(x => x.Type == AssetType.Css))
      {
        if (!string.IsNullOrEmpty(item.File))
        {
          sb.AppendFormat("<link href=\"{0}\" rel=\"stylesheet\" />", item.File).AppendLine();
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
  }
}