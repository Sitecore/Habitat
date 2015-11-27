namespace Habitat.EmotionAware
{
  using Sitecore.Data;

  public class Templates
  {

    public struct EmotionAwareSettings
    {
      public static ID ID = new ID("{C7A31ED2-0EBB-407F-B6C3-76D4EFEB0501}");

      public struct Fields
      {
        public static readonly ID EmotionAwareSettingsSubscriptionKey = new ID("{5EAEF7FF-F42A-46CD-9FA6-68B429729037}");
      }
    }
    public struct GoalsAndEventsFolder
    {
      public static ID ID = new ID("{89F2323A-6BA5-4B01-A961-45A8396BFFA2}");
    }

    public struct GoalCategory
    {
      public static ID ID = new ID("{DB6E13B8-786C-4DD6-ACF2-3E5E6A959905}");
    }

    public struct Goal
    {
      public static ID ID = new ID("{475E9026-333F-432D-A4DC-52E03B75CB6B}");
    }
  }

}