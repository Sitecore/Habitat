using Sitecore.Data;

namespace Habitat.Navigation
{
    public struct Templates
    {
        public struct NavigationRoot
        {
            public static ID ID => new ID("{F9F4FC05-98D0-4C62-860F-F08AE7F0EE25}");

            public struct Fields
            {
                public static ID IncludeRootInPrimaryMenu => new ID("{B7362C78-C726-45A7-8675-C115B3338A92}");
            }
        }

        public struct Navigable
        {
            public static ID ID => new ID("{A1CBA309-D22B-46D5-80F8-2972C185363F}");

            public struct Fields
            {
                public static ID ShowInNavigation => new ID("{5585A30D-B115-4753-93CE-422C3455DEB2}");
                public static ID NavigationTitle => new ID("{1B483E91-D8C4-4D19-BA03-462074B55936}");
            }
        }
    }
}