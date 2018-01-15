﻿namespace Sitecore.Foundation.Assets.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Web;
    using System.Web.Mvc;
    using ClientDependency.Core;
    using ClientDependency.Core.Mvc;
    using Sitecore.Foundation.Assets.Compression;
    using Sitecore.Foundation.Assets.Models;
    using Sitecore.Foundation.Assets.Repositories;

    /// <inheritdoc />
    /// <summary>
    ///     A service for combining your javascript and css into a single server HTTP call on the page. 
    ///     Performs minification
    ///     Combines multiple asset files into a single cached request. 
    ///     Uses the ClientDependencies library under the hood.
    /// 
    ///     To use this service you will need to rename the following:
    ///     - "App_Config/ClientDependency.config.disabled".
    ///     - "\src\Foundation\Assets\code\Web.config.transform.ClientDependency.minification.example"  to      Web.config.transform
    /// 
    ///     Usage in Layouts  -  @CompositeAssetFileService.Current.RenderStyles()
    ///                       -  @CompositeAssetFileService.Current.RenderScript(ScriptLocation.Body)
    /// 
    ///     Documentation For ClientDependency:   https://github.com/Shazwazza/ClientDependency/wiki
    /// </summary>
    public class CompositeAssetFileService : RenderAssetsService
    {
        private static CompositeAssetFileService _current;
        public new static CompositeAssetFileService Current => _current ?? (_current = new CompositeAssetFileService());

        public override HtmlString RenderScript(ScriptLocation location)
        {
            return this.RenderAssetType(location, AssetType.JavaScript);
        }

        public override HtmlString RenderStyles()
        {
            List<String> locations = this.GetFilePaths(AssetType.Css);
            return this.RenderAssetType(locations, AssetType.Css);
        }

        protected HtmlString RenderAssetType(ScriptLocation location, AssetType assetType)
        {
            List<String> locations = this.GetFilePaths(location, assetType);
            return this.RenderAssetType(locations, assetType);
        }

        protected HtmlString RenderAssetType(List<String> locations, AssetType type, int group = 0, int baseIndex = 0)
        {
            List<IClientDependencyFile> dependencies = new List<IClientDependencyFile>();
            int index = baseIndex;
            foreach (var locationJs in locations)
            {
                string path = locationJs;
                if (path.Contains("//") && !path.Contains("?"))
                {
                    path = path.Replace("\r", string.Empty);
                    path = path.Replace("https://", "//");
                    path = path.Replace("http://", "//");
                }

                ClientDependencyType clientType = type == AssetType.JavaScript ? ClientDependencyType.Javascript : ClientDependencyType.Css;

                BasicFile basicFile = new BasicFile(clientType)
                {
                    Group = group,
                    Priority = index,
                    FilePath = path,
                    PathNameAlias = string.Empty,
                    ForceProvider = string.Empty,
                    ForceBundle = true
                };
                dependencies.Add(basicFile);
                index++;
            }

            if (type == AssetType.JavaScript)
            {
                return new HtmlString(DependencyLoadAccess.GetLoader(new HttpContextWrapper(HttpContext.Current)).RenderSitecoreJsPlaceholder(new List<IClientDependencyPath>(), dependencies));
            }
            else
            {
                return new HtmlString(DependencyLoadAccess.GetLoader(new HttpContextWrapper(HttpContext.Current)).RenderSitecoreCssPlaceholder(new List<IClientDependencyPath>(), dependencies));
            }
        }
    }
}