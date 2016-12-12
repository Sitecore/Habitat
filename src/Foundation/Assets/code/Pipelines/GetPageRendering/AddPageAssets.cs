namespace Sitecore.Foundation.Assets.Pipelines.GetPageRendering
{
    using System.Linq;
    using Sitecore.Data;
    using Sitecore.Data.Items;
    using Sitecore.Foundation.Assets.Models;
    using Sitecore.Foundation.Assets.Repositories;
    using Sitecore.Foundation.SitecoreExtensions.Extensions;
    using Sitecore.Mvc.Pipelines.Response.GetPageRendering;
    using Sitecore.Mvc.Presentation;

    public class AddPageAssets : GetPageRenderingProcessor
    {
        public override void Process(GetPageRenderingArgs args)
        {
            this.AddAssets(PageContext.Current.Item);
        }

        protected void AddAssets(Item item)
        {
            var styling = this.GetPageAssetValue(item, Templates.PageAssets.Fields.CssCode);
            if (!string.IsNullOrWhiteSpace(styling))
            {
                AssetRepository.Current.AddStyling(styling, styling.GetHashCode().ToString(), true);
            }
            var scriptBottom = this.GetPageAssetValue(item, Templates.PageAssets.Fields.JavascriptCodeBottom);
            if (!string.IsNullOrWhiteSpace(scriptBottom))
            {
                AssetRepository.Current.AddScript(scriptBottom, scriptBottom.GetHashCode().ToString(), ScriptLocation.Body, true);
            }
            var scriptHead = this.GetPageAssetValue(item, Templates.PageAssets.Fields.JavascriptCodeTop);
            if (!string.IsNullOrWhiteSpace(scriptHead))
            {
                AssetRepository.Current.AddScript(scriptHead, scriptHead.GetHashCode().ToString(), ScriptLocation.Head, true);
            }
        }

        private string GetPageAssetValue(Item item, ID assetField)
        {
            if (item.IsDerived(Templates.PageAssets.ID))
            {
                var assetValue = item[assetField];
                if (!string.IsNullOrWhiteSpace(assetValue))
                {
                    return assetValue;
                }
            }

            return GetInheritedPageAssetValue(item, assetField);
        }

        private static string GetInheritedPageAssetValue(Item item, ID assetField)
        {
            var inheritedAssetItem = item.Axes.GetAncestors().FirstOrDefault(i => i.IsDerived(Templates.PageAssets.ID) && MainUtil.GetBool(item[Templates.PageAssets.Fields.InheritAssets], false) && string.IsNullOrWhiteSpace(item[assetField]));
            return inheritedAssetItem?[assetField];
        }
    }
}