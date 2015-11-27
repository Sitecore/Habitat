namespace Habitat.EmotionAware.Services
{
  using Habitat.Framework.ProjectOxfordAI.Enums;
  using Sitecore.Analytics.Model;
  using Sitecore.Analytics.Model.Entities;

  public interface IEmotionAnalyticsService
  {
    ITagValue RegisterEmotionOnCurrentContact(Emotions emotion);
    PageEventData RegisterGoal(string goalName, string pageUrl);
  }
}