namespace Sitecore.Feature.News.Tests
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using System.Threading.Tasks;
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
      searchSettings.Tempaltes.Should().Contain(Templates.NewsArticle.ID);
    }
  }
}
