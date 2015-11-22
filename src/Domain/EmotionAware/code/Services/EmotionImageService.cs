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
        private const string SubscriptionKey = "2b7889bbf1b242d7af21fa589af49542";

        public async Task<Emotions> GetEmotionFromImage(string stringBase64Image)
        {
            IEmotionsService emotionsService = new EmotionsService(SubscriptionKey);

            MemoryStream faceImage = new MemoryStream(Convert.FromBase64String(stringBase64Image));

            IDictionary<Emotions, float> emotionRanksResult = await emotionsService.ReadEmotionsFromImageStreamAndGetRankedEmotions(faceImage).ConfigureAwait(false);

            if (emotionRanksResult == null)
                return Emotions.None;

            return emotionRanksResult.ElementAt(0).Key;
         
        }
    }
}