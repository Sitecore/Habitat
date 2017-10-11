namespace Sitecore.Foundation.Assets.Compression
{
    using System;
    using System.Web;

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