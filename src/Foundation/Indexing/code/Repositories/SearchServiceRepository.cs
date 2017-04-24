namespace Sitecore.Foundation.Indexing.Repositories
{
    using Sitecore.Foundation.DependencyInjection;
    using Sitecore.Foundation.Indexing.Models;
    using Sitecore.Foundation.Indexing.Services;

    public class SearchServiceRepository : ISearchServiceRepository
    {
        public virtual SearchService Get(ISearchSettings settings)
        {
            return new SearchService(settings);
        }
    }
}