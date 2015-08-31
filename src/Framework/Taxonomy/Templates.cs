using Sitecore.Data;

namespace Habitat.Framework.Taxonomy
{
    public struct Templates
    {
        public struct Taggable
        {
            public static ID ID = new ID("{76E517CE-D593-4EF7-BB9B-E52EB4E230C8}");

            public struct Fields
            {
                public static ID Tags = new ID("{F5868B5D-CE4D-4CAD-93DC-1C4E973C2FA8}");
            }
        }

        public struct Tag
        {
            public static ID ID = new ID("{A1E9A1FE-E1D0-45A6-BEC5-7954E1F56FAA}");
        }
    }
}