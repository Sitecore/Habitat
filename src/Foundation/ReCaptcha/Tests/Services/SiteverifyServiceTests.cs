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
    public class SiteverifyServiceTests
    {
        [Fact]
        public async void Error_Codes_Should_Be_Null()
        {
            var siteVerifyService = Substitute.For<ISiteVerifyService>();

            var response = new ReCaptchaResponseModel
            {
                ErrorCodes = { },
                Success = true
            };

            siteVerifyService.SiteVerifyAsync("").Returns(Task.FromResult(response));

            var result = await siteVerifyService.SiteVerifyAsync("");

            result.ErrorCodes.Should().BeNull();
            result.Success.Should().BeTrue();
        }

        [Fact]
        public async void Error_Codes_Should_Not_Be_Empty()
        {
            var siteVerifyService = Substitute.For<ISiteVerifyService>();

            var response = new ReCaptchaResponseModel
            {
                ErrorCodes = new[] { "" },
                Success = false
            };

            siteVerifyService.SiteVerifyAsync("").Returns(Task.FromResult(response));

            var result = await siteVerifyService.SiteVerifyAsync("");

            result.ErrorCodes.Should().NotBeEmpty();
            result.Success.Should().BeFalse();
        }

        [Theory]
        [AutoDbData]
        public void Error_Success_Should_Be_True_Hostname_testkey(Db db)
        {
            ISiteVerifyService siteVerifyService = new SiteVerifyService();

            db.Configuration.Settings["Foundation.ReCaptcha.V2.SiteKey"] = "6LeIxAcTAAAAAJcZVRqyHh71UMIEGNQ_MXjiZKhI";
            db.Configuration.Settings["Foundation.ReCaptcha.V2.Secret"] = "6LeIxAcTAAAAAGG-vFI1TnRWxMZNFuojJ4WifJWe";

            var result = siteVerifyService.SiteVerifyAsync("").ConfigureAwait(false).GetAwaiter().GetResult();

            result.ErrorCodes.Should().BeNull();
            result.ChallengeTs.Should().NotBeNullOrEmpty();
            result.Hostname.Should().Be("testkey.google.com");
            result.Success.Should().BeTrue();
        }

    }
}
