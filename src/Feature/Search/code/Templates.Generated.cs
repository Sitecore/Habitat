namespace Sitecore.Feature.Search
{
    using global::Sitecore.Data;
    
    public partial struct Templates
    {

        public partial struct PagedSearchResultsParameters
        {
            public static readonly ID ID = new ID("{D1D3E60F-E571-48D2-84CF-B053EE660C13}");

            public partial struct Fields
            {
                public static readonly ID PagesToShow = new ID("{D7DDE02F-B1F1-416D-91E0-7C3612EF4871}");
                public const string PagesToShow_FieldName = "PagesToShow";
                public static readonly ID ResultsOnPage = new ID("{FCC7E3B4-46AB-4A51-975F-A6B259B3D214}");
                public const string ResultsOnPage_FieldName = "ResultsOnPage";
            }
        }

        public partial struct SearchContext
        {
            public static readonly ID ID = new ID("{B524E8BE-A099-4A63-BE3F-DD4C42FD4185}");

            public partial struct Fields
            {
                public static readonly ID SearchResultsPage = new ID("{1C843E6A-02B9-4AA0-9FED-FDFDDC419AE3}");
                public const string SearchResultsPage_FieldName = "Search Results Page";
            }
        }

        public partial struct SearchResults
        {
            public static readonly ID ID = new ID("{14E452CA-064D-48A8-9FF2-2744D10437A1}");

            public partial struct Fields
            {
                public static readonly ID Facets = new ID("{8C4DAD9D-1EA2-4EB7-AE19-D4C28604757A}");
                public const string Facets_FieldName = "Facets";
                public static readonly ID Root = new ID("{CD904125-3AE5-4709-9E6D-71473C5D5007}");
                public const string Root_FieldName = "Root";
                public static readonly ID SearchBoxTitle = new ID("{80E30DD8-8021-45F5-9FE1-23D2702CC206}");
                public const string SearchBoxTitle_FieldName = "SearchBoxTitle";
            }
        }
    }
}