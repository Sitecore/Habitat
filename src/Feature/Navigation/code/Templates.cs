namespace Sitecore.Feature.Navigation
{
  using Sitecore.Data;

  public static class Templates
  {
    public static class NavigationRoot
    {
      public static readonly ID ID = new ID("{F9F4FC05-98D0-4C62-860F-F08AE7F0EE25}");
    }

    public static class Navigable
    {
      public static readonly ID ID = new ID("{A1CBA309-D22B-46D5-80F8-2972C185363F}");

      public static class Fields
      {
        public static readonly ID ShowInNavigation = new ID("{5585A30D-B115-4753-93CE-422C3455DEB2}");
        public static readonly ID NavigationTitle = new ID("{1B483E91-D8C4-4D19-BA03-462074B55936}");
        public static readonly ID ShowChildren = new ID("{68016087-AA00-45D6-922A-678475C50D4A}");
      }
    }

    public static class Link
    {
      public static readonly ID ID = new ID("{A16B74E9-01B8-439C-B44E-42B3FB2EE14B}");

      public static class Fields
      {
        public static readonly ID Link = new ID("{FE71C30E-F07D-4052-8594-C3028CD76E1F}");
      }
    }

    public static class LinkMenuItem
    {
      public static readonly ID ID = new ID("{18BAF6B0-E0D6-4CCE-9184-A4849343E7E4}");

      public static class Fields
      {
        public static readonly ID Icon = new ID("{2C24649E-4460-4114-B026-886CFBE1A96D}");
        public static readonly ID DividerBefore = new ID("{4231CD60-47C1-42AD-B838-0A6F8F1C4CFB}");
      }
    }
  }
}