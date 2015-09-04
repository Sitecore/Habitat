using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Sitecore;
using Sitecore.Data;
using Sitecore.Mvc.Presentation;
using Habitat.Framework.Assets.Models;
using Sitecore.Diagnostics;
using Sitecore.Xml;

namespace Habitat.Framework.Assets
{
    /// <summary>A Repository for all assets required by renderings</summary>
    public class AssetRepository
    {
        private static readonly AssetRequirementCache _cache = new AssetRequirementCache(StringUtil.ParseSizeString("10MB"));
        private static AssetRepository _current;
        private readonly List<Asset> _items = new List<Asset>();
        private readonly List<ID> _seenRenderings = new List<ID>();

        public static AssetRepository Current => _current ?? (_current = new AssetRepository());

        internal IEnumerable<Asset> Items => this._items;

        internal void Add(Asset requirement, bool preventAddToCache = false)
        {
            if (requirement.AddOnceToken != null)
            {
                if (this._items.Any(x => x.AddOnceToken != null && x.AddOnceToken == requirement.AddOnceToken))
                    return;
            }

            if (requirement.File != null)
            {
                if (this._items.Any(x => x.File != null && x.File == requirement.File))
                    return;
            }

            if (!preventAddToCache)
            {
                if (RenderingContext.Current != null)
                {
                    var rendering = RenderingContext.Current.Rendering;
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
                            cachedRequirements = _cache.Get(renderingId) ?? new AssetRequirementList();

                        cachedRequirements.Add(requirement);
                        _cache.Set(renderingId, cachedRequirements);
                    }
                }
            }

            // Passed the checks, add the requirement.
            this._items.Add(requirement);
        }

        public void Add(ID renderingID)
        {
            // Check if rendering has already been executed in this page request
            // and if so, no need to add it again.
            if (this._seenRenderings.Contains(renderingID))
                return;

            var list = _cache.Get(renderingID);

            if (list != null)
            {
                foreach (var requirement in list)
                    this.Add(requirement, true);
            }
        }

        public void AddScript(string file, bool preventAddToCache = false)
        {
            this.Add(new Asset(AssetType.JavaScript, file), preventAddToCache);
        }

        public void AddScript(string script, string addOnceToken, ScriptLocation location, bool preventAddToCache = false)
        {
            this.Add(new Asset(AssetType.JavaScript, null, location, script, script.GetHashCode().ToString()), preventAddToCache);
        }

        public void AddStyling(string file, bool preventAddToCache = false)
        {
            this.Add(new Asset(AssetType.Css, file), preventAddToCache);
        }

        public void AddStyling(string styling, string addOnceToken, bool preventAddToCache = false)
        {
            this.Add(new Asset(AssetType.Css, null, ScriptLocation.Head, styling, styling.GetHashCode().ToString()), preventAddToCache);
        }

        internal Asset CreateFromConfiguration(XmlNode node)
        {
            var assetTypeString = XmlUtil.GetAttribute("type", node, null);
            var assetFile = XmlUtil.GetAttribute("file", node, null);
            var scriptLocationString = XmlUtil.GetAttribute("location", node, null);
            var innerValue = node.InnerXml;

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

            ScriptLocation? scriptLocation = null;
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
                if (scriptLocation.HasValue)
                    asset = new Asset(assetType, assetFile, scriptLocation.Value);
                else
                    asset = new Asset(assetType, assetFile);
            }
            else if (!string.IsNullOrEmpty(innerValue))
            {
                if (scriptLocation.HasValue)
                    asset = new Asset(assetType, null, inline: innerValue, addOnceToken: innerValue.GetHashCode().ToString(), location: scriptLocation.Value);
                else
                    asset = new Asset(assetType, null, inline: innerValue, addOnceToken: innerValue.GetHashCode().ToString());
            }

            return asset;
        }

    }
}