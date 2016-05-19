namespace Sitecore.Foundation.Indexing.Tests
{
  using FluentAssertions;
  using Ploeh.AutoFixture.Xunit2;
  using Sitecore.Foundation.Indexing.Repositories;
  using Sitecore.Foundation.Testing.Attributes;
  using Xunit;

  public class SearchServiceRepositoryTests
  {
    [Theory]
    [AutoDbData]
    public void Get_ReturnsSearchService([Frozen] ISearchSettingsRepository settingRepository, SearchServiceRepository serviceRepository)
    {
      var result = serviceRepository.Get();
      result.Should().BeOfType<SearchService>();
    }
  }
}
