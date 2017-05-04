namespace Sitecore.Feature.Search.Models
{
    public class PagingSettings
    {
        private int pagesToShow;
        private int resultsOnPage;
        public const int DefaultResultsOnPage = 10;
        public const int DefaultPagesToShow = 5;

        public int PagesToShow
        {
            get
            {
                return this.pagesToShow < 1 ? DefaultPagesToShow : this.pagesToShow;
            }
            set
            {
                this.pagesToShow = value;
            }
        }

        public int ResultsOnPage
        {
            get
            {
                return this.resultsOnPage < 1 ? DefaultResultsOnPage : this.resultsOnPage;
            }
            set
            {
                this.resultsOnPage = value;
            }
        }
    }
}