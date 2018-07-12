namespace Sitecore.Feature.Demo
{
  using Sitecore.Data;

  public struct Templates
  {
    public struct ProfilingSettings
    {
      public static readonly ID ID = new ID("{C6D4DDD5-B912-4C1A-A3A3-E1D90E4D0939}");

      public struct Fields
      {
        public static readonly ID SiteProfiles = new ID("{2A84ECA4-68BB-4451-B4AC-98EA71A5A3DC}");
      }
    }

    public struct DemoContent
    {
      public static readonly ID ID = new ID("{1224B40E-7B6C-42B3-A6D0-C40A6C61345C}");

      public struct Fields
      {
                public static readonly ID HtmlContent = new ID("{0BC0AEDF-A6D0-4F74-933C-BD1779CD40B2}");
                public static readonly ID Referrer = new ID("{D28EFE17-C491-47C0-B62D-934DC9DA38A4}");
                public static readonly ID IpAddress = new ID("{1618E249-E670-46A7-A198-6E85B7745726}");

                public static readonly ID Latitude = new ID("{D0A49D11-B9A3-43E1-AF51-66804DF3B63B}");
                public static readonly ID Longitude = new ID("{D34BEF48-023B-4930-8360-A89BEB8B3D82}");
                public static readonly ID AreaCode = new ID("{D19D087F-6F79-4D20-A14B-78CF5CF4FF7D}");
                public static readonly ID BusinessName = new ID("{666FE277-D4DD-435E-B2BE-3A02612E8A5B}");
                public static readonly ID City = new ID("{FB9E9F07-C887-45D8-8F34-F156F17C57C9}");
                public static readonly ID Country = new ID("{0DBCDBCB-185A-4845-8ACB-97D5A1E1C401}");
                public static readonly ID DNS = new ID("{CCABB4F1-5559-4E28-8A47-A22251B79746}");
                public static readonly ID ISP = new ID("{2064B0FB-A738-467A-A262-3EB2E1E9E2F0}");
                public static readonly ID MetroCode = new ID("{84958826-58AE-4B51-A1BA-667F08995879}");
                public static readonly ID PostalCode = new ID("{0B1FA959-43BE-478E-8013-3DB7D2921554}");
                public static readonly ID Region = new ID("{26FF4697-C334-49EE-8AAE-9480DFEB40C3}");
                public static readonly ID Url = new ID("{F0FFE350-6033-4E41-A99F-91ECCA49BC7C}");
      }
    }

    public struct Token
    {
      public static readonly ID ID = new ID("{A7EBF38A-5F66-4579-92D1-568A8BA50293}");

      public struct Fields
      {
        public static readonly string TokenValue = "Token Value";
      }
    }
  }
}