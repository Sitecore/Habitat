namespace Sitecore.Foundation.Assets
{
    using Sitecore.Data;

    public struct Templates
    {
        public struct RenderingAssets
        {
            public static readonly ID ID = new ID("{7CEAC341-B953-4C69-B907-EE44302BF6AE}");

            public struct Fields
            {
                public static readonly ID ScriptFiles = new ID("{E514A1EB-DDBA-44F7-8528-82CA2280F778}");
                public static readonly ID StylingFiles = new ID("{4867D192-326A-4AA4-81EF-EA430E224AFF}");
                public static readonly ID InlineScript = new ID("{11421560-0BCB-403A-B099-8595C34341FD}");
                public static readonly ID InlineStyling = new ID("{FD0DEC96-B220-4196-B544-68B11EEE727A}");
            }
        }

        public struct PageAssets
        {
            public static readonly ID ID = new ID("{91962B60-25F6-428F-8D10-02AA1E49D6A5}");

            public struct Fields
            {
                public static readonly ID JavascriptCodeTop = new ID("{D79D9DDD-2774-42F4-94C3-50C892F6E13D}");
                public static readonly ID JavascriptCodeBottom = new ID("{B3BA9EA9-D0A1-49DF-9F4B-28FA5D353DC8}");
                public static readonly ID CssCode = new ID("{06A96EFC-F2E5-45C3-A7DC-4DDDFA366CC0}");
                public static readonly ID InheritAssets = new ID("{F19E8A50-9950-4861-9E66-9598A1898E71}");
            }
        }

        public struct HasTheme
        {
            public static readonly ID ID = new ID("{5B6F8720-3A93-4DA1-92A0-C3E85E01219A}");

            public struct Fields
            {
                public static readonly ID Theme = new ID("{53B5AF0A-265F-4E60-B2B2-4576CE0BECCF}");
            }
        }
    }
}