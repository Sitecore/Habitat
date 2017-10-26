namespace Sitecore.Foundation.Indexing.Repositories
{
    using Sitecore.Foundation.Indexing.Models;
    using Sitecore.Foundation.Indexing.Services;

    public interface ISearchServiceRepository
    {
        SearchService Get(ISearchSettings searchSettings);
    }
}