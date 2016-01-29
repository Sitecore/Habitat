namespace Sitecore.Foundation.Testing.Commands
{
  using System.Reflection;
  using Ploeh.AutoFixture.Kernel;
  using Sitecore.Configuration;
  using Sitecore.ContentSearch;

  public class ReplaceSearchAttributeCommand : ISpecimenCommand
  {
    private static void SetupSearchProvider(SearchProvider searchProvider)
    {
      var providerHelper = new ProviderHelper<SearchProvider, SearchProviderCollection>("randomString");
      typeof(ContentSearchManager).GetField("ProviderHelper", BindingFlags.Static | BindingFlags.NonPublic)?.SetValue(null, providerHelper);
      providerHelper.GetType().GetField("_provider", BindingFlags.Instance | BindingFlags.NonPublic)?.SetValue(providerHelper, searchProvider);
    }

    public void Execute(object specimen, ISpecimenContext context)
    {
      var provider = specimen as SearchProvider;
      if (provider == null)
      {
        return;
      }

      SetupSearchProvider(provider);
    }
  }
}