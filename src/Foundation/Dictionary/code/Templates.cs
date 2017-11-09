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

    public struct DictionaryPluralEntry
    {
      public static ID ID => new ID("{EA8709B5-ADDD-4179-9F54-40D709421EFF}");
      public struct Fields
      {
        public static ID PhraseZero => new ID("{75C574D7-9FBA-4DFD-BE5B-5405655DAF12}");
        public static ID PhraseOne => new ID("{BC70DEBF-7886-4835-9A65-D3276A098F03}");
        public static ID PhraseTwo => new ID("{37D72E7F-BA96-4275-AA13-122218894060}");
        public static ID PhraseFew => new ID("{E4200F91-B221-4F82-A9E6-C0B1FEFE1BA5}");
        public static ID PhraseMany => new ID("{AA1AB933-15B6-42AE-B6C5-86225CE23A24}");
        public static ID PhraseOther => new ID("{3FAA1728-51C9-44EE-8AF3-73F1E856A1E9}");
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