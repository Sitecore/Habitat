namespace Sitecore.Feature.News.Tests
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using System.Threading.Tasks;
  using FluentAssertions;
  using Ploeh.AutoFixture.Xunit2;
  using Sitecore.Feature.News.Repositories;
  using Sitecore.Feature.News.Tests.Extensions;
  using Sitecore.Foundation.Indexing;
  using Xunit;

  public class SearchServiceRepositoryTests
  {
    [Theory]
    [AutoDbData]
    public void Should_Return_SearchService([Frozen] ISearchSettingsRepository settingRepository, SearchServiceRepository serviceRepository)
    {
      var result = serviceRepository.Get();
      result.Should().BeOfType<SearchService>();
    }
  }
}
