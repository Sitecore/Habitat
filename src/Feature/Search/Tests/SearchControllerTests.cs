namespace Sitecore.Feature.Search.Tests
{
  using System;
  using System.Web;
  using System.Web.Mvc;
  using FluentAssertions;
  using NSubstitute;
  using NSubstitute.Core;
  using Ploeh.AutoFixture.AutoNSubstitute;
  using Ploeh.AutoFixture.Xunit2;
  using Sitecore.Feature.Search.Controllers;
  using Sitecore.Feature.Search.Models;
  using Sitecore.Feature.Search.Repositories;
  using Sitecore.Foundation.Indexing;
  using Sitecore.Foundation.Indexing.Models;
  using Sitecore.Foundation.Indexing.Repositories;
  using Sitecore.Foundation.SitecoreExtensions.Repositories;
  using Sitecore.Foundation.Testing.Attributes;
  using Xunit;

  public class SearchControllerTests
  {
    [Theory]
    [AutoDbData]
    public void PagedSearchResults_ShouldReturnModel([Substitute] SearchService service, [Substitute] PagingSettings pagingSettings, [Substitute]SearchContext searchContext, [Frozen]ISearchContextRepository contextRepository, QueryRepository queryRepository, ISearchServiceRepository serviceRepository, string query, ISearchResults searchResults, IRenderingPropertiesRepository renderingPropertiesRepository)
    {
      renderingPropertiesRepository.Get<PagingSettings>().Returns(pagingSettings);
      service.Search(Arg.Any<IQuery>()).Returns(searchResults);
      serviceRepository.Get().Returns(service);
      contextRepository.Get().Returns(searchContext);
      var controller = new SearchController(serviceRepository, contextRepository, queryRepository, renderingPropertiesRepository);
      var result = controller.PagedSearchResults(query, null) as ViewResult;
      result.Model.Should().BeOfType<PagedSearchResults>();
    }

    [Theory]
    [AutoDbData]
    public void SearchResultsHeader_ShouldReturnModel(ISearchResults searchResults, [Substitute] SearchService service, SearchContext searchContext, ISearchServiceRepository serviceRepository, ISearchContextRepository contextRepository, QueryRepository queryRepository, IRenderingPropertiesRepository renderingPropertiesRepository, string query)
    {
      contextRepository.Get().Returns(searchContext);
      var controller = new SearchController(serviceRepository, contextRepository, queryRepository, renderingPropertiesRepository);
      var result = controller.SearchResultsHeader(query) as ViewResult;
      result.Model.Should().BeOfType<SearchContext>();
    }

    [Theory]
    [AutoDbData]
    public void SearchResults_ShouldReturnModel([Substitute] ControllerContext controllerContext, [Substitute] HttpContextBase context, ISearchResults searchResults, [Substitute] SearchService service, ISearchServiceRepository serviceRepository, ISearchContextRepository contextRepository, QueryRepository queryRepository, IRenderingPropertiesRepository renderingPropertiesRepository, string query)
    {
      service.Search(Arg.Any<IQuery>()).Returns(searchResults);
      serviceRepository.Get().Returns(service);
      var controller = new SearchController(serviceRepository, contextRepository, queryRepository, renderingPropertiesRepository)
                       {
                         ControllerContext = controllerContext
                       };
      controller.ControllerContext.HttpContext = context;
      var result = controller.SearchResults(query) as ViewResult;
      result.Model.Should().As<ISearchResults>();
    }

    [Theory]
    [AutoDbData]
    public void GlobalSearch_ShouldReturnModel(ISearchResults searchResults, [Substitute] SearchService service, [Substitute] SearchContext context, ISearchServiceRepository serviceRepository, ISearchContextRepository contextRepository, QueryRepository queryRepository, IRenderingPropertiesRepository renderingPropertiesRepository, string query)
    {
      service.Search(Arg.Any<IQuery>()).Returns(searchResults);
      serviceRepository.Get().Returns(service);
      contextRepository.Get().Returns(context);
      var controller = new SearchController(serviceRepository, contextRepository, queryRepository, renderingPropertiesRepository);
      var result = controller.GlobalSearch() as ViewResult;
      result.Model.Should().As<ISearchResults>();
    }

    [Theory]
    [AutoDbData]
    public void SearchResults_CanBeInitialized(SearchContext context, [NoAutoProperties] SearchResults results)
    {
    }
  }
}