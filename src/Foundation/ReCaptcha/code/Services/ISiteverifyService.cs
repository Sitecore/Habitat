namespace Sitecore.Foundation.ReCaptcha.Services
{
    using System.Threading.Tasks;
    using Sitecore.Foundation.ReCaptcha.Models;

    public interface ISiteverifyService
    {
        Task<ReCaptchaResponseModel> SiteVerifyAsync(string response, bool invisible = false);
    }
}
