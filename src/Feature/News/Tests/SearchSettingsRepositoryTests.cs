namespace Sitecore.Feature.News.Tests
{
  using FluentAssertions;
  using Sitecore.Feature.News.Repositories;
  using Sitecore.Feature.News.Tests.Extensions;
  using Xunit;

  public class SearchSettingsRepositoryTests
  {
    [Theory]
    [AutoDbData]
    public void Get_ReturnsSearchSettings(SearchSettingsRepository settingsRepository)
    {
      var searchSettings = settingsRepository.Get();
      searchSettings.Templates.Should().Contain(Templates.NewsArticle.ID);
    }
  }
}
