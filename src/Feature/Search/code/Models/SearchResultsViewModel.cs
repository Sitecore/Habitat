namespace Sitecore.Feature.Search.Models
{
    using System;
    using Sitecore.Foundation.Indexing.Models;

    public class SearchResultsViewModel
    {
        public ISearchResults Results { get; set; }

        public string Query { get; set; }

        public string Facets { get; set; }
        public int Page { get; set; }

        public int TotalResults { get; set; }

        public int ResultsOnPage { get; set; }

        public int VisiblePagesCount { get; set; }

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
    }
}