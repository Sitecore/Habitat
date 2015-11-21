namespace Habitat.EmotionAware.Services
{
    using Habitat.Framework.ProjectOxfordAI.Enums;

    public interface IEmotionImageService
    {
        Emotions GetEmotionFromImage(string stringBase64Image);
    }
}