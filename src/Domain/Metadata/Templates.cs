using Sitecore.Data;

namespace Habitat.Metadata
{
    public struct Templates
    {
        public struct PageMetadata
        {
            public static ID ID = new ID("{D88CCD80-D851-470D-AF11-701FF23504E7}");

            public struct Fields
            {
                public static readonly ID BrowserTitle = new ID("{CA0479CE-0BFE-4522-83DE-BA688B380A78}");
                public static readonly ID Description = new ID("{BB7A38C0-323C-4F81-8EB9-288ABF7C4638}");
                public static readonly ID Keywords = new ID("{4B16F930-73C9-4643-BB1B-00F06E60A073}");
                public static readonly ID CanIndex = new ID("{683D7237-206A-488F-9DEE-4A4E41FB161D}");
                public static readonly ID CustomMetadata = new ID("{167ABA77-5172-42AF-9F9E-00299113839E}");
            }
        }

        public struct SiteMetadata
        {
            public static readonly ID ID = new ID("{CF38E914-9298-47CC-9205-210553E79F97}");

            public struct Fields
            {
                public static readonly ID SiteBrowserTitle = new ID("{235AE392-97AC-4822-BE38-837DA3E7724E}");
            }
        }
    }
}