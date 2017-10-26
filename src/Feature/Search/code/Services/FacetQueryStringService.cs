using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sitecore.Feature.Search.Services
{
    using Sitecore.Foundation.DependencyInjection;

    [Service]
    public class FacetQueryStringService
    {
        private const char FacetValueSeparator = '|';
        private const char FacetsSeparator = '&';
        private const char FacetSeparator = '=';

        public Dictionary<string, string[]> ParseFacets(string facetQueryString)
        {
            facetQueryString = HttpUtility.UrlDecode(facetQueryString); 

            var returnValue = new Dictionary<string, string[]>();
            if (string.IsNullOrWhiteSpace(facetQueryString))
                return returnValue;

            var facetsValuesStrings = facetQueryString.Split(new [] {FacetsSeparator}, StringSplitOptions.RemoveEmptyEntries);
            foreach (var facetValuesString in facetsValuesStrings)
            {
                var facetValues = facetValuesString.Split(FacetSeparator);
                if (facetValues.Length <= 1)
                    continue;

                var name = facetValues[0].ToLowerInvariant();
                var values = facetValues[1].Split(FacetValueSeparator).Distinct().ToArray();
                if (returnValue.ContainsKey(name))
                    returnValue[name] = returnValue[name].Union(values).Distinct().ToArray();
                else
                    returnValue.Add(name, values);
            }
            return returnValue;
        }

        public string GetFacetQueryString(Dictionary<string, string[]> searchQueryFacets)
        {
            var returnValue = "";
            foreach (var key in searchQueryFacets.Keys)
            {
                var values = string.Join(FacetValueSeparator.ToString(), searchQueryFacets[key]);
                if (!string.IsNullOrEmpty(returnValue))
                {
                    returnValue += FacetsSeparator;
                }

                returnValue += key + FacetSeparator + values;
            }
            return HttpUtility.UrlEncode(returnValue);
        }

        public string ToggleFacet(string facetQueryString, string facetName, string facetValue)
        {
            var parsedFacets = this.ParseFacets(facetQueryString);
            if (parsedFacets.ContainsKey(facetName))
            {
                var values = parsedFacets[facetName];
                if (values.Any(v => v.Equals(facetValue, StringComparison.InvariantCultureIgnoreCase)))
                {
                    //Remove facet value from list
                    values = values.Where(v => !v.Equals(facetValue, StringComparison.InvariantCultureIgnoreCase)).ToArray();
                }
                else
                {
                    //Add facet value from list
                    values = values.Union(new[] {facetValue}).ToArray();
                }

                if (!values.Any())
                {
                    parsedFacets.Remove(facetName);
                }
                else
                {
                    parsedFacets[facetName] = values;
                }
            }
            else
            {
                parsedFacets[facetName] = new[] { facetValue };
            }
            return this.GetFacetQueryString(parsedFacets);
        }
    }
}