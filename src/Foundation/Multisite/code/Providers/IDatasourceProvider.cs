using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.Foundation.MultiSite.Providers
{
  using Sitecore.Data.Items;

  public interface IDatasourceProvider
  {
    Item[] GetSources(string name, Item contextItem);
  }
}
