using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sitecore.Feature.Language.Controllers
{
    using System.Net;
    using System.Web.Mvc;
    using Sitecore.Feature.Language.Infrastructure.Pipelines;
    using Sitecore.Pipelines;

    public class LanguageController : Controller
    {
        [HttpPost]
        public JsonResult ChangeLanguage(string newLanguage, string currentLanguage)
        {
            var args = new ChangeLanguagePipelineArgs(currentLanguage, newLanguage);
            CorePipeline.Run("language.changeLanguage", args, false);

            return new JsonResult {Data = args.CustomData};
        }
    }
}