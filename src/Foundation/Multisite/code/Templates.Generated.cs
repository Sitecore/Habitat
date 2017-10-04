namespace Sitecore.Foundation.Multisite
{
    using global::Sitecore.Data;
    
    public partial struct Templates
    {

        public partial struct Site
        {
            public static readonly ID ID = new ID("{BB85C5C2-9F87-48CE-8012-AF67CF4F765D}");

            public partial struct Fields
            {
            }
        }

        public partial struct SiteSettings
        {
            public static readonly ID ID = new ID("{BCCFEBEA-DCCB-48FE-9570-6503829EC03F}");

            public partial struct Fields
            {
            }
        }

        public partial struct DatasourceConfiguration
        {
            public static readonly ID ID = new ID("{C82DC5FF-09EF-4403-96D3-3CAF377B8C5B}");

            public partial struct Fields
            {
                public static readonly ID DatasourceLocation = new ID("{5FE1CC43-F86C-459C-A379-CD75950D85AF}");
                public const string DatasourceLocation_FieldName = "DatasourceLocation";
                public static readonly ID DatasourceTemplate = new ID("{498DD5B6-7DAE-44A7-9213-1D32596AD14F}");
                public const string DatasourceTemplate_FieldName = "DatasourceTemplate";
            }
        }

        public partial struct DatasourceSettingsFolder
        {
            public static readonly ID ID = new ID("{7A98BE60-9F59-4064-82C2-58DA63562FA5}");

            public partial struct Fields
            {
            }
        }

        public partial struct SiteSettingsRoot
        {
            public static readonly ID ID = new ID("{4C82B6DD-FE7C-4144-BCB3-F21B4080568F}");

            public partial struct Fields
            {
            }
        }
    }
}