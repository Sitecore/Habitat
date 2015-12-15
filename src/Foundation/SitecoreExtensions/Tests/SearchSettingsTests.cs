namespace Sitecore.Foundation.SitecoreExtensions.Tests
{
  using FluentAssertions;
  using Sitecore.Data.Items;
  using Sitecore.Foundation.Indexing.Models;
  using Sitecore.Foundation.SitecoreExtensions.Tests.Common;
  using Xunit;

  public class SearchSettingsBaseTests
  {
    [Theory]
    [AutoDbData]
    public void Root_SetRootSomeItem_RootShouldReturnsSameItem(SearchSettingsBase settings, Item item)
    {
      settings.Root = item;
      settings.Root.ShouldBeEquivalentTo(item);
    }
  }
}
