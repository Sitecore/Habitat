namespace Sitecore.Feature.Sitemap
{
    using Sitecore.Data;

    public struct Templates
    {
        public struct SitemapXMLSettings
        {
            public static readonly ID ID = new ID("{152EC1AE-4486-4519-A227-0E59BC812810}");

            public struct Fields
            {
                public static readonly ID GenerateSitempXML = new ID("{A816BDC8-D5AF-4845-911B-AE876BBDF10A}");
                public static readonly ID GenerateSitempXMLOnPublishing = new ID("{4A26D031-8500-4DDB-B965-516FD394F959}");
                public static readonly ID GenerateSitemapXMLByScheduler = new ID("{E260D3EA-5DD2-4D6E-AF48-D01A2091DCAC}");
                public static readonly ID IsProduction = new ID("{DB120AB0-733C-4B13-8C3F-9DDEFCE1EFF2}");
            }
        }
        public struct Sitemap
        {
            public static readonly ID ID = new ID("{8DCF7961-9470-4707-9FE0-3B1A372255D0}");

            public struct Fields
            {
                public static readonly ID IncludeInSitemap = new ID("{B8EFA7EA-7744-46B1-9C25-FF92FA83128B}");
            }
        }

        public struct SitemapSelector
        {
            public static readonly ID ID = new ID("{7381526C-FA87-4292-B617-1C8E42527364}");

            public struct Fields
            {
                public static readonly ID SearchEngine = new ID("{FC42E276-D0BC-4888-B645-CA141949759D}");
                public static readonly ID Sites = new ID("{3634EAD8-A127-4535-A196-DB1440E9E213}");
            }
        }

        public struct SitemapSearchEngine
        {
            public static readonly ID ID = new ID("{7034C3F4-C66E-4C71-B9CB-A33D7FF5BFDD}");

            public struct Fields
            {
                public static readonly ID HttpRequestString = new ID("{D6EDC5AA-5615-4A99-B90A-6AAADBC83CA1}");
            }
        }

        public struct Sites
        {
            public static readonly ID ID = new ID("{EB76FCDA-DE99-44AF-90F7-8207D6786611}");

            public struct Fields
            {
                public static readonly ID SiteName = new ID("{54A8E7A2-BBB9-4D99-A665-FF075447249E}");
                public static readonly ID ServerURL = new ID("{5F7F5D28-9183-4AC6-8FB6-31242D94D67B}");
                public static readonly ID FileName = new ID("{13CDD377-9B9E-4BC6-88FE-4C37AF98B9C0}");
            }
        }

        public struct Navigable
        {
            public static readonly ID ID = new ID("{A1CBA309-D22B-46D5-80F8-2972C185363F}");

            public struct Fields
            {
                public static readonly ID NavigationTitle = new ID("{1B483E91-D8C4-4D19-BA03-462074B55936}");
            }
        }
    }
}