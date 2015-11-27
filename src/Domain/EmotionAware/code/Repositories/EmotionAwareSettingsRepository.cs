namespace Habitat.EmotionAware.Repositories
{
  using System.EnterpriseServices;
  using Habitat.Framework.SitecoreExtensions.Extensions;
  using Sitecore.Data.Items;
  using System.Linq;
  using Habitat.EmotionAware.Models;
  using Habitat.Framework.SitecoreExtensions.Repositories;
  using Sitecore;

  public class EmotionAwareSettingsRepository : IEmotionAwareSettingsRepository
  {

    private readonly Item emotionAwareSettingsItem;

    public EmotionAwareSettingsRepository()
    {
      emotionAwareSettingsItem = GetEmotionAwareSettings();
    }

    private Item GetEmotionAwareSettings()
    {
      return Context.Site.GetContextItem(Templates.EmotionAwareSettings.ID);
    }


    public EmotionAwareSettings Get()
    {
      if (emotionAwareSettingsItem == null)
        return null;

      return new EmotionAwareSettings()
      {
        SubscriptionKey = emotionAwareSettingsItem.GetString(Templates.EmotionAwareSettings.Fields.EmotionAwareSettingsSubscriptionKey)
      };

    }
  }
}