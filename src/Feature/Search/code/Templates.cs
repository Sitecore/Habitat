namespace Sitecore.Feature.Search
{
    using Sitecore.Data;

    public static class Templates
    {
        public static class SearchResults
        {
            public static readonly ID ID = new ID("{14E452CA-064D-48A8-9FF2-2744D10437A1}");

            public static class Fields
            {
                public static readonly ID SearchBoxTitle = new ID("{80E30DD8-8021-45F5-9FE1-23D2702CC206}");
                public static readonly ID Root = new ID("{CD904125-3AE5-4709-9E6D-71473C5D5007}");
                public static readonly ID Facets = new ID("{8C4DAD9D-1EA2-4EB7-AE19-D4C28604757A}");
            }
        }

        public static class SearchContext
        {
            public static readonly ID ID = new ID("{B524E8BE-A099-4A63-BE3F-DD4C42FD4185}");

            public static class Fields
            {
                public static readonly ID SearchResultsPage = new ID("{1C843E6A-02B9-4AA0-9FED-FDFDDC419AE3}");
            }
        }
    }
}