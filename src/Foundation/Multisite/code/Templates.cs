namespace Sitecore.Foundation.Multisite
{
  using Sitecore.Data;
  using Sitecore.Shell.Framework.Commands.Masters;

  public class Templates
  {
    public struct Site
    {
      public static ID ID = new ID("{BB85C5C2-9F87-48CE-8012-AF67CF4F765D}");
    }

    public struct DatasourceConfiguration
    {
      public static ID ID = new ID("{C82DC5FF-09EF-4403-96D3-3CAF377B8C5B}");

      public struct Fields
      {
        public static readonly ID DatasourceLocation = new ID("{5FE1CC43-F86C-459C-A379-CD75950D85AF}");
        public static readonly ID DatasourceTemplate = new ID("{498DD5B6-7DAE-44A7-9213-1D32596AD14F}");
      }
    }

    public struct SiteSettings
    {
      public static ID ID = new ID("{BCCFEBEA-DCCB-48FE-9570-6503829EC03F}");
    }

    public struct RenderingOptions
    {
      public static ID ID = new ID("{D1592226-3898-4CE2-B190-090FD5F84A4C}");

      public struct Fields
      {
        public static readonly ID DatasourceLocation = new ID("{B5B27AF1-25EF-405C-87CE-369B3A004016}");
        public static readonly ID DatasourceTemplate = new ID("{1A7C85E5-DC0B-490D-9187-BB1DBCB4C72F}");
      }
    }
  }
}