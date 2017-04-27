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
                while (firstPage + this.VisiblePagesCount > this.TotalPagesCount - 1)
                {
                    firstPage--;
                }

                if (firstPage < 0)
                {
                    firstPage = 0;
                }

                return firstPage;
            }
        }

        public int LastPage
        {
            get
            {
                var lastPage = this.FirstPage + this.VisiblePagesCount;

                if (lastPage > this.TotalPagesCount - 1)
                {
                    lastPage = this.TotalPagesCount - 1;
                }

                return lastPage;
            }
        }
    }
}