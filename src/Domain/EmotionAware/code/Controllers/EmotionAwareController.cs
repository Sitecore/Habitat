namespace Habitat.EmotionAware.Controllers
{
    using System.Web.Mvc;
    using Habitat.EmotionAware.Services;
    using Habitat.Framework.ProjectOxfordAI.Enums;
    using Sitecore.Mvc.Controllers;

    public class EmotionAwareController : SitecoreController
    {

        private readonly IEmotionImageService emotionImageService;


        public EmotionAwareController() : this(new EmotionImageService())
        {
        }

        public EmotionAwareController(IEmotionImageService emotionImageService)
        {
            this.emotionImageService = emotionImageService;
        }



        public ActionResult RegisterEmotion(string emotionImageStream)
        {
            if (string.IsNullOrWhiteSpace(emotionImageStream))
                return this.Json(new { Success = false, Message = "No image was received" });


            Emotions emotion = this.emotionImageService.GetEmotionFromImage(emotionImageStream);

            return this.Json(new { Success = false, Message = emotion.ToString() });
        }
    }
}