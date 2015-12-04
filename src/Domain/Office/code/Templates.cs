namespace Habitat.Office
{
  using Sitecore.Data;

  public struct Templates
  {
    public struct Office
    {
      public static ID ID = new ID("{1E6A8C8C-6646-4776-8AB4-615265669633}");

      public struct Fields
      {
        public static ID Name = new ID("{F12C22BB-E57D-4FAB-96E1-1229E4E7FF0E}");
        public const string Name_FieldName = "OfficeName";

        public static ID Address = new ID("{0295C01D-214C-4C23-AFC2-3F0B4E88B643}");
        public const string Address_FieldName = "OfficeAddress";

        public static ID Latitude = new ID("{67EBBB8D-88BB-41A5-96EF-CEDB7B9B637E}");
        public const string Latitude_FieldName = "OfficeLatitude";

        public static ID Longitude = new ID("{73C04A83-BEB9-4B24-9116-F9BC51A5BAAD}");
        public const string Longitude_FieldName = "OfficeLongitude";
      }
    }

    public struct OfficeFolder
    {
      public static ID ID = new ID("{31713995-C6BF-4CCB-8807-198493508AFA}");
    }
  }
}