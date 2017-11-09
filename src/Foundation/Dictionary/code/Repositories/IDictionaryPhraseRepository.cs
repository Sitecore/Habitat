using Sitecore.Data;
using Sitecore.Data.Items;

namespace Sitecore.Foundation.Dictionary.Repositories
{
  public interface IDictionaryPhraseRepository
  {
    string Get(string relativePath, string defaultValue = "");
    string GetPlural(string relativePath, ID fieldId, string defaultValue = "");
    Item GetItem(string relativePath, string defaultValue = "");
    Item GetPluralItem(string relativePath, string defaultValue = "");
  }
}