using Sitecore.Data.Items;

namespace Sitecore.Foundation.Dictionary.Repositories
{
  public interface IDictionaryPhraseRepository
  {
    string Get(string relativePath, string defaultValue = "");
    Item GetItem(string relativePath, string defaultValue = "");
  }
}