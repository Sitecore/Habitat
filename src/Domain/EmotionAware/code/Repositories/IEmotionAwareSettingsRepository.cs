namespace Habitat.EmotionAware.Repositories
{
  using System.Collections.Generic;
  using Habitat.EmotionAware.Models;
  using Sitecore.Data.Items;

  public interface IEmotionAwareSettingsRepository
  {
    EmotionAwareSettings Get();

  }
}