namespace Habitat.Framework.ProjectOxfordAI.Services
{
  using System;
  using System.Collections.Generic;
  using System.IO;
  using System.Linq;
  using System.Reflection;
  using System.Threading.Tasks;
  using Habitat.Framework.ProjectOxfordAI.Enums;
  using Habitat.Framework.ProjectOxfordAI.Models;
  using Microsoft.ProjectOxford.Emotion;
  using Microsoft.ProjectOxford.Emotion.Contract;

  public class EmotionsService : IEmotionsService, IDisposable
  {
    private readonly string subscriptionKey;

    public EmotionsService(string subscriptionKey)
    {
      this.subscriptionKey = subscriptionKey;
    }


    public async Task<IDictionary<Emotions, float>> ReadEmotionsFromImageStreamAndGetRankedEmotions(Stream imageStream)
    {
      EmotionServiceClient emotionServiceClient = new EmotionServiceClient(this.subscriptionKey);

      Emotion[] emotions = await emotionServiceClient.RecognizeAsync(imageStream).ConfigureAwait(false);

      Emotion emotion = emotions.FirstOrDefault();

      if (emotion == null)
        return null;

      return this.CalculateAndRankScoreToDictionary(emotion.Scores);

    }


    private IList<EmotionRank> CalculateAndRankScore(Scores emotionScores)
    {
      if (emotionScores == null)
        return null;

      IList<EmotionRank> emotionRankingList = new List<EmotionRank>();

      foreach (PropertyInfo prop in emotionScores.GetType().GetProperties())
      {
        for (int index = 0; index < Enum.GetNames(typeof(Emotions)).Length; index++)
        {
          string emotionName = Enum.GetNames(typeof(Emotions))[index];

          if (!prop.Name.Contains(emotionName))
          {
            continue;
          }

          Emotions parsedEmotion = Emotions.None;

          Enum.TryParse(emotionName, out parsedEmotion);

          emotionRankingList.Add(new EmotionRank()
          {
            Emotion = parsedEmotion,
            Rank = (float)prop.GetValue(emotionScores)
          });

        }
      }

      //Sort and set highest value on top
      return emotionRankingList.OrderByDescending(x => x.Rank).ToList();
    }

    private IDictionary<Emotions, float> CalculateAndRankScoreToDictionary(Scores emotionScores)
    {
      if (emotionScores == null)
        return null;

      IDictionary<Emotions, float> emotionRankingDictionary = new Dictionary<Emotions, float>();

      foreach (PropertyInfo prop in emotionScores.GetType().GetProperties())
      {
        for (int index = 0; index < Enum.GetNames(typeof(Emotions)).Length; index++)
        {
          string emotionName = Enum.GetNames(typeof(Emotions))[index];

          if (!prop.Name.Contains(emotionName))
          {
            continue;
          }

          Emotions parsedEmotion = Emotions.None;

          Enum.TryParse(emotionName, out parsedEmotion);


          emotionRankingDictionary.Add(parsedEmotion, (float)prop.GetValue(emotionScores));

        }
      }

      //Sort and set highest value on top
      return emotionRankingDictionary.OrderByDescending(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
    }

    public void Dispose()
    {

    }
  }
}