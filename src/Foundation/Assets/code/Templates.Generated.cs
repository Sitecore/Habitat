namespace Sitecore.Foundation.Assets
{
    using global::Sitecore.Data;
    
    public partial struct Templates
    {

        public partial struct HasTheme
        {
            public static readonly ID ID = new ID("{5B6F8720-3A93-4DA1-92A0-C3E85E01219A}");

            public partial struct Fields
            {
                public static readonly ID Theme = new ID("{53B5AF0A-265F-4E60-B2B2-4576CE0BECCF}");
                public const string Theme_FieldName = "Theme";
            }
        }

        public partial struct PageAssets
        {
            public static readonly ID ID = new ID("{91962B60-25F6-428F-8D10-02AA1E49D6A5}");

            public partial struct Fields
            {
                public static readonly ID CssCode = new ID("{06A96EFC-F2E5-45C3-A7DC-4DDDFA366CC0}");
                public const string CssCode_FieldName = "CssCode";
                public static readonly ID InheritAssets = new ID("{F19E8A50-9950-4861-9E66-9598A1898E71}");
                public const string InheritAssets_FieldName = "InheritAssets";
                public static readonly ID JavascriptCodeBottom = new ID("{B3BA9EA9-D0A1-49DF-9F4B-28FA5D353DC8}");
                public const string JavascriptCodeBottom_FieldName = "JavascriptCodeBottom";
                public static readonly ID JavascriptCodeTop = new ID("{D79D9DDD-2774-42F4-94C3-50C892F6E13D}");
                public const string JavascriptCodeTop_FieldName = "JavascriptCodeTop";
            }
        }

        public partial struct RenderingAssets
        {
            public static readonly ID ID = new ID("{7CEAC341-B953-4C69-B907-EE44302BF6AE}");

            public partial struct Fields
            {
                public static readonly ID CssAssets = new ID("{4867D192-326A-4AA4-81EF-EA430E224AFF}");
                public const string CssAssets_FieldName = "Css assets";
                public static readonly ID CssInline = new ID("{FD0DEC96-B220-4196-B544-68B11EEE727A}");
                public const string CssInline_FieldName = "Css inline";
                public static readonly ID JavaScriptAssets = new ID("{E514A1EB-DDBA-44F7-8528-82CA2280F778}");
                public const string JavaScriptAssets_FieldName = "JavaScript assets";
                public static readonly ID JavaScriptInline = new ID("{11421560-0BCB-403A-B099-8595C34341FD}");
                public const string JavaScriptInline_FieldName = "JavaScript inline";
            }
        }

        public partial struct ThemeFolder
        {
            public static readonly ID ID = new ID("{10059264-CD0B-47B9-8350-A31A90815921}");

            public partial struct Fields
            {
            }
        }

        public partial struct Theme
        {
            public static readonly ID ID = new ID("{1C87EA50-CC18-48CC-86DE-592E274D0C4F}");

            public partial struct Fields
            {
                public static readonly ID CssAssets = new ID("{4867D192-326A-4AA4-81EF-EA430E224AFF}");
                public const string CssAssets_FieldName = "Css assets";
                public static readonly ID CssInline = new ID("{FD0DEC96-B220-4196-B544-68B11EEE727A}");
                public const string CssInline_FieldName = "Css inline";
                public static readonly ID JavaScriptAssets = new ID("{E514A1EB-DDBA-44F7-8528-82CA2280F778}");
                public const string JavaScriptAssets_FieldName = "JavaScript assets";
                public static readonly ID JavaScriptInline = new ID("{11421560-0BCB-403A-B099-8595C34341FD}");
                public const string JavaScriptInline_FieldName = "JavaScript inline";
            }
        }
    }
}