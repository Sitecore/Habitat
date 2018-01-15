namespace Sitecore.Foundation.Assets.Compression
{
    using System;
    using System.Collections.Generic;
    using System.Web;
    using ClientDependency.Core;
    using ClientDependency.Core.Config;
    using ClientDependency.Core.FileRegistration.Providers;
    using ClientDependency.Core.Mvc;

    /// <inheritdoc />
    /// <summary>
    /// Based on the original ClientDependency.Core.Mvc.SitecoreDependencyLoader
    /// Modified for the purposes of Rendering a single JS and CSS placholder and replacing with the asset file paths provided.
    /// this.GenerateOutput(type); in ClientDependencies was a private method and access was needed for rendering purposes.
    /// </summary>
    public class SitecoreDependencyLoader : BaseLoader
    {
        private static readonly object Locker = new object();
        
        private readonly List<RendererOutput> _output = new List<RendererOutput>();
        public HashSet<IClientDependencyPath> Paths = new HashSet<IClientDependencyPath>();
        public List<IClientDependencyFile> SitecoreDependencies = new List<IClientDependencyFile>();
        public const string ContextKey = "MvcLoader";
        private const string JsMarkupRegex = "<!--\\[Javascript:Name=\"(?<renderer>.*?)\"\\]//-->";
        private const string CssMarkupRegex = "<!--\\[Css:Name=\"(?<renderer>.*?)\"\\]//-->";

        private SitecoreDependencyLoader(HttpContextBase ctx) : base(ctx)
        {
            this.Provider = (BaseFileRegistrationProvider) ClientDependencySettings.Instance.DefaultMvcRenderer;
            if (ctx.Items.Contains((object) "MvcLoader"))
                throw new InvalidOperationException("Only one ClientDependencyLoader may exist in a context");
            ctx.Items[(object) "MvcLoader"] = (object) this;
        }
        
        public string ParseJsPlaceholders(string html, ClientDependencyType type)
        {
            this.GenerateOutput(type);
            return SitecorePlaceholderParser.ParseJsPlaceholders(this.CurrentContext, html,
                "<!--\\[Javascript:Name=\"(?<renderer>.*?)\"\\]//-->",
                this._output.ToArray());
        }

        public string ParseCssPlaceholders(string html, ClientDependencyType type)
        {
            this.GenerateOutput(type);
            return SitecorePlaceholderParser.ParseCssPlaceholders(this.CurrentContext, html,
                "<!--\\[Css:Name=\"(?<renderer>.*?)\"\\]//-->",
                this._output.ToArray());
        }

        public string RenderSitecoreJsPlaceholder(List<IClientDependencyPath> paths, List<IClientDependencyFile> sitecoreDeps)
        {
            this.SitecoreDependencies = sitecoreDeps;
            this.Paths.UnionWith(paths);
            var placeholder = $"<!--[{(object)ClientDependencyType.Javascript}:Name=\"{(object)this.Provider.Name}\"]//-->";
            return this.ParseJsPlaceholders(placeholder, ClientDependencyType.Javascript);
        }

        public string RenderSitecoreCssPlaceholder(List<IClientDependencyPath> paths, List<IClientDependencyFile> sitecoreDeps)
        {
            this.SitecoreDependencies = sitecoreDeps;
            this.Paths.UnionWith(paths);
            var placeholder = $"<!--[{(object)ClientDependencyType.Css}:Name=\"{(object)this.Provider.Name}\"]//-->";
            return this.ParseCssPlaceholders(placeholder, ClientDependencyType.Css);
        }

        public static SitecoreDependencyLoader TryCreate(HttpContextBase ctx, out bool isNew)
        {
            if (SitecoreDependencyLoader.GetInstance(ctx) == null)
            {
                lock (SitecoreDependencyLoader.Locker)
                {
                    if (DependencyRenderer.GetInstance(ctx) == null)
                    {
                        SitecoreDependencyLoader dependencyRenderer = new SitecoreDependencyLoader(ctx);
                        isNew = true;
                        return dependencyRenderer;
                    }
                }
            }
            isNew = false;
            return SitecoreDependencyLoader.GetInstance(ctx);
        }

        public static SitecoreDependencyLoader GetInstance(HttpContextBase ctx)
        {
            if (!ctx.Items.Contains((object) "MvcLoader"))
                return (SitecoreDependencyLoader) null;
            return ctx.Items[(object) "MvcLoader"] as SitecoreDependencyLoader;
        }

        protected void GenerateOutput(ClientDependencyType type)
        {
            string jsOutput;
            string cssOutput;
            ((BaseRenderer)this.Provider).RegisterDependencies(this.SitecoreDependencies, new HashSet<IClientDependencyPath>(), out jsOutput, out cssOutput, this.CurrentContext);
            this._output.Add(new RendererOutput()
            {
                Name = this.Provider.Name,
                OutputCss = cssOutput,
                OutputJs = jsOutput
            });
        }
    }
}