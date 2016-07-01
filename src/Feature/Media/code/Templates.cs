namespace Sitecore.Feature.Media
{
  using Sitecore.Data;

  public struct Templates
  {
    public struct BackgroundType
    {
      public static readonly ID ID = new ID("{55A5BDAD-EB69-40F5-8195-CDA182E48EE4}");

      public struct Fields
      {
        public static readonly ID Class = new ID("{AF6B8E5C-10A2-46BE-8310-407434EC1055}");
      }
    }

    public struct HasMedia
    {
      public static readonly ID ID = new ID("{A44E450E-BA3F-4FAF-9C53-C63241CC34EB}");

      public struct Fields
      {
        public static readonly ID Title = new ID("{63DDA48B-B0CB-45A7-9A1B-B26DDB41009B}");
        public static readonly ID Description = new ID("{302C9F8D-F703-4F76-A4AB-73D222648232}");
        public static readonly ID Thumbnail = new ID("{4FF62B0A-D73B-4436-BEA2-023154F2FFC4}");
      }
    }

    public struct HasMediaSelector
    {
      public static readonly ID ID = new ID("{AE4635AF-CFBF-4BF6-9B50-00BE23A910C0}");

      public struct Fields
      {
        public static readonly ID MediaSelector = new ID("{72EA8682-24D2-4BEB-951C-3E2164974105}");
      }
    }

    public struct HasMediaImage
    {
      public static readonly ID ID = new ID("{FAE0C913-1600-4EBA-95A9-4D6FD7407E25}");

      public struct Fields
      {
        public static readonly ID Image = new ID("{9F51DEAD-AD6E-41C2-9759-7BE17EB474A4}");
      }
    }

    public struct HasMediaVideo
    {
      public static readonly ID ID = new ID("{5A1B724B-B396-4C48-A833-655CD19018E1}");

      public struct Fields
      {
        public static readonly ID VideoLink = new ID("{2628705D-9434-4448-978C-C3BF166FA1EB}");
      }
    }
  }
}