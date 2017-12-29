namespace Sitecore.Feature.Metadata
{
    using global::Sitecore.Data;
    
    public partial struct Templates
    {

        public partial struct Keyword
        {
            public static readonly ID ID = new ID("{409F883A-0DC8-431A-9508-7316B59B92BE}");

            public partial struct Fields
            {
                public static readonly ID Keyword = new ID("{7BDBBA5F-C7E6-45C2-82F5-010DED013588}");
                public const string Keyword_FieldName = "Keyword";
            }
        }

        public partial struct PageMetadata
        {
            public static readonly ID ID = new ID("{D88CCD80-D851-470D-AF11-701FF23504E7}");

            public partial struct Fields
            {
                public static readonly ID BrowserTitle = new ID("{CA0479CE-0BFE-4522-83DE-BA688B380A78}");
                public const string BrowserTitle_FieldName = "BrowserTitle";
                public static readonly ID CanIndex = new ID("{683D7237-206A-488F-9DEE-4A4E41FB161D}");
                public const string CanIndex_FieldName = "CanIndex";
                public static readonly ID CustomMetaData = new ID("{167ABA77-5172-42AF-9F9E-00299113839E}");
                public const string CustomMetaData_FieldName = "CustomMetaData";
                public static readonly ID MetaDescription = new ID("{BB7A38C0-323C-4F81-8EB9-288ABF7C4638}");
                public const string MetaDescription_FieldName = "MetaDescription";
                public static readonly ID MetaKeywords = new ID("{4B16F930-73C9-4643-BB1B-00F06E60A073}");
                public const string MetaKeywords_FieldName = "MetaKeywords";
                public static readonly ID SeoFollowLinks = new ID("{0DCA829C-9FCE-41F5-9990-C6182FEFE905}");
                public const string SeoFollowLinks_FieldName = "SeoFollowLinks";
            }
        }

        public partial struct SiteMetadata
        {
            public static readonly ID ID = new ID("{CF38E914-9298-47CC-9205-210553E79F97}");

            public partial struct Fields
            {
                public static readonly ID SiteBrowserTitle = new ID("{235AE392-97AC-4822-BE38-837DA3E7724E}");
                public const string SiteBrowserTitle_FieldName = "SiteBrowserTitle";
            }
        }
    }
}