namespace Sitecore.Feature.Maps.Repositories
{
  public class SearchSettingsRepository : Foundation.Indexing.Repositories.SearchSettingsRepositoryBase
  {
    public override Foundation.Indexing.Models.ISearchSettings Get()
    {
      return new Foundation.Indexing.Models.SearchSettingsBase
             {
               Templates = new[] {Templates.MapPoint.ID}
             };
    }
  }
}