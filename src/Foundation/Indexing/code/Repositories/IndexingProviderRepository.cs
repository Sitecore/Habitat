#region using

#endregion

namespace Sitecore.Foundation.Indexing.Repositories
{
    using System.Collections.Generic;
    using System.Configuration;
    using System.Configuration.Provider;
    using System.Linq;
    using Sitecore.Configuration;
    using Sitecore.Configuration.Providers;
    using Sitecore.Foundation.Indexing.Models;

    internal static class IndexingProviderRepository
    {
        private static IEnumerable<ProviderBase> _all;
        private static ISearchResultFormatter _defaultSearchResultFormatter;

        public static IEnumerable<ProviderBase> All
        {
            get
            {
                if (_all == null)
                {
                    Initialize();
                }
                return _all;
            }
            private set
            {
                _all = value;
            }
        }

        public static IEnumerable<IQueryPredicateProvider> QueryPredicateProviders
        {
            get
            {
                return All.Where(p => p is IQueryPredicateProvider).Cast<IQueryPredicateProvider>();
            }
        }

        public static IEnumerable<IQueryFacetProvider> QueryFacetProviders
        {
            get
            {
                return All.Where(p => p is IQueryFacetProvider).Cast<IQueryFacetProvider>();
            }
        }

        public static ISearchResultFormatter DefaultSearchResultFormatter
        {
            get
            {
                if (_defaultSearchResultFormatter == null)
                {
                    Initialize();
                }
                return _defaultSearchResultFormatter;
            }
            private set
            {
                _defaultSearchResultFormatter = value;
            }
        }

        public static IEnumerable<ISearchResultFormatter> SearchResultFormatters
        {
            get
            {
                return All.Where(p => p is ISearchResultFormatter).Cast<ISearchResultFormatter>();
            }
        }

        public static IEnumerable<IQueryRoot> QueryRootProviders
        {
            get
            {
                return All.Where(p => p is IQueryRoot).Cast<IQueryRoot>();
            }
        }

        private static void Initialize()
        {
            ProviderBase defaultProvider;
            var providers = Factory.GetProviders<ProviderBase, ProviderCollectionBase<ProviderBase>>("solutionFramework/indexing", out defaultProvider);
            var defaultSearchResultFormatter = defaultProvider as ISearchResultFormatter;
            if (defaultSearchResultFormatter == null)
            {
                throw new ConfigurationErrorsException("The default solutionFramework/indexing provider must derive from ISearchResultFormatter");
            }

            _all = providers;
            _defaultSearchResultFormatter = defaultSearchResultFormatter;
        }
    }
}