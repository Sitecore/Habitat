namespace Sitecore.Foundation.Dictionary
{
  using Sitecore.Data;

  public struct Templates
  {
    public struct DictionaryFolder
    {
      public static ID ID => new ID("{98E4BDC6-9B43-4EB2-BAA3-D4303C35852E}");
    }

    public struct DictionaryEntry
    {
      public static ID ID => new ID("{EC4DD3F2-590D-404B-9189-2A12679749CC}");
      public struct Fields
      {
        public static ID Phrase => new ID("{DDACDD55-5B08-405F-9E58-04F09AED640A}");
      }
    }

    public struct DictionarySettings
    {
      public static ID ID => new ID("{31D191DD-3FA1-4D2F-A348-7F315F72279F}");
      public struct Fields
      {
        public static ID DefaultLanguage => new ID("{36DB0022-5858-4CF7-9BCC-4C3ADC002FB3}");
      }
    }
  }
}