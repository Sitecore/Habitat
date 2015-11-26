namespace Habitat.Search.Tests
{
  using System.Runtime.InteropServices.ComTypes;
  using System.Web.Mvc;
  using FluentAssertions;
  using Habitat.Accounts.Tests.Extensions;
  using Habitat.Framework.Indexing;
  using Habitat.Framework.Indexing.Models;
  using Habitat.Search.Controllers;
  using Habitat.Search.Models;
  using Habitat.Search.Repositories;
  using NSubstitute;
  using NSubstitute.Core.Arguments;
  using Ploeh.AutoFixture.Xunit2;
  using Xunit;
  using Ploeh.AutoFixture.AutoNSubstitute;

  public class SearchControllerTests
  {
    [Theory]
    [AutoDbData]
    public void ShouldReturnPagedSearchResult([Substitute] SearchService service, ISearchServiceRepository serviceRepository, ISearchSettingsRepository settingsRepository, QueryRepository queryRepository, string query, ISearchResults searchResults, [Substitute] SearchController searchController)
    {
      service.Search(Arg.Any<IQuery>()).Returns(searchResults);
      serviceRepository.Get().Returns(service);
      var controller = new SearchController(serviceRepository, settingsRepository, queryRepository);
      var result = controller.PagedSearchResults(query, null) as ViewResult;
      result.Model.Should().BeOfType<PagedSearchResults>();
    }
  }
}
