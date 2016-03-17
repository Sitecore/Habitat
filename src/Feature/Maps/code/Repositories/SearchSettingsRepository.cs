namespace Sitecore.Feature.Maps.Repositories
{
  using System.Collections.Generic;
  using Data;

  public class SearchSettingsRepository : Foundation.Indexing.Repositories.SearchSettingsRepositoryBase
  {      
    public override Foundation.Indexing.Models.ISearchSettings Get()
    {
      return new Foundation.Indexing.Models.SearchSettingsBase
      {   
        Templates = new List<ID>
        {
          Templates.MapPoint.ID
        }
      };
    }
  }
}