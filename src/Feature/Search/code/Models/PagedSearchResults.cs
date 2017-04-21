namespace Sitecore.Feature.Search.Models
{
    using System;
    using Sitecore.Foundation.Indexing.Models;

    public class PagedSearchResults : IPageable
    {
        public const int DefaultResultsOnPage = 10;
        public const int DefaultPagesToShow = 5;

        private int resultsOnPage;

        public PagedSearchResults(SearchQuery query, int totalResults, int pagesToShow, int resultsOnPage)
        {
            var pageIndex = (double)query.IndexOfFirstResult / query.NoOfResults;
            this.Page = Convert.ToInt32(Math.Truncate(pageIndex)) + 1;
            this.VisiblePagesCount = pagesToShow <= 1 ? DefaultPagesToShow : pagesToShow;
            this.TotalResults = totalResults;
            this.ResultsOnPage = resultsOnPage <= 1 ? DefaultResultsOnPage : resultsOnPage;
        }

        public int VisiblePagesCount { get; }

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
                var firstPage = this.Page - this.VisiblePagesCount / 2;

                if (this.TotalPagesCount - this.Page < this.VisiblePagesCount / 2)
                {
                    firstPage -= this.VisiblePagesCount / 2 - this.TotalPagesCount + this.Page - 1 + this.VisiblePagesCount % 2;
                }

                if (firstPage < 1)
                {
                    firstPage = 1;
                }

                return firstPage;
            }
        }

        public int LastPage
        {
            get
            {
                var lastPage = this.Page + this.VisiblePagesCount / 2 - 1 + this.VisiblePagesCount % 2;

                if (this.Page - this.VisiblePagesCount / 2 < 1)
                {
                    lastPage += 1 + this.VisiblePagesCount / 2 - this.Page;
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
                if (this.resultsOnPage == 0)
                {
                    this.resultsOnPage = DefaultResultsOnPage;
                }

                return this.resultsOnPage;
            }
            set
            {
                this.resultsOnPage = value;
            }
        }
    }
}