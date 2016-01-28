namespace Sitecore.Feature.Search.Tests
{
  using System.Web;
  using System.Web.Mvc;
  using FluentAssertions;
  using NSubstitute;
  using Ploeh.AutoFixture.AutoNSubstitute;
  using Ploeh.AutoFixture.Xunit2;
  using Sitecore.Feature.Search.Controllers;
  using Sitecore.Feature.Search.Models;
  using Sitecore.Feature.Search.Repositories;
  using Sitecore.Feature.Search.Tests.Extensions;
  using Sitecore.Foundation.Indexing;
  using Sitecore.Foundation.Indexing.Models;
  using Sitecore.Foundation.SitecoreExtensions.Repositories;
  using Sitecore.Foundation.Testing.Attributes;
  using Xunit;

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
    public void ShouldReturnGlobalSearch(ISearchResults searchResults, [Substitute] SearchService service, [Substitute] SearchSettings settings, ISearchServiceRepository serviceRepository, ISearchSettingsRepository settingsRepository, QueryRepository queryRepository, IRenderingPropertiesRepository renderingPropertiesRepository, string query)
    {
      service.Search(Arg.Any<IQuery>()).Returns(searchResults);
      serviceRepository.Get().Returns(service);
      settingsRepository.Get().Returns(settings);
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
