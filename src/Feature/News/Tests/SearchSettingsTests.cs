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
  using Xunit;

  public class SearchSettingsTests
  {
    [Theory]
    [AutoDbData]
    public void Should_SetAndRead_RootField(SearchSettings settings, Item item)
    {
      settings.Root = item;
      settings.Root.ShouldBeEquivalentTo(item);
    }
  }
}
