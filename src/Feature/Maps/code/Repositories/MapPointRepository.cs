﻿namespace Sitecore.Feature.Maps.Repositories
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using Models;
  using Foundation.SitecoreExtensions.Extensions;

  public class MapPointRepository : IMapPointRepository
  {
    private readonly Foundation.Indexing.Repositories.ISearchServiceRepository searchServiceRepository;

    public MapPointRepository() : this(new Foundation.Indexing.Repositories.SearchServiceRepository(new Foundation.Indexing.Models.SearchSettingsBase { Templates = new[] { Templates.MapPoint.ID } }))
    {
    }

    public MapPointRepository(Foundation.Indexing.Repositories.ISearchServiceRepository searchServiceRepository)
    {
      this.searchServiceRepository = searchServiceRepository;
    }

    public IEnumerable<MapPoint> GetAll(Data.Items.Item contextItem)
    {
      if (contextItem == null)
      {
        throw new ArgumentNullException(nameof(contextItem));
      }
      if (contextItem.IsDerived(Templates.MapPoint.ID))
      {
        return new List<MapPoint>
               {
                 new MapPoint(contextItem)
               };
      }
      if (!contextItem.IsDerived(Templates.MapPointsFolder.ID))
      {
        throw new ArgumentException("Item must derive from MapPointsFolder or MapPoint", nameof(contextItem));
      }

      var searchService = this.searchServiceRepository.Get();
      searchService.Settings.Root = contextItem;

      return searchService.FindAll().Results.Select(x => new MapPoint(x.Item));
    }
  }
}