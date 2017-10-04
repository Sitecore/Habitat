namespace Sitecore.Feature.Media
{
    using global::Sitecore.Data;
    
    public partial struct Templates
    {

        public partial struct HasMedia
        {
            public static readonly ID ID = new ID("{A44E450E-BA3F-4FAF-9C53-C63241CC34EB}");

            public partial struct Fields
            {
                public static readonly ID MediaDescription = new ID("{302C9F8D-F703-4F76-A4AB-73D222648232}");
                public const string MediaDescription_FieldName = "MediaDescription";
                public static readonly ID MediaThumbnail = new ID("{4FF62B0A-D73B-4436-BEA2-023154F2FFC4}");
                public const string MediaThumbnail_FieldName = "MediaThumbnail";
                public static readonly ID MediaTitle = new ID("{63DDA48B-B0CB-45A7-9A1B-B26DDB41009B}");
                public const string MediaTitle_FieldName = "MediaTitle";
                public static readonly ID MediaImage = new ID("{9F51DEAD-AD6E-41C2-9759-7BE17EB474A4}");
                public const string MediaImage_FieldName = "MediaImage";
                public static readonly ID MediaSelector = new ID("{72EA8682-24D2-4BEB-951C-3E2164974105}");
                public const string MediaSelector_FieldName = "MediaSelector";
                public static readonly ID MediaVideoLink = new ID("{2628705D-9434-4448-978C-C3BF166FA1EB}");
                public const string MediaVideoLink_FieldName = "MediaVideoLink";
            }
        }

        public partial struct HasMediaImage
        {
            public static readonly ID ID = new ID("{FAE0C913-1600-4EBA-95A9-4D6FD7407E25}");

            public partial struct Fields
            {
                public static readonly ID MediaDescription = new ID("{302C9F8D-F703-4F76-A4AB-73D222648232}");
                public const string MediaDescription_FieldName = "MediaDescription";
                public static readonly ID MediaThumbnail = new ID("{4FF62B0A-D73B-4436-BEA2-023154F2FFC4}");
                public const string MediaThumbnail_FieldName = "MediaThumbnail";
                public static readonly ID MediaTitle = new ID("{63DDA48B-B0CB-45A7-9A1B-B26DDB41009B}");
                public const string MediaTitle_FieldName = "MediaTitle";
                public static readonly ID MediaSelector = new ID("{72EA8682-24D2-4BEB-951C-3E2164974105}");
                public const string MediaSelector_FieldName = "MediaSelector";
                public static readonly ID MediaVideoLink = new ID("{2628705D-9434-4448-978C-C3BF166FA1EB}");
                public const string MediaVideoLink_FieldName = "MediaVideoLink";
                public static readonly ID MediaImage = new ID("{9F51DEAD-AD6E-41C2-9759-7BE17EB474A4}");
                public const string MediaImage_FieldName = "MediaImage";
            }
        }

        public partial struct HasMediaSelector
        {
            public static readonly ID ID = new ID("{AE4635AF-CFBF-4BF6-9B50-00BE23A910C0}");

            public partial struct Fields
            {
                public static readonly ID MediaSelector = new ID("{72EA8682-24D2-4BEB-951C-3E2164974105}");
                public const string MediaSelector_FieldName = "MediaSelector";
            }
        }

        public partial struct HasMediaVideo
        {
            public static readonly ID ID = new ID("{5A1B724B-B396-4C48-A833-655CD19018E1}");

            public partial struct Fields
            {
                public static readonly ID MediaDescription = new ID("{302C9F8D-F703-4F76-A4AB-73D222648232}");
                public const string MediaDescription_FieldName = "MediaDescription";
                public static readonly ID MediaThumbnail = new ID("{4FF62B0A-D73B-4436-BEA2-023154F2FFC4}");
                public const string MediaThumbnail_FieldName = "MediaThumbnail";
                public static readonly ID MediaTitle = new ID("{63DDA48B-B0CB-45A7-9A1B-B26DDB41009B}");
                public const string MediaTitle_FieldName = "MediaTitle";
                public static readonly ID MediaImage = new ID("{9F51DEAD-AD6E-41C2-9759-7BE17EB474A4}");
                public const string MediaImage_FieldName = "MediaImage";
                public static readonly ID MediaSelector = new ID("{72EA8682-24D2-4BEB-951C-3E2164974105}");
                public const string MediaSelector_FieldName = "MediaSelector";
                public static readonly ID MediaVideoLink = new ID("{2628705D-9434-4448-978C-C3BF166FA1EB}");
                public const string MediaVideoLink_FieldName = "MediaVideoLink";
            }
        }

        public partial struct MediaSiteExtension
        {
            public static readonly ID ID = new ID("{D339E56B-6A8A-46BD-A7D3-C9725D50DD4A}");

            public partial struct Fields
            {
                public static readonly ID Mediafolder = new ID("{E7A63BF6-5A06-498D-B6C1-C8F058ABE2B3}");
                public const string Mediafolder_FieldName = "Mediafolder";
            }
        }

        public partial struct BackgroundTypeFolder
        {
            public static readonly ID ID = new ID("{1FD75C49-F524-4C15-9F82-DCB2D4CF2FA9}");

            public partial struct Fields
            {
            }
        }

        public partial struct BackgroundType
        {
            public static readonly ID ID = new ID("{55A5BDAD-EB69-40F5-8195-CDA182E48EE4}");

            public partial struct Fields
            {
                public static readonly ID Class = new ID("{AF6B8E5C-10A2-46BE-8310-407434EC1055}");
                public const string Class_FieldName = "Class";
            }
        }

        public partial struct MediaParameters
        {
            public static readonly ID ID = new ID("{5DF30DC0-E2FC-4921-B8F2-C54FAC1BD03E}");

            public partial struct Fields
            {
            }
        }

        public partial struct ParametersTemplateSectionBackground
        {
            public static readonly ID ID = new ID("{B962A806-D708-4001-B0A3-3FA31F2263C5}");

            public partial struct Fields
            {
                public static readonly ID Media = new ID("{73D4BA11-BC5C-44DB-B184-8FD59857C8DB}");
                public const string Media_FieldName = "Media";
                public static readonly ID Parallax = new ID("{D598D6DD-3B61-47C3-B84B-8C73211FEF04}");
                public const string Parallax_FieldName = "Parallax";
                public static readonly ID Type = new ID("{F9588BB9-013E-4C21-B339-5ED379252CDE}");
                public const string Type_FieldName = "Type";
            }
        }
    }
}