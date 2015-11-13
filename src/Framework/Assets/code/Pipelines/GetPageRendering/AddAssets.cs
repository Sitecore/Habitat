namespace Habitat.Framework.Assets.Pipelines.GetPageRendering
{
  using System.Collections.Generic;
  using System.Linq;
  using System.Xml;
  using Habitat.Framework.Assets.Models;
  using Habitat.Framework.SitecoreExtensions.Extensions;
  using Sitecore;
  using Sitecore.Data;
  using Sitecore.Data.Items;
  using Sitecore.Diagnostics;
  using Sitecore.Mvc.Pipelines.Response.GetPageRendering;
  using Sitecore.Mvc.Presentation;

  /// <summary>
  ///   Mvc.BuildPageDefinition pipeline processor to dynamically reference Cassette Bundles
  /// </summary>
  public class AddAssets : GetPageRenderingProcessor
  {
    private IList<Asset> _defaultAssets;

    private IList<Asset> DefaultAssets => this._defaultAssets ?? (this._defaultAssets = new List<Asset>());

    public void AddAsset(XmlNode node)
    {
      var asset = AssetRepository.Current.CreateFromConfiguration(node);
      if (asset != null)
      {
        this.DefaultAssets.Add(asset);
      }
    }

    public override void Process(GetPageRenderingArgs args)
    {
      this.AddDefaultAssetsFromConfiguration();

      this.AddPageAssets(PageContext.Current.Item);

      this.AddRenderingAssets(args.PageContext.PageDefinition.Renderings);
    }

    private void AddRenderingAssets(IEnumerable<Rendering> renderings)
    {
      foreach (var rendering in renderings)
      {
        var renderingItem = this.GetRenderingItem(rendering);
        if (renderingItem == null)
        {
          return;
        }

        AddScriptAssetsFromRendering(renderingItem);
        AddInlineScriptFromRendering(renderingItem);
        AddStylingAssetsFromRendering(renderingItem);
        AddInlineStylingFromAssets(renderingItem);
      }
    }

    private static void AddInlineStylingFromAssets(Item renderingItem)
    {
      var cssInline = renderingItem[Templates.RenderingAssets.Fields.InlineStyling];
      if (!string.IsNullOrEmpty(cssInline))
      {
        AssetRepository.Current.AddStyling(cssInline, renderingItem.ID.ToString(), true);
      }
    }

    private static void AddStylingAssetsFromRendering(Item renderingItem)
    {
      var cssAssets = renderingItem[Templates.RenderingAssets.Fields.StylingFiles];
      foreach (var cssAsset in cssAssets.Split(';', ',', '\n'))
      {
        AssetRepository.Current.AddStyling(cssAsset, true);
      }
    }

    private static void AddInlineScriptFromRendering(Item renderingItem)
    {
      var javaScriptInline = renderingItem[Templates.RenderingAssets.Fields.InlineScript];
      if (!string.IsNullOrEmpty(javaScriptInline))
      {
        AssetRepository.Current.AddScript(javaScriptInline, renderingItem.ID.ToString(), ScriptLocation.Body, true);
      }
    }

    private static void AddScriptAssetsFromRendering(Item renderingItem)
    {
      var javaScriptAssets = renderingItem[Templates.RenderingAssets.Fields.ScriptFiles];
      foreach (var javaScriptAsset in javaScriptAssets.Split(';', ',', '\n'))
      {
        AssetRepository.Current.AddScript(javaScriptAsset, true);
      }
    }

    private Item GetRenderingItem(Rendering rendering)
    {
      if (rendering.RenderingItem == null)
      {
        Log.Warn($"rendering.RenderingItem is null for {rendering.RenderingItemPath}", this);
        return null;
      }

      if (Context.PageMode.IsNormal && rendering.Caching.Cacheable)
      {
        AssetRepository.Current.Add(rendering.RenderingItem.ID);
      }
      return rendering.RenderingItem.InnerItem;
    }

    private void AddPageAssets(Item item)
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

    private void AddDefaultAssetsFromConfiguration()
    {
      foreach (var asset in this.DefaultAssets)
      {
        AssetRepository.Current.Add(asset, true);
      }
    }
  }
}