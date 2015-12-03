namespace Sitecore.Foundation.SitecoreExtensions.Repositories
{
  using System;
  using Sitecore;
  using Sitecore.Diagnostics;

  public static class DictionaryRepository
  {
    private static string DictionaryRoot
    {
      get
      {
        if (Context.Site == null)
        {
          return string.Empty;
        }

        var dictionaryPath = Context.Site.Properties["dictionaryPath"];
        if (!string.IsNullOrEmpty(dictionaryPath))
        {
          return dictionaryPath;
        }
        return string.Empty;
      }
    }

    public static string Get(string relativePath, string defaultValue)
    {
      if (Context.Database == null)
      {
        return defaultValue;
      }

      var pathToDictionaryKey = string.Concat(DictionaryRoot, relativePath);
      var dictionaryItem = Context.Database.GetItem(pathToDictionaryKey, Context.Language);

      if (dictionaryItem == null)
      {
        Log.Warn(string.Concat("Could not find the dictionary with they path ", pathToDictionaryKey), typeof(Exception));
        return defaultValue;
      }
      return dictionaryItem.Fields["Phrase"].Value ?? defaultValue;
    }
  }
}