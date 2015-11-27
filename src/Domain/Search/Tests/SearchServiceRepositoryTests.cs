namespace Habitat.Search.Tests
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using System.Threading.Tasks;
  using FluentAssertions;
  using Habitat.Accounts.Tests.Extensions;
  using Habitat.Framework.Indexing;
  using Habitat.Search.Repositories;
  using Xunit;

  public class SearchServiceRepositoryTests
  {
    [Theory]
    [AutoDbData]
    public void ShouldReturnSearchService(ISearchSettingsRepository settingsRepository)
    {
      var repository = new SearchServiceRepository(settingsRepository);
      var service = repository.Get();
      service.Should().BeOfType<SearchService>();
    }
  }
}
