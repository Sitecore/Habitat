using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Habitat.Search.Repositories
{
  using Habitat.Framework.Indexing;

  public interface ISearchServiceRepository
  {
    SearchService Get();
  }
}