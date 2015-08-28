using Sitecore;
using Sitecore.Configuration;
using Sitecore.Data;

namespace Habitat.Framework.SitecoreExtensions.Repositories
{
  internal static class DatabaseRepository
  {
    public static Database GetActiveDatabase()
    {
      var database = Context.ContentDatabase ?? Context.Database;
      if (database != null && database.Name != "core")
        return database;
      return Factory.GetDatabase("master");
    }
  }
}