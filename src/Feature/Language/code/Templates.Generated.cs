namespace Sitecore.Feature.Language
{
    using global::Sitecore.Data;
    
    public partial struct Templates
    {

        public partial struct LanguageSettings
        {
            public static readonly ID ID = new ID("{748EBA96-3F0C-4F45-8AFB-DE8DCC707B84}");

            public partial struct Fields
            {
                public static readonly ID SupportedLanguages = new ID("{5F115B6D-6052-4C7E-B442-AE923A7E9BD2}");
                public const string SupportedLanguages_FieldName = "SupportedLanguages";
            }
        }
    }
}