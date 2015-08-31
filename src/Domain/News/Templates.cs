using Sitecore.Data;

namespace Habitat.News
{
    public struct Templates
    {
        public struct NewsArticle
        {
            public static ID ID = new ID("{2F75C8AF-35FC-4A88-B585-7595203F442C}");

            public struct Fields
            {
                public static readonly ID Date = new ID("{C464D2D7-3382-428A-BCDF-0963C60BA0E3}");
            }
        }

        public struct NewsFolder
        {
            public static readonly ID ID = new ID("{74889B26-061C-4D6A-8CDB-422665FC34EC}");
        }
    }
}