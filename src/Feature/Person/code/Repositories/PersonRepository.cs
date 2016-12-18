namespace Sitecore.Feature.Person.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using Sitecore.Data.Items;
    using Sitecore.Foundation.Indexing.Models;
    using Sitecore.Foundation.Indexing.Repositories;

    public class PersonRepository
    {
        public IEnumerable<Item> Get(Item contextItem)
        {
            var searchServiceRepository = new SearchServiceRepository(new SearchSettingsBase { Templates = new[] { Templates.Person.ID } });
            var searchService = searchServiceRepository.Get();
            searchService.Settings.Root = contextItem;
            return searchService.FindAll().Results.Select(x => x.Item).Where(x => x != null);
        }
    }
}