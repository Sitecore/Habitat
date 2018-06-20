using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sitecore.Feature.News.Tests
{
  using FluentAssertions;
  using Sitecore.Data.Items;
  using Sitecore.Feature.News.Tests.Extensions;
  using Sitecore.Foundation.Indexing.Models;
  using Xunit;

  public class SearchSettingsTests
  {
    [Theory]
    [AutoDbData]
    public void Root_SetRootSomeItem_RootShouldReturnsSameItem(SearchSettingsBase settings, Item item)
    {
      settings.Root = item;
      settings.Root.Should().BeEquivalentTo(item);
    }
  }
}
