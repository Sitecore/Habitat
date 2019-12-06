namespace Sitecore.Feature.Maps
{
  using Data;

  public static class Templates
  {
    public static class MapPoint
    {
      public static readonly ID ID = new ID("{1E6A8C8C-6646-4776-8AB4-615265669633}");

      public static class Fields
      {
        public static readonly ID Name = new ID("{F12C22BB-E57D-4FAB-96E1-1229E4E7FF0E}");
        public static readonly ID Address = new ID("{0295C01D-214C-4C23-AFC2-3F0B4E88B643}");
        public static readonly ID Location = new ID("{F686AC8E-1D33-45DB-8E1A-1B40CD491E7A}");
      }
    }

    public static class MapPointsFolder
    {
      public static readonly ID ID = new ID("{31713995-C6BF-4CCB-8807-198493508AFA}");
    }
  }
}