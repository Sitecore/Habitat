using System.Collections.Generic;
using System.Linq;
using Sitecore;
using Sitecore.Data;
using Sitecore.Mvc.Presentation;
using Habitat.Framework.Assets.Models;

namespace Habitat.Framework.Assets
{
    /// <summary>A Repository for all assets required by renderings</summary>
    public class AssetRepository
    {
        /// <summary>Sitecore CustomCache object which holds the requirements for cacheable renderings</summary>
        private static readonly AssetRequirementCache Cache = new AssetRequirementCache(StringUtil.ParseSizeString("10MB"));

        private static AssetRepository _current;

        /// <summary>The requirements which have been found in renderings executed on this page request</summary>
        private readonly List<AssetRequirement> items = new List<AssetRequirement>();

        /// <summary>A list of rendering IDs which have been executed on this page request</summary>
        private readonly List<ID> seenRenderings = new List<ID>();

        public static AssetRepository Current => _current ?? (_current = new AssetRepository());

        /// <summary>The requirements which have been found in renderings executed on this page request</summary>
        internal IEnumerable<AssetRequirement> Items => this.items;

        internal void Add(AssetRequirement requirement, bool preventAddToCache = false)
        {
            // If this code block should only be added once per page, check that now.
            if (requirement.AddOnceToken != null)
            {
                if (this.items.Any(x => x.AddOnceToken != null && x.AddOnceToken == requirement.AddOnceToken))
                    return;
            }

            // If requirement is a file, check it hasn't been added already.
            if (requirement.File != null)
            {
                if (this.items.Any(x => x.File != null && x.File == requirement.File))
                    return;
            }

            // If rendering is cacheable it requires special attention. We need to make sure asset references
            // are also cached so we can process them elsewhere in the rendering pipeline.
            if (!preventAddToCache)
            {
                if (RenderingContext.Current != null)
                {
                    var rendering = RenderingContext.Current.Rendering;
                    if (rendering != null && rendering.Caching.Cacheable)
                    {
                        AssetRequirementList cachedRequirements;

                        var renderingId = rendering.RenderingItem.ID;

                        // Check if this is the first time we've seen this rendering during this page request
                        // If so, start from fresh with a new list of requirements
                        if (!this.seenRenderings.Contains(renderingId))
                        {
                            this.seenRenderings.Add(renderingId);
                            cachedRequirements = new AssetRequirementList();
                        }
                        else
                            cachedRequirements = Cache.Get(renderingId) ?? new AssetRequirementList();

                        cachedRequirements.Add(requirement);
                        Cache.Set(renderingId, cachedRequirements);
                    }
                }
            }

            // Passed the checks, add the requirement.
            this.items.Add(requirement);
        }

        /// <summary>Add requirements which would otherwise have been missed because of rendering caching</summary>
        /// <param name="renderingID">The Sitecore ID of the rendering</param>
        public void Add(ID renderingID)
        {
            // Check if rendering has already been executed in this page request
            // and if so, no need to add it again.
            if (this.seenRenderings.Contains(renderingID))
                return;

            var list = Cache.Get(renderingID);

            if (list != null)
            {
                foreach (var requirement in list)
                    this.Add(requirement, true);
            }
        }

        /// <summary>
        ///     Adds a script file to the list of assets on the page
        /// </summary>
        public void AddScript(string file, bool preventAddToCache = false)
        {
            this.Add(new AssetRequirement(AssetType.JavaScript, file), preventAddToCache);
        }

        /// <summary>
        ///     Adds inline script to the list of assets on the page
        /// </summary>
        public void AddScript(string script, string addOnceToken, ScriptLocation location, bool preventAddToCache = false)
        {
            this.Add(new AssetRequirement(AssetType.JavaScript, null, location, script, script.GetHashCode().ToString()), preventAddToCache);
        }

        /// <summary>
        ///     Adds a css file to the list of assets on the page
        /// </summary>
        public void AddStyling(string file, bool preventAddToCache = false)
        {
            this.Add(new AssetRequirement(AssetType.Css, file), preventAddToCache);
        }

        /// <summary>
        ///     Adds inline styling to the list of assets on the page
        /// </summary>
        public void AddStyling(string styling, string addOnceToken, bool preventAddToCache = false)
        {
            this.Add(new AssetRequirement(AssetType.Css, null, ScriptLocation.Head, styling, styling.GetHashCode().ToString()), preventAddToCache);
        }
    }
}