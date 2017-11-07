namespace Sitecore.Foundation.ReCaptcha.WebApi
{
    using System.Threading.Tasks;
    using System.Web.Http;
    using Sitecore.Foundation.ReCaptcha.Models;
    using Sitecore.Foundation.ReCaptcha.Services;

    public class ReCaptchaController : ApiController
    {
        private readonly ISiteverifyService siteverifyService;

        public ReCaptchaController()
        {
            this.siteverifyService = new SiteverifyService();
        }

        [HttpGet]
        public async Task<ReCaptchaResponseModel> Siteverify(string response, bool invisible = false)
        {
            return await this.siteverifyService.SiteVerifyAsync(response, invisible);
        }

    }
}