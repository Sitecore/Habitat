namespace Sitecore.Foundation.ReCaptcha.WebApi
{
    using System.Threading.Tasks;
    using System.Web.Http;
    using Sitecore.Foundation.ReCaptcha.Models;
    using Sitecore.Foundation.ReCaptcha.Services;

    public class ReCaptchaController : ApiController
    {
        private readonly ISiteVerifyService siteVerifyService;

        public ReCaptchaController() : this(new SiteVerifyService())
        { }

        public ReCaptchaController(ISiteVerifyService siteverifyService)
        {
            this.siteVerifyService = siteverifyService;
        }

        [HttpGet]
        public async Task<ReCaptchaResponseModel> SiteVerify(string response, bool invisible = false)
        {
            return await this.siteVerifyService.SiteVerifyAsync(response, invisible);
        }

    }
}