namespace Sitecore.Feature.Search.Infrastructure.Providers
{
    using System.Collections.Generic;
    using System.Configuration.Provider;
    using Sitecore.Feature.Search.Repositories;
    using Sitecore.Foundation.DependencyInjection;
    using Sitecore.Foundation.Indexing.Models;

    [Service]
    public class DefaultFacetProvider : ProviderBase, IQueryFacetProvider
    {
        public DefaultFacetProvider(ISearchContextRepository searchContextRepository)
        {
            this.SearchContextRepository = searchContextRepository;
        }

        public IEnumerable<IQueryFacet> GetFacets()
        {
            var context = this.SearchContextRepository.Get();
            return context.Facets;
        }

        public ISearchContextRepository SearchContextRepository { get; }
    }
}