using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sitecore.Foundation.MultiSite.Providers
{
  using Sitecore.Data.Items;

  public class ConfigurationDataSourceProvider : IDatasourceProvider
  {
    public Item[] GetSources(string name, Item contextItem)
    {
      return new Item[]
      {
      };
    }
  }
}