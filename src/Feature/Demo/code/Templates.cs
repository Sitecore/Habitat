namespace Sitecore.Feature.Demo
{
  using Sitecore.Data;

  public struct Templates
  {
    public struct ProfilingSettings
    {
      public static readonly ID ID = new ID("{C6D4DDD5-B912-4C1A-A3A3-E1D90E4D0939}");

      public struct Fields
      {
        public static readonly ID SiteProfiles = new ID("{2A84ECA4-68BB-4451-B4AC-98EA71A5A3DC}");
      }
    }

    public struct DemoContent
    {
      public static readonly ID ID = new ID("{1224B40E-7B6C-42B3-A6D0-C40A6C61345C}");

      public struct Fields
      {
        public static readonly ID HTMLContent = new ID("{0BC0AEDF-A6D0-4F74-933C-BD1779CD40B2}");
      }
    }

    public struct Token
    {
      public static readonly ID ID = new ID("{A7EBF38A-5F66-4579-92D1-568A8BA50293}");
      public struct Fields
      {
        public static readonly string TokenValue = "Token Value";
      }
    }
  }
}