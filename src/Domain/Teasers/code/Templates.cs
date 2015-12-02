namespace Sitecore.Feature.Teasers
{
  using Sitecore.Data;

  public struct Templates
  {
    public struct Accordeon
    {
      public static ID ID = new ID("{C7D9D293-4EF8-4380-8E10-C4632E729F39}");

      public struct Fields
      {
        public static readonly ID AccordeonSelector = new ID("{9E942565-677F-491C-A0AC-6B930E37342A}");
      }
    }

    public struct TeaserContent
    {
      public static ID ID = new ID("{FEC0E62A-01FD-40E5-88F3-E5229FE79527}");

      public struct Fields
      {
        public static readonly ID Title = new ID("{4A59D072-5B41-4A79-A157-2B2CCAC10F2B}");
        public static readonly ID Summary = new ID("{13D97A52-7C4E-407C-960D-FADDE8A3C1B1}");
        public static readonly ID Image = new ID("{0F6B5546-E0AB-4487-81DE-640C1AA1B65B}");
        public static readonly ID Link = new ID("{E8AB122C-6F54-4D4E-AEC6-F81ADDC558FC}");
      }
    }
  }
}