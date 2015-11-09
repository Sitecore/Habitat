using System;
using Sitecore;
using Sitecore.Diagnostics;

namespace Habitat.Framework.SitecoreExtensions.Repositories
{
  public static class DictionaryRepository
  {
    private static string DictionaryRoot
    {
      get
      {
        Assert.IsNotNull(Context.Site, "Site");

        var dictionaryPath = Context.Site.Properties["dictionaryPath"];
        if (!string.IsNullOrEmpty(dictionaryPath))
          return dictionaryPath;
        return string.Empty;
      }
    }

    public static string Get(string relativePath, string defaultValue)
    {
      var pathToDictionaryKey = string.Concat(DictionaryRoot, relativePath);
      var dictionaryItem = Context.Database.GetItem(pathToDictionaryKey, Context.Language);

      if (dictionaryItem != null)
        return dictionaryItem.Fields["Phrase"].Value ?? defaultValue;

      Log.Warn(string.Concat("Could not find the dictionary with they path ", pathToDictionaryKey), typeof (Exception));
      return defaultValue;
    }
  }
}