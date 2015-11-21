namespace Habitat.EmotionAware.Services
{
    using System;
    using System.IO;
    using System.Threading.Tasks;
    using Habitat.Framework.ProjectOxfordAI.Enums;
    using Habitat.Framework.ProjectOxfordAI.Services;
    using System.Collections.Generic;
    using System.Linq;
    

    public class EmotionImageService : IEmotionImageService
    {
        private string SubscriptionKey() => "2b7889bbf1b242d7af21fa589af49542";

        public Emotions GetEmotionFromImage(string stringBase64Image)
        {
            IEmotionsService emotionsService = new EmotionsService(this.SubscriptionKey());

            MemoryStream faceImage = new MemoryStream(Convert.FromBase64String(stringBase64Image));

            Task<IDictionary<Emotions, float>> task = emotionsService.ReadEmotionsFromImageStreamAndGetRankedEmotions(faceImage);

            IDictionary<Emotions, float> emotionRanksResult = task.GetAwaiter().GetResult();

            if (emotionRanksResult == null)
                return Emotions.None;

            //Sitecore.Analytics.Tracker.Current.Contact.Tags.Add("Emotion", emotionRanksResult.ElementAt(0).Key.ToString());

            return emotionRanksResult.ElementAt(0).Key;
         
        }
    }
}