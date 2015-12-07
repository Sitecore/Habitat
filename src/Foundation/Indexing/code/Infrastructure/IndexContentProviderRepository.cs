#region using



#endregion

namespace Sitecore.Foundation.Indexing.Infrastructure
{
  using System.Collections.Generic;
  using Sitecore.Configuration;
  using Sitecore.Configuration.Providers;

  internal static class IndexContentProviderRepository
  {
    private static IEnumerable<IndexContentProviderBase> _all;
    private static IndexContentProviderBase _default;

    private static void Initialize()
    {
      IndexContentProviderBase defaultProvider;
      All = Factory.GetProviders<IndexContentProviderBase, ProviderCollectionBase<IndexContentProviderBase>>("solutionFramework/indexing", out defaultProvider);
      Default = defaultProvider;
    }

    public static IEnumerable<IndexContentProviderBase> All
    {
      get
      {
        if (_all == null)
        {
          Initialize();
        }
        return _all;
      }
      private set
      {
        _all = value;
      }
    }

    public static IndexContentProviderBase Default
    {
      get
      {
        if (_default == null)
        {
          Initialize();
        }
        return _default;
      }
      private set
      {
        _default = value;
      }
    }
  }
}