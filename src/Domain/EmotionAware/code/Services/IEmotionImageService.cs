namespace Habitat.EmotionAware.Services
{
  using System.Threading.Tasks;
  using Habitat.Framework.ProjectOxfordAI.Enums;

  public interface IEmotionImageService
  {
    Task<Emotions> GetEmotionFromImage(string stringBase64Image);
  }
}