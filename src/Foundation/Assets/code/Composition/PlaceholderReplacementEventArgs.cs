namespace Sitecore.Foundation.Assets.Compression
{
    using System.Text.RegularExpressions;
    using System.Web;
    using ClientDependency.Core;

    /// <inheritdoc />
    /// <summary>
    /// Based on the original ClientDependency.Core.PlaceholderParser
    /// Copied into this location as it was internal in the ClientDependency library but we need access to it.
    /// </summary>
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