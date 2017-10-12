namespace Sitecore.Foundation.Assets.Compression
{
    using System;
    using System.Web;

    /// <inheritdoc />
    /// <summary>
    /// Based on the original ClientDependency.Core.PlaceholdersReplacedEventArgs
    /// Copied into this location as it was internal in the ClientDependency library but we need access to it.
    /// </summary>
    internal class PlaceholdersReplacedEventArgs : EventArgs
    {
        public HttpContextBase HttpContext { get; private set; }

        public string ReplacedText { get; set; }

        public PlaceholdersReplacedEventArgs(HttpContextBase httpContext, string replacedText)
        {
            this.HttpContext = httpContext;
            this.ReplacedText = replacedText;
        }
    }
}