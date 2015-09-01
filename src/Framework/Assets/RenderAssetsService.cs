namespace Habitat.Framework.Assets
{
    using System.Linq;
    using System.Text;
    using System.Web;
    using Habitat.Framework.Assets.Models;

    /// <summary>
    /// A service which helps add the required JavaScript at the end of a page, and CSS at the top of a page.
    /// In component based architecture it ensures references and inline scripts are only added once.
    /// </summary>
    public class RenderAssetsService
    {
        private static RenderAssetsService _current;

        public static RenderAssetsService Current => _current ?? (_current = new RenderAssetsService());

        /// <summary>
        /// Renders the JavaScript requirements to the page
        /// </summary>
        /// <returns>The rendered JavaScript code</returns>
        public HtmlString RenderScript(ScriptLocation location = ScriptLocation.Head)
        {
            var sb = new StringBuilder();
            foreach (var item in AssetRepository.Current.Items.Where(x => x.Type == AssetType.JavaScript && x.File != null && x.Location == location))
            {
                sb.AppendFormat("<script src=\"{0}\"></script>", item.File).AppendLine();
            }

            if (AssetRepository.Current.Items.Any(x => x.Inline != null))
            {
                sb.AppendLine("<script>\njQuery(document).ready(function() {");

                foreach (var item in AssetRepository.Current.Items.Where(x => x.Type == AssetType.JavaScript && x.Inline != null && x.Location == location))
                {
                    sb.AppendLine(item.Inline);
                }

                sb.AppendLine("});\n</script>");
            }

            return new HtmlString(sb.ToString());
        }

        /// <summary>
        /// Renders the stylesheet requirements to the page
        /// </summary>
        /// <returns>The rendered style code</returns>
        public HtmlString RenderStyles()
        {
            var sb = new StringBuilder();
            foreach (var item in AssetRepository.Current.Items.Where(x => x.Type == AssetType.Css && x.File != null))
            {
                sb.AppendFormat("<link href=\"{0}\" rel=\"stylesheet\" />", item.File).AppendLine();
            }

            if (AssetRepository.Current.Items.Any(x => x.Inline != null))
            {
                sb.AppendLine("<style type=\"text/css\">");

                foreach (var item in AssetRepository.Current.Items.Where(x => x.Type == AssetType.Css && x.Inline != null))
                {
                    sb.AppendLine(item.Inline);
                }

                sb.AppendLine("</style>");
            }

            return new HtmlString(sb.ToString());
        }
    }
}