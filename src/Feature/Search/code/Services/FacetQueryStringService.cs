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

        public Dictionary<string, string[]> ParseFacets(string facetQueryString)
        {
            var returnValue = new Dictionary<string, string[]>();
            if (string.IsNullOrWhiteSpace(facetQueryString))
                return returnValue;

            var facetsValuesStrings = facetQueryString.Split(new [] {FacetsSeparator}, StringSplitOptions.RemoveEmptyEntries);
            foreach (var facetValuesString in facetsValuesStrings)
            {
                var facetValues = facetValuesString.Split('=');
                if (facetValues.Length <= 1)
                    continue;

                var name = facetValues[0].ToLowerInvariant();
                var values = facetValues[1].Split(new [] {FacetValueSeparator}, StringSplitOptions.RemoveEmptyEntries).Distinct().ToArray();
                if (returnValue.ContainsKey(name))
                    returnValue[name] = returnValue[name].Union(values).Distinct().ToArray();
                else
                    returnValue.Add(name, values);
            }
            return returnValue;
        }
    }
}