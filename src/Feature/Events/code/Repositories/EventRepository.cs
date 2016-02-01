namespace Sitecore.Feature.Events.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Sitecore.Data.Items;
    using Sitecore.Foundation.Indexing.Models;
    using Sitecore.Foundation.Indexing.Repositories;

    public class EventRepository : IEventRepository
    {
        public Item ContextItem { get; set; }

        private readonly ISearchServiceRepository searchServiceRepository;

        public EventRepository(Item contextItem) : this(contextItem, new SearchServiceRepository(new SearchSettingsRepository())) { }

        public EventRepository(Item contextItem, ISearchServiceRepository searchServiceRepository)
        {
            if (contextItem == null)
            {
                throw new ArgumentNullException(nameof(contextItem));
            }
            
            ContextItem = contextItem;

            this.searchServiceRepository = searchServiceRepository;
        }

        public IEnumerable<Item> Get()
        {
            var results = GetSearchResults();
            return results.Results.Select(x => x.Item).OrderByDescending(i => i[Templates.Event.Fields.StartDate]);
        }

        public IEnumerable<Item> GetCalendarEvents(int month)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Item> GetLatest(int count)
        {
            return Get().Take(count);
        }

        private ISearchResults GetSearchResults()
        {
            var searchService = searchServiceRepository.Get();
            searchService.Settings.Root = ContextItem;
            var results = searchService.FindAll();
            return results;
        }
    }
}