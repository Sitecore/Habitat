namespace Sitecore.Feature.Search.Repositories
{
  using Sitecore.Feature.Search.Models;

  public interface ISearchSettingsRepository
  {
    SearchSettings Get();
    SearchSettings Get(string query);

  }
}