namespace Sitecore.Foundation.Assets.Compression
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Web;
    using ClientDependency.Core;

    /// <summary>
    /// Based on the original ClientDependency.Core.PlaceholderParser
    /// Modified for the purposes of replacing our JS and CSS placeholder.
    /// </summary>
    public class SitecorePlaceholderParser
    {
        public static string ParseJsPlaceholders(HttpContextBase currentContext, string html, string jsMarkupRegex, RendererOutput[] output)
        {
            html = Regex.Replace(html, jsMarkupRegex, (MatchEvaluator) (m =>
            {
                Group grp = m.Groups["renderer"];
                if (grp == null)
                    return m.ToString();
                if (!((IEnumerable<RendererOutput>) output).Any<RendererOutput>())
                    return "<!-- CDF: No JS dependencies were declared //-->";
                RendererOutput rendererOutput =
                    ((IEnumerable<RendererOutput>) output).SingleOrDefault<RendererOutput>(
                        (Func<RendererOutput, bool>) (x => x.Name == grp.ToString() && !string.IsNullOrEmpty(x.OutputJs)));
                PlaceholderReplacementEventArgs e = new PlaceholderReplacementEventArgs(currentContext,
                    ClientDependencyType.Javascript, rendererOutput != null ? rendererOutput.OutputJs : "", m);
                
                return e.ReplacedText;
            }), RegexOptions.Compiled);
           
            PlaceholdersReplacedEventArgs e1 = new PlaceholdersReplacedEventArgs(currentContext, html);
            
            return e1.ReplacedText;
        }

        public static string ParseCssPlaceholders(HttpContextBase currentContext, string html, string cssMarkupRegex, RendererOutput[] output)
        {
            html = Regex.Replace(html, cssMarkupRegex, (MatchEvaluator)(m =>
            {
                Group grp = m.Groups["renderer"];
                if (grp == null)
                    return m.ToString();
                if (!((IEnumerable<RendererOutput>)output).Any<RendererOutput>())
                    return "<!-- CDF: No CSS dependencies were declared //-->";
                RendererOutput rendererOutput =
                    ((IEnumerable<RendererOutput>)output).SingleOrDefault<RendererOutput>(
                        (Func<RendererOutput, bool>)(x => x.Name == grp.ToString() && !string.IsNullOrEmpty(x.OutputCss)));
                PlaceholderReplacementEventArgs e = new PlaceholderReplacementEventArgs(currentContext,
                    ClientDependencyType.Css, rendererOutput != null ? rendererOutput.OutputCss : "", m);

                return e.ReplacedText;
            }), RegexOptions.Compiled);
            PlaceholdersReplacedEventArgs e1 = new PlaceholdersReplacedEventArgs(currentContext, html);

            return e1.ReplacedText;
        }
    }
}