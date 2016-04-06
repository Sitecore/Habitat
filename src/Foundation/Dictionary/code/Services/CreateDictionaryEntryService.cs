using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sitecore.Foundation.Dictionary.Services
{
  using System.Globalization;
  using System.Linq.Expressions;
  using System.Threading;
  using Sitecore.Data;
  using Sitecore.Data.Items;
  using Sitecore.Foundation.Dictionary.Models;
  using Sitecore.SecurityModel;

  internal static class CreateDictionaryEntryService
  {
    public static Item CreateDictionaryEntry(Dictionary dictionary, string relativePath, string defaultValue)
    {
      lock (dictionary)
      {
        var parts = relativePath.Split(new[] { '/', '\\' }, StringSplitOptions.RemoveEmptyEntries).ToArray();
        var root = dictionary.Root;
        for (var i = 0; i < parts.Length - 1; i++)
        {
          root = CreateDictionaryFolder(parts[i], root);
        }
        return CreateDictionaryEntry(parts.Last(), root, defaultValue);
      }
    }

    private static Item CreateDictionaryEntry(string name, Item root, string defaultValue)
    {
      using (new SecurityDisabler())
      {
        var item = GetOrCreateDictionaryItem(name, root, Templates.DictionaryEntry.ID);
        using (new EditContext(item))
        {
          item[Templates.DictionaryEntry.Fields.Phrase] = defaultValue;
        }
        return item;
      }
    }

    private static Item CreateDictionaryFolder(string name, Item parent)
    {
      using (new SecurityDisabler())
      {
        return GetOrCreateDictionaryItem(name, parent, Templates.DictionaryFolder.ID);
      }
    }

    private static Item GetOrCreateDictionaryItem(string name, Item parent, ID templateID)
    {
      var item = parent.Axes.GetChild(name);
      if (item != null)
        return item;

      try
      {
        return parent.Add(name, new TemplateID(templateID));
      }
      catch (Exception ex)
      {
        throw new InvalidOperationException($"Could not create item {name} under {parent.Name}", ex);
      }
    }
  }
}