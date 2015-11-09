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
        if (Context.Site == null)
        {
          return string.Empty;
        }

                var dictionaryPath = Context.Site.Properties["dictionaryPath"];
                if (!string.IsNullOrEmpty(dictionaryPath))
                    return dictionaryPath;
                return string.Empty;
            }
        }

        public static string Get(string relativePath, string defaultValue)
        {
      if (Context.Database == null)
      {
        return defaultValue;
      }

      string pathToDictionaryKey = string.Concat(DictionaryRoot, relativePath);
      Item dictionaryItem = Context.Database.GetItem(pathToDictionaryKey, Context.Language);

      if (dictionaryItem == null)
      {
        Log.Warn(string.Concat("Could not find the dictionary with they path ", pathToDictionaryKey), typeof(Exception));
                return defaultValue;
            }
    }
}