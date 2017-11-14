namespace Sitecore.Foundation.Dictionary.Repositories
{
  using System;
  using System.Linq;
  using System.Web;
  using Sitecore.Data;
  using Sitecore.Data.Items;
  using Sitecore.Diagnostics;
  using Sitecore.Foundation.Dictionary.Models;
  using Sitecore.Foundation.Dictionary.Services;
  using Sitecore.SecurityModel;

  public class DictionaryPhraseRepository : IDictionaryPhraseRepository
  {
    public Dictionary Dictionary { get; set; }

    public DictionaryPhraseRepository(Dictionary dictionary)
    {
      this.Dictionary = dictionary;
    }

    public static IDictionaryPhraseRepository Current => GetCurrentFromCacheOrCreate();

    private static IDictionaryPhraseRepository GetCurrentFromCacheOrCreate()
    {
      if (HttpContext.Current != null)
      {
        var repository = HttpContext.Current.Items["DictionaryPhraseRepository.Current"] as IDictionaryPhraseRepository;
        if (repository != null)
        {
          return repository;
        }
      }
      var returnValue = new DictionaryPhraseRepository(DictionaryRepository.Current);

      if (HttpContext.Current != null)
      {
        HttpContext.Current.Items.Add("DictionaryPhraseRepository.Current", returnValue);
      }
      return returnValue;
    }

    public string Get([NotNull] string relativePath, string defaultValue)
    {
      return this.Get(relativePath, Templates.DictionaryEntry.Fields.Phrase, false, defaultValue);
    }

    public string GetPlural([NotNull] string relativePath, ID fieldId, string defaultValue = "")
    {
      return this.Get(relativePath, fieldId, true, defaultValue);
    }

    private string Get([NotNull] string relativePath, ID fieldId, bool plural, string defaultValue)
    {
      if (relativePath == null)
      {
        throw new ArgumentNullException(nameof(relativePath));
      }
      if (Context.Database == null)
      {
        return defaultValue;
      }

      var dictionaryItem = this.GetOrAutoCreateItem(relativePath, plural, defaultValue);
      if (dictionaryItem == null)
      {
        return defaultValue;
      }

      return dictionaryItem.Fields[fieldId].Value ?? defaultValue;
    }

    public Item GetItem([NotNull] string relativePath, string defaultValue = "")
    {
        return this.GetItem(relativePath, false, defaultValue);
    }

    public Item GetPluralItem([NotNull] string relativePath, string defaultValue = "")
    {
        return this.GetItem(relativePath, true, defaultValue);
    }

    private Item GetItem([NotNull] string relativePath, bool plural, string defaultValue = "")
    {
      if (relativePath == null)
      {
        throw new ArgumentNullException(nameof(relativePath));
      }

      var item = this.GetOrAutoCreateItem(relativePath, plural, defaultValue);
      if (item == null)
      {
        Log.Debug($"Could not find the dictionary item for the site '{this.Dictionary.Site.Name}' with the path '{relativePath}'", this);
      }
      return item;
    }

    private Item GetOrAutoCreateItem([NotNull]string relativePath, bool plural, [CanBeNull]string defaultValue)
    {
      relativePath = AssertRelativePath(relativePath);

      var item = this.Dictionary.Root.Axes.GetItem(relativePath);
      if (item != null)
        return item;

      if (!this.Dictionary.AutoCreate || defaultValue == null)
        return null;
      try
      {
        return CreateDictionaryEntryService.CreateDictionaryEntry(this.Dictionary, relativePath, plural, defaultValue);
      }
      catch (Exception ex)
      {
        Log.Error($"Failed to get or create {relativePath} from the dictionary in site {this.Dictionary.Site.Name}", ex, this);
        return null;
      }
    }

    private static string AssertRelativePath(string relativePath)
    {
      if (relativePath == null)
      {
        throw new ArgumentNullException(nameof(relativePath));
      }

      if (relativePath.StartsWith("/"))
      {
        relativePath = relativePath.Substring(1);
      }
      if (string.IsNullOrWhiteSpace(relativePath))
      {
        throw new ArgumentException("the path is not a valid relative path", nameof(relativePath));
      }
      return relativePath;
    }
  }
}