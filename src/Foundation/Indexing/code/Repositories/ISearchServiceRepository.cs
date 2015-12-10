using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.Feature.News.Repositories
{
  using Sitecore.Foundation.Indexing;

  public interface ISearchServiceRepository
  {
    SearchService Get();
  }
}
