namespace Sitecore.Feature.Multisite
{
    using global::Sitecore.Data;
    
    public partial struct Templates
    {

        public partial struct SiteConfiguration
        {
            public static readonly ID ID = new ID("{0FCCFE4F-B087-498F-BD26-5CDFFC522C9A}");

            public partial struct Fields
            {
                public static readonly ID ShowInMenu = new ID("{12537182-F35C-403F-AFB5-747D55C450B8}");
                public const string ShowInMenu_FieldName = "ShowInMenu";
                public static readonly ID Title = new ID("{F07811D3-41E9-440A-9D81-310C1D78BED6}");
                public const string Title_FieldName = "Title";
            }
        }
    }
}