namespace Habitat.Framework.ProjectOxfordAI.Services
{
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;
    using Habitat.Framework.ProjectOxfordAI.Enums;
   
    public interface IEmotionsService
    {
        Task<IDictionary<Emotions, float>> ReadEmotionsFromImageStreamAndGetRankedEmotions(Stream imageStream);

    }
}
