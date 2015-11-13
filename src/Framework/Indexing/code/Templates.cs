namespace Habitat.Framework.Indexing
{
  using Sitecore.Data;

  internal struct Templates
  {
    internal struct IndexedItem
    {
      public static ID ID = new ID("{8FD6C8B6-A9A4-4322-947E-90CE3D94916D}");

      public struct Fields
      {
        public static readonly ID IncludeInSearchResults = new ID("{8D5C486E-A0E3-4DBE-9A4A-CDFF93594BDA}");
        public const string IncludeInSearchResults_FieldName = "IncludeInSearchResults";
      }
    }
  }
}