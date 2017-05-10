namespace Sitecore.Foundation.Assets.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml;
    using Sitecore.Data;
    using Sitecore.Diagnostics;
    using Sitecore.Foundation.Assets.Models;
    using Sitecore.Mvc.Presentation;
    using Sitecore.Xml;

    /// <summary>A Repository for all assets required by renderings</summary>
    public class AssetRepository
    {
        private static readonly AssetRequirementCache _cache = new AssetRequirementCache(StringUtil.ParseSizeString("10MB"));

        [ThreadStatic]
        private static AssetRepository _current;

        private readonly List<Asset> _items = new List<Asset>();
        private readonly List<ID> _seenRenderings = new List<ID>();

        public static AssetRepository Current => _current ?? (_current = new AssetRepository());

        internal IEnumerable<Asset> Items => this._items;

        internal void Clear()
        {
            this._items.Clear();
        }

        public Asset Add(Asset asset, bool preventAddToCache = false)
        {
            if (asset == null)
            {
                throw new ArgumentNullException(nameof(asset));
            }

            if (asset.AddOnceToken != null)
            {
                if (this._items.Any(x => x.AddOnceToken != null && x.AddOnceToken == asset.AddOnceToken))
                {
                    return null;
                }
            }

            if (asset.Content != null)
            {
                if (this._items.Any(x => x.Content != null && x.Content == asset.Content))
                {
                    return null;
                }
            }

            if (!preventAddToCache)
            {
                if (RenderingContext.CurrentOrNull != null)
                {
                    var rendering = RenderingContext.CurrentOrNull.Rendering;
                    if (rendering != null && rendering.Caching.Cacheable)
                    {
                        AssetRequirementList cachedRequirements;

                        var renderingId = rendering.RenderingItem.ID;

                        if (!this._seenRenderings.Contains(renderingId))
                        {
                            this._seenRenderings.Add(renderingId);
                            cachedRequirements = new AssetRequirementList();
                        }
                        else
                        {
                            cachedRequirements = _cache.Get(renderingId) ?? new AssetRequirementList();
                        }

                        cachedRequirements.Add(asset);
                        _cache.Set(renderingId, cachedRequirements);
                    }
                }
            }

            // Passed the checks, add the requirement.
            this._items.Add(asset);
            return asset;
        }

        public void Add(ID renderingID)
        {
            // Check if rendering has already been executed in this page request
            // and if so, no need to add it again.
            if (this._seenRenderings.Contains(renderingID))
            {
                return;
            }

            var list = _cache.Get(renderingID);

            if (list == null)
            {
                return;
            }
            foreach (var requirement in list)
            {
                this.Add(requirement, true);
            }
        }

        public Asset AddScriptFile(string file, bool preventAddToCache = false)
        {
            return this.AddScriptFile(file, ScriptLocation.Body, preventAddToCache);
        }

        public Asset AddScriptFile(string file, ScriptLocation location, bool preventAddToCache = false)
        {
            return this.AddAsset(file, location, preventAddToCache, AssetType.JavaScript, AssetContentType.File);
        }

        public Asset AddInlineScript(string script, ScriptLocation location, bool preventAddToCache)
        {
            return this.AddAsset(script, location, preventAddToCache, AssetType.JavaScript, AssetContentType.Inline);
        }

        private Asset AddAsset(string content, ScriptLocation location, bool preventAddToCache, AssetType assetType, AssetContentType contentType, string site = null)
        {
            var asset = this.CreateAsset(content, location, assetType, contentType, site);
            return asset == null ? null : this.Add(asset, preventAddToCache);
        }

        private Asset CreateAsset(string content, ScriptLocation location, AssetType assetType, AssetContentType contentType, string site)
        {
            var cleanContent = this.CleanContent(content);
            if (cleanContent == null)
                return null;
            var asset = new Asset(assetType, location, cleanContent, contentType, site);
            return asset;
        }

        private string CleanContent(string content)
        {
            content = content?.Trim();
            return string.IsNullOrWhiteSpace(content) ? null : content;
        }

        public Asset AddStylingFile(string file, bool preventAddToCache = false)
        {
            return this.AddAsset(file, ScriptLocation.Head, preventAddToCache, AssetType.Css, AssetContentType.File);
        }

        public Asset AddInlineStyling(string styling, bool preventAddToCache)
        {
            return this.AddAsset(styling, ScriptLocation.Head, preventAddToCache, AssetType.Css, AssetContentType.Inline);
        }

        internal Asset CreateFromConfiguration(XmlNode node)
        {
            var assetTypeString = XmlUtil.GetAttribute("type", node, null);
            var assetFile = XmlUtil.GetAttribute("file", node, null);
            var scriptLocationString = XmlUtil.GetAttribute("location", node, null);
            var site = XmlUtil.GetAttribute("site", node, null);
            var inlineValue = node.InnerXml;

            if (string.IsNullOrWhiteSpace(assetTypeString))
            {
                Log.Warn($"Invalid asset in GetPageRendering.AddAssets pipeline: {node.OuterXml}", this);
                return null;
            }
            AssetType assetType;
            if (!Enum.TryParse(assetTypeString, true, out assetType))
            {
                Log.Warn($"Invalid asset type in GetPageRendering.AddAssets pipeline: {node.OuterXml}", this);
                return null;
            }

            var scriptLocation = ScriptLocation.Body;
            if (scriptLocationString != null)
            {
                ScriptLocation location;
                if (!Enum.TryParse(scriptLocationString, true, out location))
                {
                    Log.Warn($"Invalid script location in GetPageRendering.AddAssets pipeline: {node.OuterXml}", this);
                    return null;
                }
                scriptLocation = location;
            }

            Asset asset = null;
            if (!string.IsNullOrEmpty(assetFile))
            {
                asset = this.CreateAsset(assetFile, scriptLocation, assetType, AssetContentType.File, site);
            }
            else if (!string.IsNullOrEmpty(inlineValue))
            {
                asset = this.CreateAsset(inlineValue, scriptLocation, assetType, AssetContentType.Inline, site);
            }

            return asset;
        }
    }
}