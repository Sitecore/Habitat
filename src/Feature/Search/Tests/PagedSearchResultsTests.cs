namespace Sitecore.Feature.Search.Tests
{
  using System;
  using FluentAssertions;
  using Sitecore.Feature.Search.Models;
  using Sitecore.Foundation.Testing.Attributes;
  using Xunit;

  public class PagedSearchResultsTests
  {
    [Theory]
    [AutoDbData]
    public void TotalPagesCount_ShouldCalculate(int page, int totalResults, int pagesToShow, int resultsOnPage)
    {
      var searchResults = new PagedSearchResults(page, totalResults, pagesToShow, resultsOnPage);
      var totalPages = Math.Ceiling(totalResults / (double)resultsOnPage);
      searchResults.TotalPagesCount.ShouldBeEquivalentTo(totalPages);
    }

    [Theory]
    [AutoDbData]
    public void FirstPage_ShouldBeLessThanLast(int page, int totalResults, int pagesToShow, int resultsOnPage)
    {
      var searchResults = new PagedSearchResults(page, totalResults, pagesToShow, resultsOnPage);
      searchResults.FirstPage.Should().BeLessOrEqualTo(searchResults.LastPage);
    }

    [Theory]
    [AutoDbData]
    public void FirstPage_ShouldBeGreaterThanZero(int page, int totalResults, int pagesToShow, int resultsOnPage)
    {
      var searchResults = new PagedSearchResults(page, totalResults, pagesToShow, resultsOnPage);
      searchResults.FirstPage.Should().BeGreaterThan(0);
    }

    [Theory]
    [AutoDbData]
    public void LastPage_ShouldNotBeGreaterThanTotalPages(int page, int totalResults, int pagesToShow, int resultsOnPage)
    {
      var searchResults = new PagedSearchResults(page, totalResults, pagesToShow, resultsOnPage);
      searchResults.FirstPage.Should().BeLessOrEqualTo(searchResults.TotalPagesCount);
    }

    [Theory]
    [AutoDbData]
    public void LastPage_EdgeCase(int page, int totalResults, int pagesToShow, int resultsOnPage)
    {
      page = 1;
      var searchResults = new PagedSearchResults(page, totalResults, pagesToShow, resultsOnPage);
      searchResults.LastPage.Should().BeOneOf(pagesToShow, searchResults.TotalPagesCount);
    }
  }
}