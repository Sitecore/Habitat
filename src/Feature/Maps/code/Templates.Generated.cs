namespace Sitecore.Feature.Maps
{
    using global::Sitecore.Data;
    
    public partial struct Templates
    {

        public partial struct MapPoint
        {
            public static readonly ID ID = new ID("{1E6A8C8C-6646-4776-8AB4-615265669633}");

            public partial struct Fields
            {
                public static readonly ID MapPointAddress = new ID("{0295C01D-214C-4C23-AFC2-3F0B4E88B643}");
                public const string MapPointAddress_FieldName = "MapPointAddress";
                public static readonly ID MapPointLocation = new ID("{F686AC8E-1D33-45DB-8E1A-1B40CD491E7A}");
                public const string MapPointLocation_FieldName = "MapPointLocation";
                public static readonly ID MapPointName = new ID("{F12C22BB-E57D-4FAB-96E1-1229E4E7FF0E}");
                public const string MapPointName_FieldName = "MapPointName";
            }
        }

        public partial struct MapPointsFolder
        {
            public static readonly ID ID = new ID("{31713995-C6BF-4CCB-8807-198493508AFA}");

            public partial struct Fields
            {
            }
        }

        public partial struct MapRenderingParameters
        {
            public static readonly ID ID = new ID("{D77856C3-8A5E-452C-8854-F2965EDF25E0}");

            public partial struct Fields
            {
                public static readonly ID CenterLocation = new ID("{3016477A-1DAC-460C-A3E2-0E8834E685BD}");
                public const string CenterLocation_FieldName = "CenterLocation";
                public static readonly ID EnableCenterMapControl = new ID("{35F8D3E6-887E-4E54-B715-B81459846CBB}");
                public const string EnableCenterMapControl_FieldName = "EnableCenterMapControl";
                public static readonly ID EnableMapTypeControl = new ID("{3FDDA0EA-96EF-4533-B658-E071A2A8E052}");
                public const string EnableMapTypeControl_FieldName = "EnableMapTypeControl";
                public static readonly ID EnableRotateControl = new ID("{FD762E2F-71F8-44AC-AE55-22CDB29CDBA2}");
                public const string EnableRotateControl_FieldName = "EnableRotateControl";
                public static readonly ID EnableScaleControl = new ID("{92514B1F-0F21-4A91-AF7F-852E283E1019}");
                public const string EnableScaleControl_FieldName = "EnableScaleControl";
                public static readonly ID EnableStreetViewControl = new ID("{A7862BD0-2DDC-4745-9A03-31D297C12BCD}");
                public const string EnableStreetViewControl_FieldName = "EnableStreetViewControl";
                public static readonly ID EnableZoomControl = new ID("{C77614FB-8EF2-4418-A486-6CF014B70F22}");
                public const string EnableZoomControl_FieldName = "EnableZoomControl";
                public static readonly ID MapType = new ID("{90D0BBDC-EA74-4D9A-A570-DAFD6EDC5F92}");
                public const string MapType_FieldName = "MapType";
                public static readonly ID ZoomLevel = new ID("{405A9441-2F1C-4278-A3DD-3D9F818227BE}");
                public const string ZoomLevel_FieldName = "ZoomLevel";
            }
        }

        public partial struct MapType
        {
            public static readonly ID ID = new ID("{04C34CF5-B7EA-4408-88E8-5FC851173DBD}");

            public partial struct Fields
            {
                public static readonly ID Name = new ID("{4A724065-E4CA-4CDD-9027-F56CEEF1B082}");
                public const string Name_FieldName = "Name";
            }
        }
    }
}