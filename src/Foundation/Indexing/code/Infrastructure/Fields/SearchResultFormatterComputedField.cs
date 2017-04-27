#region using

#endregion

namespace Sitecore.Foundation.Indexing.Infrastructure.Fields
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using Sitecore.ContentSearch;
    using Sitecore.ContentSearch.ComputedFields;
    using Sitecore.ContentSearch.Linq.Common;
    using Sitecore.ContentSearch.Linq.Methods;
    using Sitecore.ContentSearch.Pipelines.GetFacets;
    using Sitecore.ContentSearch.Pipelines.ProcessFacets;
    using Sitecore.Foundation.Indexing.Infrastructure.Providers;
    using Sitecore.Foundation.Indexing.Models;
    using Sitecore.Foundation.Indexing.Repositories;
    using Sitecore.Foundation.SitecoreExtensions.Extensions;

    public class SearchResultFormatterComputedField : IVirtualFieldProcessor, IComputedIndexField
    {
        public string FieldName { get; set; } = Constants.IndexFields.SearchResultFormatter;
        public string ReturnType { get; set; }

        public object ComputeFieldValue(IIndexable indexable)
        {
            var indexItem = indexable as SitecoreIndexableItem;
            var item = indexItem?.Item;
            if (item == null)
                return null;

            var formatter = IndexingProviderRepository.SearchResultFormatters.FirstOrDefault(provider => provider.SupportedTemplates.Any(item.IsDerived));
            if (formatter == null || formatter is FallbackSearchResultFormatter)
                return null;
            return formatter.GetType().AssemblyQualifiedName;

        }

        private string TranslateToFormatter(string fieldValue)
        {
            return IndexingProviderRepository.SearchResultFormatters.FirstOrDefault(provider => provider.ContentType == fieldValue)?.GetType().AssemblyQualifiedName;
        }

        private string TranslateToContentType(string fieldValue)
        {
            return IndexingProviderRepository.SearchResultFormatters.FirstOrDefault(provider => provider.GetType().AssemblyQualifiedName == fieldValue)?.ContentType;
        }

        public TranslatedFieldQuery TranslateFieldQuery(string fieldName, object fieldValue, ComparisonType comparison, FieldNameTranslator fieldNameTranslator)
        {
            var queryValue = fieldValue != null ? this.TranslateToFormatter(fieldValue.ToString()) : null;
            if (queryValue == null)
                return null;

            var query = new TranslatedFieldQuery();
            query.FieldComparisons.Add(new Tuple<string, object, ComparisonType>(this.FieldName, queryValue, comparison));
            return query;
        }

        public IDictionary<string, object> TranslateFieldResult(IDictionary<string, object> fields, FieldNameTranslator fieldNameTranslator)
        {
            var indexFieldName = fieldNameTranslator.GetIndexFieldName(this.FieldName);
            if (string.IsNullOrEmpty(indexFieldName) || !fields.ContainsKey(indexFieldName))
                return fields;
            var formatterId = fields[indexFieldName];
            if (formatterId == null)
                return fields;
            var value = this.TranslateToContentType(formatterId.ToString());
            if (value == null)
                return fields;
            if (fields.ContainsKey(Constants.IndexFields.ContentType))
                fields[Constants.IndexFields.ContentType] = value;
            else
                fields.Add(Constants.IndexFields.ContentType, value);
            return fields;
        }

        public GetFacetsArgs TranslateFacetQuery(GetFacetsArgs args)
        {
            var facetQueries = args.FacetQueries.ToList();
            var list = facetQueries.Where(q => (q.FieldNames.Count() == 1) && q.FieldNames.First() == Constants.IndexFields.ContentType).ToList();
            if (list.Count > 0)
            {
                var resultCount = list[0].MinimumResultCount;
                var fieldNames = new[] {this.FieldName };
                facetQueries.Add(new FacetQuery(null, fieldNames, resultCount, null));
                list.ForEach(delegate (FacetQuery f) {facetQueries.Remove(f);});
            }

            return new GetFacetsArgs(args.BaseQuery, facetQueries, args.VirtualFieldProcessors, args.FieldNameTranslator);
        }

        public IDictionary<string, ICollection<KeyValuePair<string, int>>> TranslateFacetResult(ProcessFacetsArgs args)
        {
            ICollection<KeyValuePair<string, int>> values;
            var newValues = new List<KeyValuePair<string, int>>();
            var facets = args.Facets;
            var indexFieldName = args.FieldNameTranslator.GetIndexFieldName(this.FieldName);
            if (facets.TryGetValue(indexFieldName, out values))
            {
                facets.Remove(indexFieldName);
                foreach (var value in values)
                {
                    var translatedValue = this.TranslateToContentType(value.Key);
                    if (translatedValue == null)
                        continue;
                    newValues.Add(new KeyValuePair<string, int>(translatedValue, value.Value));
                }
                if (newValues.Any())
                {
                    facets[Constants.IndexFields.ContentType] = newValues;
                }
            }
            return facets;
        }
    }
}