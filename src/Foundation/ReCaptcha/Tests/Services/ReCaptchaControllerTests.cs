using System.Threading.Tasks;
using Sitecore.Foundation.ReCaptcha.Models;
using Sitecore.Foundation.ReCaptcha.Services;
using NSubstitute;
using Xunit;
using FluentAssertions;
using Sitecore.Foundation.Testing.Attributes;
using Sitecore.FakeDb;

namespace Sitecore.Foundation.ReCaptcha.Tests.Services
{
    using Sitecore.Foundation.ReCaptcha.WebApi;

    public class ReCaptchaControllerTests
    {

        [Theory]
        [AutoDbData]
        public void Error_Success_Should_Be_True_Hostname_testkey(Db db)
        {
            ISiteVerifyService service = new SiteVerifyService();
            ReCaptchaController controller = new ReCaptchaController(service);

            db.Configuration.Settings["Foundation.ReCaptcha.V2.SiteKey"] = "6LeIxAcTAAAAAJcZVRqyHh71UMIEGNQ_MXjiZKhI";
            db.Configuration.Settings["Foundation.ReCaptcha.V2.Secret"] = "6LeIxAcTAAAAAGG-vFI1TnRWxMZNFuojJ4WifJWe";

            var result = controller.SiteVerify("").ConfigureAwait(false).GetAwaiter().GetResult();

            result.ErrorCodes.Should().BeNull();
            result.ChallengeTs.Should().NotBeNullOrEmpty();
            result.Hostname.Should().Be("testkey.google.com");
            result.Success.Should().BeTrue();
        }

    }
}
