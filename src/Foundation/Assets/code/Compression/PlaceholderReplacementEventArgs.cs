namespace Sitecore.Foundation.Assets.Compression
{
    using System.Text.RegularExpressions;
    using System.Web;
    using ClientDependency.Core;

    internal class PlaceholderReplacementEventArgs : PlaceholdersReplacedEventArgs
    {
        public ClientDependencyType Type { get; private set; }

        public Match RegexMatch { get; private set; }

        public PlaceholderReplacementEventArgs(HttpContextBase httpContext, ClientDependencyType type,
            string replacedText, Match regexMatch)
            : base(httpContext, replacedText)
        {
            this.Type = type;
            this.RegexMatch = regexMatch;
        }
    }
}