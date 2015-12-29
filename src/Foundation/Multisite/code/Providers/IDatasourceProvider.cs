using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.Foundation.MultiSite.Providers
{
  using Sitecore.Data;
  using Sitecore.Data.Items;
  using Sitecore.Syndication;

  public interface IDatasourceProvider
  {
    Item[] GetSources(string name, Item contextItem);

    Item GetSourceTemplate(string name, Item conteItem);

    Database Database { get; set; }
  }
}
