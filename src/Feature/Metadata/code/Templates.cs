namespace Sitecore.Feature.Metadata
{
    using Sitecore.Data;

    public static class Templates
    {
        public static class PageMetadata
        {
            public static ID ID = new ID("{D88CCD80-D851-470D-AF11-701FF23504E7}");

            public static class Fields
            {
                public static readonly ID BrowserTitle = new ID("{CA0479CE-0BFE-4522-83DE-BA688B380A78}");
                public static readonly ID Description = new ID("{BB7A38C0-323C-4F81-8EB9-288ABF7C4638}");
                public static readonly ID Keywords = new ID("{4B16F930-73C9-4643-BB1B-00F06E60A073}");
                public static readonly ID CanIndex = new ID("{683D7237-206A-488F-9DEE-4A4E41FB161D}");
                public static readonly ID CanFollow = new ID("{0DCA829C-9FCE-41F5-9990-C6182FEFE905}");
                public static readonly ID CustomMetadata = new ID("{167ABA77-5172-42AF-9F9E-00299113839E}");
            }
        }

        public static class SiteMetadata
        {
            public static readonly ID ID = new ID("{CF38E914-9298-47CC-9205-210553E79F97}");

            public static class Fields
            {
                public static readonly ID SiteBrowserTitle = new ID("{235AE392-97AC-4822-BE38-837DA3E7724E}");
            }
        }

        public static class Keyword
        {
            public static ID ID = new ID("{409F883A-0DC8-431A-9508-7316B59B92BE}");

            public static class Fields
            {
                public static readonly ID Keyword = new ID("{7BDBBA5F-C7E6-45C2-82F5-010DED013588}");
            }
        }
    }
}