namespace Sitecore.Feature.Maps.Repositories
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using Foundation.SitecoreExtensions.Extensions;

  public class MapPointRepository : IMapPointRepository
  {
    private readonly News.Repositories.ISearchServiceRepository searchServiceRepository;

    public MapPointRepository() : this(new Foundation.Indexing.Repositories.SearchServiceRepository(new SearchSettingsRepository()))
    {
    }

    public MapPointRepository(News.Repositories.ISearchServiceRepository searchServiceRepository)
    {
      this.searchServiceRepository = searchServiceRepository;
    }

    public IEnumerable<Data.Items.Item> GetAll(Data.Items.Item contextItem)
    {
      if (contextItem == null)
      {
        throw new ArgumentNullException(nameof(contextItem));
      }
      if (contextItem.IsDerived(Templates.MapPoint.ID))
      {
        return new List<Data.Items.Item>
               {
                 contextItem
               };
      }
      if (!contextItem.IsDerived(Templates.MapPointsFolder.ID))
      {
        throw new ArgumentException("Item must derive from MapPointsFolder or MapPoint", nameof(contextItem));
      }

      var searchService = searchServiceRepository.Get();
      searchService.Settings.Root = contextItem;

      return searchService.FindAll().Results.Select(x => x.Item);
    }
  }
}