namespace Sitecore.Feature.PageContent
{
  using Sitecore.Data;

  public struct Templates
  {
    public struct HasPageContent
    {
      public static ID ID = new ID("{AF74A00B-8CA7-4C9A-A5C1-156A68590EE2}");

      public struct Fields
      {
        public static readonly ID Title = new ID("{C30A013F-3CC8-4961-9837-1C483277084A}");
        public const string Title_FieldName = "Title";
        public static readonly ID Summary = new ID("{AC3FD4DB-8266-476D-9635-67814D91E901}");
        public const string Summary_FieldName = "Summary";
        public static readonly ID Body = new ID("{D74F396D-5C5E-4916-BD0A-BFD58B6B1967}");
        public const string Body_FieldName = "Body";
        public static readonly ID Image = new ID("{9492E0BB-9DF9-46E7-8188-EC795C4ADE44}");
      }
    }
  }
}