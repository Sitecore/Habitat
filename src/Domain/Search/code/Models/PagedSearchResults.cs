namespace Sitecore.Feature.Search.Models
{
  using Sitecore.Foundation.Indexing.Models;

  public class PagedSearchResults : IPageble
  {
    public const int DefaultResultsOnPage = 4;

    
    private int resultsOnPage;
    private int visiblePagesCount;

    public PagedSearchResults(int page, int totalResults, int pagesToShow, int resultsOnPage)
    {
      this.Page = page;
      this.visiblePagesCount = pagesToShow;
      this.TotalResults = totalResults;
      this.ResultsOnPage = resultsOnPage;
    }

    public virtual int TotalPagesCount
    {
      get
      {
        var pageCount = this.TotalResults / this.ResultsOnPage;
        if (this.TotalResults % this.ResultsOnPage > 0)
        {
          pageCount++;
        }

        return pageCount;
      }
    }

    public int FirstPage
    {
      get
      {
        var firstPage = this.Page - (visiblePagesCount / 2);
        

        if (this.TotalPagesCount - this.Page < this.visiblePagesCount / 2)
        {
          firstPage -= ((this.visiblePagesCount / 2) - this.TotalPagesCount + this.Page - 1 + (this.VisiblePagesCount % 2));
        }

        if (firstPage < 1)
        {
          firstPage = 1;
        }

        return firstPage;
      }
    }

    public int VisiblePagesCount
    {
      get
      {
        return this.visiblePagesCount;
      }
    }


    public int LastPage
    {
      get
      {
        var lastPage = this.Page + (visiblePagesCount / 2) - 1 + (this.VisiblePagesCount % 2);

        if ( this.Page - (this.VisiblePagesCount / 2) < 1)
        {
          lastPage += (1 + (this.VisiblePagesCount / 2) - this.Page);
        }

        if (lastPage > this.TotalPagesCount)
        {
          lastPage = this.TotalPagesCount;
        }

        return lastPage;
      }
    }

    public ISearchResults Results { get; set; }

    public string Query { get; set; }

    public int Page { get; set; }

    public int TotalResults { get; set; }

    public int ResultsOnPage
    {
      get
      {
        if (resultsOnPage == 0)
        {
          resultsOnPage = DefaultResultsOnPage;
        } 

        return resultsOnPage;
      }
      set
      {
        this.resultsOnPage = value;
      }
    }
  }
}