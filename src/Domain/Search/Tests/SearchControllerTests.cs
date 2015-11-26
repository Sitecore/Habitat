namespace Habitat.Search.Tests
{
  using System.Collections.Generic;
  using System.Runtime.InteropServices.ComTypes;
  using System.Web;
  using System.Web.Mvc;
  using FluentAssertions;
  using Habitat.Accounts.Tests.Extensions;
  using Habitat.Framework.Indexing;
  using Habitat.Framework.Indexing.Models;
  using Habitat.Framework.SitecoreExtensions.Repositories;
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
    public void ShouldReturnPagedSearchResult([Substitute] SearchService service, ISearchServiceRepository serviceRepository, ISearchSettingsRepository settingsRepository, QueryRepository queryRepository, string query, ISearchResults searchResults, [Substitute] SearchController searchController, IRenderingPropertiesRepository renderingPropertiesRepository, [Substitute] PagingSettings pagingSettings)
    {
      renderingPropertiesRepository.Get<PagingSettings>().Returns(pagingSettings);
      service.Search(Arg.Any<IQuery>()).Returns(searchResults);
      serviceRepository.Get().Returns(service);
      var controller = new SearchController(serviceRepository, settingsRepository, queryRepository, renderingPropertiesRepository);
      var result = controller.PagedSearchResults(query, null) as ViewResult;
      result.Model.Should().BeOfType<PagedSearchResults>();
    }

    [Theory]
    [AutoDbData]
    public void ShouldReturnSearchSettings(ISearchResults searchResults,[Substitute] SearchService service, SearchSettings searchSettings, ISearchServiceRepository serviceRepository, ISearchSettingsRepository settingsRepository, QueryRepository queryRepository,   IRenderingPropertiesRepository renderingPropertiesRepository, string query)
    {
      settingsRepository.Get(Arg.Any<string>()).Returns(searchSettings);
      var controller = new SearchController(serviceRepository, settingsRepository, queryRepository, renderingPropertiesRepository);
      var result = controller.SearchSettings(query) as ViewResult;
      result.Model.Should().BeOfType<SearchSettings>();
    }

    [Theory]
    [AutoDbData]
    public void ShouldReturnSearchResults([Substitute]ControllerContext controllerContext,[Substitute] HttpContextBase context,ISearchResults searchResults, [Substitute] SearchService service, ISearchServiceRepository serviceRepository, ISearchSettingsRepository settingsRepository, QueryRepository queryRepository, IRenderingPropertiesRepository renderingPropertiesRepository, string query)
    {
      service.Search(Arg.Any<IQuery>()).Returns(searchResults);
      serviceRepository.Get().Returns(service);
      var controller = new SearchController(serviceRepository, settingsRepository, queryRepository, renderingPropertiesRepository);
      controller.ControllerContext = controllerContext;
      controller.ControllerContext.HttpContext = context;
      var result = controller.SearchResults(query) as ViewResult;
      result.Model.Should().As<ISearchResults>();
    }

    [Theory]
    [AutoDbData]
    public void ShouldReturnGlobalSearch(ISearchResults searchResults, [Substitute] SearchService service, ISearchServiceRepository serviceRepository, ISearchSettingsRepository settingsRepository, QueryRepository queryRepository, IRenderingPropertiesRepository renderingPropertiesRepository, string query)
    {
      service.Search(Arg.Any<IQuery>()).Returns(searchResults);
      serviceRepository.Get().Returns(service);
      var controller = new SearchController(serviceRepository, settingsRepository, queryRepository, renderingPropertiesRepository);
      var result = controller.GlobalSearch() as ViewResult;
      result.Model.Should().As<ISearchResults>();
    }

    [Theory]
    [AutoDbData]
    public void SearchResultModelCanBeInitialized(SearchSettings settings, [NoAutoProperties]SearchResults results)
    {
    }
  }
}
