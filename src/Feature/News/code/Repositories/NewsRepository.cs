namespace Sitecore.Feature.News.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Sitecore.Data.Items;
    using Sitecore.Foundation.DependencyInjection;
    using Sitecore.Foundation.Indexing.Models;
    using Sitecore.Foundation.Indexing.Repositories;

    [Service(typeof(INewsRepository))]
    public class NewsRepository : INewsRepository
    {
        private readonly ISearchServiceRepository searchServiceRepository;

        public NewsRepository(ISearchServiceRepository searchServiceRepository)
        {
            this.searchServiceRepository = searchServiceRepository;
        }

        public IEnumerable<Item> Get(Item contextItem)
        {
            if (contextItem == null)
            {
                throw new ArgumentNullException(nameof(contextItem));
            }
            if (!contextItem.DescendsFrom(Templates.NewsFolder.ID))
            {
                throw new ArgumentException("Item must derive from NewsFolder", nameof(contextItem));
            }

            var searchService = this.searchServiceRepository.Get(new SearchSettingsBase { Templates = new[] { Templates.NewsArticle.ID } });
            searchService.Settings.Root = contextItem;
            //TODO: Refactor for scalability
            var results = searchService.FindAll();
            return results.Results.Select(x => x.Item).Where(x => x != null).OrderByDescending(i => i[Templates.NewsArticle.Fields.Date]);
        }

        public IEnumerable<Item> GetLatest(Item contextItem, int count)
        {
            //TODO: Refactor for scalability
            return this.Get(contextItem).Take(count);
        }
    }
}