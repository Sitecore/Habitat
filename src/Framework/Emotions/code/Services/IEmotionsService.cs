namespace Habitat.Framework.Emotions.Services
{
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;
    using Habitat.Framework.Emotions.Enums;
   
    public interface IEmotionsService
    {
        Task<IDictionary<Emotions, float>> ReadEmotionsFromImageStreamAndGetRankedEmotions(Stream imageStream);

    }
}
