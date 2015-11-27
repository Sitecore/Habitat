namespace Habitat.EmotionAware.Services
{
  using System;
  using System.IO;
  using System.Threading.Tasks;
  using Habitat.Framework.ProjectOxfordAI.Enums;
  using Habitat.Framework.ProjectOxfordAI.Services;
  using System.Collections.Generic;
  using System.Linq;
  using Habitat.EmotionAware.Models;
  using Habitat.EmotionAware.Repositories;
  using Sitecore.Diagnostics;


  public class EmotionImageService : IEmotionImageService
  {
    private readonly IEmotionAwareSettingsRepository emotionAwareSettingsRepository;

    private readonly EmotionAwareSettings emotionAwareSettings;

    public EmotionImageService(IEmotionAwareSettingsRepository emotionAwareSettingsRepository)
    {
      this.emotionAwareSettingsRepository = emotionAwareSettingsRepository;
    }

    public EmotionImageService() : this(new EmotionAwareSettingsRepository())
    {
      emotionAwareSettings = emotionAwareSettingsRepository.Get();
    }

    public async Task<Emotions> GetEmotionFromImage(string stringBase64Image)
    {

      if (this.emotionAwareSettings == null || string.IsNullOrWhiteSpace(emotionAwareSettings.SubscriptionKey))
      {
        Log.Error("SubscriptionKey is missing for emotion service", typeof(EmotionImageService));
        return Emotions.None;
      }

      IEmotionsService emotionsService = new EmotionsService(emotionAwareSettings.SubscriptionKey);

      MemoryStream faceImage = new MemoryStream(Convert.FromBase64String(stringBase64Image));

      IDictionary<Emotions, float> emotionRanksResult = await emotionsService.ReadEmotionsFromImageStreamAndGetRankedEmotions(faceImage);

      if (emotionRanksResult == null)
        return Emotions.None;

      return emotionRanksResult.ElementAt(0).Key;

    }



  }


}