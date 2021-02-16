using System.Linq;

namespace Sitecore.Feature.Language.Controllers
{
    using System.Web.Mvc;
    using Sitecore.Abstractions;
    using Sitecore.Feature.Language.Infrastructure.Pipelines;
    using Sitecore.Feature.Language.Models;
    using Sitecore.Feature.Language.Repositories;
    using Sitecore.Foundation.SitecoreExtensions.Attributes;

    public class LanguageController : Controller
    {
        public const string ChangeLanguagePipeline = "language.changeLanguage";

        private ILanguageRepository LanguageRepository { get; }
        private BaseCorePipelineManager PipelineManager { get; }

        public LanguageController(ILanguageRepository languageRepository, BaseCorePipelineManager pipelineManager)
        {
            this.LanguageRepository = languageRepository;
            this.PipelineManager = pipelineManager;
        }

        [HttpPost]
        [SkipAnalyticsTracking]
        public JsonResult ChangeLanguage(string newLanguage, string currentLanguage)
        {
            var args = new ChangeLanguagePipelineArgs(currentLanguage, newLanguage);
            this.PipelineManager.Run(ChangeLanguagePipeline, args, false);

            return new JsonResult {Data = args.CustomData};
        }

        public ActionResult LanguageSelector()
        {
            var model = new LanguageSelector
            {
                ActiveLanguage = this.LanguageRepository.GetActive(),
                SupportedLanguages = this.LanguageRepository.GetSupportedLanguages().ToList()
            };
            return this.View(model);
        }
    }
}