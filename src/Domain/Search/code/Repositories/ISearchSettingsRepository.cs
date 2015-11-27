using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Habitat.Search.Repositories
{
  using Habitat.Search.Models;

  public interface ISearchSettingsRepository
  {
    SearchSettings Get();
    SearchSettings Get(string query);

  }
}