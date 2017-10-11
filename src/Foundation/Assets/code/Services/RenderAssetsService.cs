namespace Sitecore.Foundation.Assets.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Web;
    using Sitecore.Foundation.Assets.Models;
    using Sitecore.Foundation.Assets.Repositories;

    /// <summary>
    ///     A service which helps add the required JavaScript at the end of a page, and CSS at the top of a page.
    ///     In component based architecture it ensures references and inline scripts are only added once.
    /// </summary>
    public class RenderAssetsService
    {
        private static RenderAssetsService _current;
        public static RenderAssetsService Current => _current ?? (_current = new RenderAssetsService());

        public virtual HtmlString RenderScript(ScriptLocation location)
        {
            var assets = AssetRepository.Current.Items.Where(asset => (asset.Type == AssetType.JavaScript || asset.Type == AssetType.Raw) && asset.Location == location && this.IsForContextSite(asset));

            var sb = new StringBuilder();
            foreach (var item in assets)
            {
                if (item.Type == AssetType.Raw)
                {
                    sb.Append(item.Content).AppendLine();
                }
                else
                {
                    switch (item.ContentType)
                    {
                        case AssetContentType.File:
                            sb.AppendFormat("<script src=\"{0}\"></script>", item.Content).AppendLine();
                            break;
                        case AssetContentType.Inline:
                            if (item.Type == AssetType.Raw)
                            {
                                sb.AppendLine(HttpUtility.HtmlDecode(item.Content));
                            }
                            else
                            {
                                sb.AppendLine("<script>\njQuery(document).ready(function() {");
                                sb.AppendLine(item.Content);
                                sb.AppendLine("});\n</script>");
                            }
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
            }
            return new HtmlString(sb.ToString());
        }

        public virtual HtmlString RenderStyles()
        {
            var sb = new StringBuilder();
            foreach (var item in AssetRepository.Current.Items.Where(asset => asset.Type == AssetType.Css && this.IsForContextSite(asset)))
            {
                switch (item.ContentType)
                {
                    case AssetContentType.File:
                        sb.AppendFormat("<link href=\"{0}\" rel=\"stylesheet\" />", item.Content).AppendLine();
                        break;
                    case AssetContentType.Inline:
                        sb.AppendLine("<style type=\"text/css\">");
                        sb.AppendLine(item.Content);
                        sb.AppendLine("</style>");
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            return new HtmlString(sb.ToString());
        }

        protected bool IsForContextSite(Asset asset)
        {
            if (asset.Site == null)
            {
                return true;
            }

            foreach (var part in asset.Site.Split(new[] {'|'}, StringSplitOptions.RemoveEmptyEntries))
            {
                var siteWildcard = part.Trim().ToLowerInvariant();
                if (siteWildcard == "*" || Context.Site.Name.Equals(siteWildcard, StringComparison.InvariantCultureIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }

        public virtual List<string> GetFilePaths(ScriptLocation location, AssetType typeOfAsset)
        {
            var assets = AssetRepository.Current.Items.Where(
                asset => (asset.Type == typeOfAsset || asset.Type == AssetType.Raw)
                         && asset.Location == location && asset.ContentType == AssetContentType.File && this.IsForContextSite(asset));
            List<string> paths = new List<string>();

            foreach (var item in assets)
            {
                if (!string.IsNullOrEmpty(item.Content))
                {
                    paths.Add(item.Content);
                }
            }
            return paths;
        }
    }
}