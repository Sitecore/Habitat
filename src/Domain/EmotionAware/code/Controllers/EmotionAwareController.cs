namespace Habitat.EmotionAware.Controllers
{
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using Habitat.EmotionAware.Services;
    using Habitat.Framework.ProjectOxfordAI.Enums;
    using Sitecore.Mvc.Controllers;

    public class EmotionAwareController : Controller
    {

        private readonly IEmotionImageService emotionImageService;


        public EmotionAwareController() : this(new EmotionImageService())
        {
        }

        public EmotionAwareController(IEmotionImageService emotionImageService)
        {
            this.emotionImageService = emotionImageService;
        }


        [HttpPost]
        public ActionResult RegisterEmotion(string emotionImageStream)
        {
            if (string.IsNullOrWhiteSpace(emotionImageStream))
                return this.Json(new { Success = false, Message = "No image was received" });

            Emotions emotion = Task.Run(() => this.emotionImageService.GetEmotionFromImage(emotionImageStream)).Result;

            //Register the emotion on the contact
            if (Sitecore.Analytics.Tracker.Current.Contact.Tags.Find("Emotion") != null)
                Sitecore.Analytics.Tracker.Current.Contact.Tags.Set("Emotion", emotion.ToString());
            else
                Sitecore.Analytics.Tracker.Current.Contact.Tags.Add("Emotion", emotion.ToString());

            return this.Json(new { Success = true, Message = emotion.ToString() });
        }


    }
}