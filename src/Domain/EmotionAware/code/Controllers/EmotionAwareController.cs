namespace Habitat.EmotionAware.Controllers
{
  using System.Threading.Tasks;
  using System.Web.Mvc;
  using Habitat.EmotionAware.Services;
  using Habitat.Framework.ProjectOxfordAI.Enums;


  public class EmotionAwareController : Controller
  {

    private readonly IEmotionImageService emotionImageService;
    private readonly IEmotionAnalyticsService emotionAnalyticsService;


    public EmotionAwareController() : this(new EmotionImageService(), new EmotionAnalyticsService())
    {
    }

    public EmotionAwareController(IEmotionImageService emotionImageService, IEmotionAnalyticsService emotionAnalyticsService)
    {
      this.emotionImageService = emotionImageService;
      this.emotionAnalyticsService = emotionAnalyticsService;
    }


    [HttpPost]
    public ActionResult RegisterEmotion(string emotionImageStream, string pageUrl)
    {
      if (string.IsNullOrWhiteSpace(emotionImageStream))
        return this.Json(new { Success = false, Message = "No image was received" });

      Emotions emotion = Task.Run(() => this.emotionImageService.GetEmotionFromImage(emotionImageStream)).Result;

      if (emotion == Emotions.None)
        return this.Json(new { Success = false, Message = "No emotion was detected" });

      this.emotionAnalyticsService.RegisterEmotionOnCurrentContact(emotion);

      this.emotionAnalyticsService.RegisterGoal(emotion.ToString(), pageUrl);

      return this.Json(new { Success = true, Message = emotion.ToString() });
    }


  }
}