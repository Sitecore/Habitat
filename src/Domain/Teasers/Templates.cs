using Sitecore.Data;

namespace Habitat.Teasers
{
    public struct Templates
    {
        public struct Teaser
        {
            public static ID ID => new ID("{8D190C40-BC8F-4D1B-94BD-A201A45274A5}");
            public struct Fields
            {
                public static ID Title => new ID("{AE2FC28B-7A6B-41F4-8B21-7DEC7C5CB7FE}");
                public static ID Text => new ID("{475B6098-2AE5-4DDA-97D2-0217A8D4C454}");
                public static ID Image => new ID("{D5E80175-69E8-47CB-81B1-B7C8719706B2}");
                public static ID Link => new ID("{CC6C0053-F2F7-4D07-81ED-A53229674348}");
            }
        }
    }
}
