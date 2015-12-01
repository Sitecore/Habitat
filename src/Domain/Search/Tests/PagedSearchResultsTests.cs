namespace Habitat.Search.Tests
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using System.Threading.Tasks;
  using FluentAssertions;
  using Habitat.Accounts.Tests.Extensions;
  using Habitat.Search.Models;
  using Ploeh.AutoFixture.Xunit2;
  using Xunit;

  public class PagedSearchResultsTests
  {
    [Theory]
    [AutoDbData]
    public void ShouldCalculateTotalPages(int page, int totalResults, int pagesToShow, int resultsOnPage)
    {
      var searchResults = new PagedSearchResults(page, totalResults, pagesToShow, resultsOnPage);
      var totalPages = Math.Ceiling(totalResults / (double)resultsOnPage);
      searchResults.TotalPagesCount.ShouldBeEquivalentTo(totalPages);
    }

    [Theory]
    [AutoDbData]
    public void FirstPageShouldBeLessThanLast(int page, int totalResults, int pagesToShow, int resultsOnPage)
    {
      var searchResults = new PagedSearchResults(page, totalResults, pagesToShow, resultsOnPage);
      searchResults.FirstPage.Should().BeLessOrEqualTo(searchResults.LastPage);
    }

    [Theory]
    [AutoDbData]
    public void FirstPageShouldBeGreaterThanZero(int page, int totalResults, int pagesToShow, int resultsOnPage)
    {
      var searchResults = new PagedSearchResults(page, totalResults, pagesToShow, resultsOnPage);
      searchResults.FirstPage.Should().BeGreaterThan(0);
    }

    [Theory]
    [AutoDbData]
    public void LastPageShouldNotBeGreaterThanTotalPages(int page, int totalResults, int pagesToShow, int resultsOnPage)
    {
      var searchResults = new PagedSearchResults(page, totalResults, pagesToShow, resultsOnPage);
      searchResults.FirstPage.Should().BeLessOrEqualTo(searchResults.TotalPagesCount);
    }

    [Theory]
    [AutoDbData]
    public void LastPageEdgeCase(int page, int totalResults, int pagesToShow, int resultsOnPage)
    {
      page = 1;
      var searchResults = new PagedSearchResults(page, totalResults, pagesToShow, resultsOnPage);
      searchResults.LastPage.Should().BeOneOf(pagesToShow, searchResults.TotalPagesCount);
    }
  }
}
