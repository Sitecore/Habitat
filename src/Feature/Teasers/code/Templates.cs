namespace Sitecore.Feature.Teasers
{
  using Sitecore.Data;

  public struct Templates
  {
    public struct DynamicTeaser
    {
      public static ID ID = new ID("{20A56D46-F5E3-4DB8-8B96-081575363D44}");

      public struct Fields
      {
        public static readonly ID Active = new ID("{9E942565-677F-491C-A0AC-6B930E37342A}");
        public static readonly ID Count = new ID("{A33F9523-96C4-4E42-B6D7-1E861718D373}");
      }
    }

    public struct TeaserHeadline
    {
      public static ID ID = new ID("{C80D124B-B9AC-432E-8C26-DBF3A7F18D20}");
      public struct Fields
      {
        public static readonly ID Title = new ID("{4A59D072-5B41-4A79-A157-2B2CCAC10F2B}");
        public static readonly ID Icon = new ID("{3AF50903-63A9-464B-A375-B94983624E7D}");
      }
    }

    public struct TeaserContent
    {
      public static ID ID = new ID("{FEC0E62A-01FD-40E5-88F3-E5229FE79527}");

      public struct Fields
      {
        public static readonly ID Label = new ID("{3F7E7E3A-4E8E-4639-B079-FC5AEFF172F5}");
        public static readonly ID Summary = new ID("{13D97A52-7C4E-407C-960D-FADDE8A3C1B1}");
        public static readonly ID Image = new ID("{0F6B5546-E0AB-4487-81DE-640C1AA1B65B}");
        public static readonly ID Link = new ID("{E8AB122C-6F54-4D4E-AEC6-F81ADDC558FC}");
      }
    }

    public struct TeaserVideoContent
    {
      public static ID ID = new ID("{04075EB6-6D94-4BF2-9AEB-D29A89CDBA00}");

      public struct Fields
      {
        public static readonly ID VideoLink = new ID("{AC846A16-FD3F-4243-A21F-668A21010C44}");
      }
    }

    public struct Icon
    {
      public static ID ID = new ID("{E90D00B6-0BE9-48E0-9C3F-047274024270}");

      public struct Fields
      {
        public static readonly ID CssClass = new ID("{585F89D1-570C-4F66-A6EC-195A8DA654E1}");
      }
    }
  }
}