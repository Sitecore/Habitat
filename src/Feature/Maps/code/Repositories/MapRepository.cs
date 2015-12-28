namespace Sitecore.Feature.Maps.Repositories
{
  using System;
  using System.Collections.Generic;
  using System.Linq;

  public class MapRepository : IMapRepository
  {
    private readonly News.Repositories.ISearchServiceRepository searchServiceRepository;

    public MapRepository() : this(new Foundation.Indexing.Repositories.SearchServiceRepository(new SearchSettingsRepository()))
    {
      
    }

    public MapRepository(News.Repositories.ISearchServiceRepository searchServiceRepository)
    {
      this.searchServiceRepository = searchServiceRepository;
    }

    public IEnumerable<Data.Items.Item> GetAll(Data.Items.Item contextItem)
    {
      if (contextItem == null)
      {
        throw new ArgumentNullException(nameof(contextItem));
      }
      if (Foundation.SitecoreExtensions.Extensions.ItemExtensions.IsDerived(contextItem, Templates.MapPoint.ID))
      {
        return new List<Data.Items.Item> { contextItem };
      }
      if (!Foundation.SitecoreExtensions.Extensions.ItemExtensions.IsDerived(contextItem, Templates.MapPointsFolder.ID))
      {
        throw new ArgumentException("Item must derive from MapPointsFolder or MapPoint", nameof(contextItem));
      }

      var searchService = searchServiceRepository.Get();
      searchService.Settings.Root = contextItem;

      return searchService.FindAll().Results.Select(x => x.Item);
    }    
  }
}
