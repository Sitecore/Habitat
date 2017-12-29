namespace Sitecore.Feature.Demo
{
    using global::Sitecore.Data;
    
    public partial struct Templates
    {

        public partial struct CampaignToken
        {
            public static readonly ID ID = new ID("{01628DCB-611F-495E-A1DA-E89027AB7035}");

            public partial struct Fields
            {
                public static readonly ID TokenValue = new ID("{3B485BD9-DEAE-4AAF-9C32-4106C4214162}");
                public const string TokenValue_FieldName = "Token Value";
            }
        }

        public partial struct DemoContent
        {
            public static readonly ID ID = new ID("{1224B40E-7B6C-42B3-A6D0-C40A6C61345C}");

            public partial struct Fields
            {
                public static readonly ID HTMLContent = new ID("{0BC0AEDF-A6D0-4F74-933C-BD1779CD40B2}");
                public const string HTMLContent_FieldName = "HTML Content";
                public static readonly ID AreaCode = new ID("{D19D087F-6F79-4D20-A14B-78CF5CF4FF7D}");
                public const string AreaCode_FieldName = "Area Code";
                public static readonly ID BusinessName = new ID("{666FE277-D4DD-435E-B2BE-3A02612E8A5B}");
                public const string BusinessName_FieldName = "Business Name";
                public static readonly ID City = new ID("{FB9E9F07-C887-45D8-8F34-F156F17C57C9}");
                public const string City_FieldName = "City";
                public static readonly ID Country = new ID("{0DBCDBCB-185A-4845-8ACB-97D5A1E1C401}");
                public const string Country_FieldName = "Country";
                public static readonly ID DNS = new ID("{CCABB4F1-5559-4E28-8A47-A22251B79746}");
                public const string DNS_FieldName = "DNS";
                public static readonly ID ISP = new ID("{2064B0FB-A738-467A-A262-3EB2E1E9E2F0}");
                public const string ISP_FieldName = "ISP";
                public static readonly ID Latitude = new ID("{D0A49D11-B9A3-43E1-AF51-66804DF3B63B}");
                public const string Latitude_FieldName = "Latitude";
                public static readonly ID Longitude = new ID("{D34BEF48-023B-4930-8360-A89BEB8B3D82}");
                public const string Longitude_FieldName = "Longitude";
                public static readonly ID MetroCode = new ID("{84958826-58AE-4B51-A1BA-667F08995879}");
                public const string MetroCode_FieldName = "Metro Code";
                public static readonly ID PostalCode = new ID("{0B1FA959-43BE-478E-8013-3DB7D2921554}");
                public const string PostalCode_FieldName = "Postal Code";
                public static readonly ID Region = new ID("{26FF4697-C334-49EE-8AAE-9480DFEB40C3}");
                public const string Region_FieldName = "Region";
                public static readonly ID Url = new ID("{F0FFE350-6033-4E41-A99F-91ECCA49BC7C}");
                public const string Url_FieldName = "Url";
                public static readonly ID IPAddress = new ID("{1618E249-E670-46A7-A198-6E85B7745726}");
                public const string IPAddress_FieldName = "IP Address";
                public static readonly ID Referrer = new ID("{D28EFE17-C491-47C0-B62D-934DC9DA38A4}");
                public const string Referrer_FieldName = "Referrer";
            }
        }

        public partial struct LinkToken
        {
            public static readonly ID ID = new ID("{359AD0B9-20B5-49E7-A430-891D6E67BC5C}");

            public partial struct Fields
            {
                public static readonly ID TokenValue = new ID("{4A27A4AA-9201-4D1D-B612-F45DDA086C4C}");
                public const string TokenValue_FieldName = "Token Value";
            }
        }

        public partial struct ProfilingSettings
        {
            public static readonly ID ID = new ID("{C6D4DDD5-B912-4C1A-A3A3-E1D90E4D0939}");

            public partial struct Fields
            {
                public static readonly ID SiteProfiles = new ID("{2A84ECA4-68BB-4451-B4AC-98EA71A5A3DC}");
                public const string SiteProfiles_FieldName = "SiteProfiles";
            }
        }

        public partial struct TextToken
        {
            public static readonly ID ID = new ID("{327D6873-A384-435A-BE87-7E869782B243}");

            public partial struct Fields
            {
                public static readonly ID TokenValue = new ID("{7B9F4FE3-6285-4969-9784-B96D49973161}");
                public const string TokenValue_FieldName = "Token Value";
            }
        }

        public partial struct Token
        {
            public static readonly ID ID = new ID("{A7EBF38A-5F66-4579-92D1-568A8BA50293}");

            public partial struct Fields
            {
            }
        }
    }
}