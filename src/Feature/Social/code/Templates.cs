namespace Sitecore.Feature.Social
{
  using Sitecore.Data;

  public struct Templates
  {
    public struct OpenGraph
    {
      public static ID ID = new ID("{BDD24F35-05E8-4466-8798-7D3DD6A6C991}");

      public struct Fields
      {
        public static readonly ID Title = new ID("{0EE2F208-1FEE-42FC-8051-6696D49A92BF}");
        public static readonly ID Description = new ID("{A12D5346-87EE-484D-83C5-F1E8E1B99666}");
        public static readonly ID Image = new ID("{11F41661-E5FE-44E7-B8DA-7CFF2D39B4B2}");
      }
    }

    public struct TwitterFeed
    {
      public static ID ID = new ID("{BDD24F35-05E8-4466-8798-7D3DD6A6C991}");

      public struct Fields
      {
        public static readonly ID FeedTitle = new ID("{099E4085-150C-4073-88D9-8B159D9A8B01}");
        public static readonly ID TwitterUrl = new ID("{92EF8986-45E2-42DE-913F-B91FD960297A}");
        public static readonly ID WidgetId = new ID("{D1CF33B1-8B8A-4AAB-AA7E-2460566BEDED}");
      }
    }
  }
}