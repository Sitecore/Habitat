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
            var siteverifyService = Substitute.For<ISiteverifyService>();

            var response = new ReCaptchaResponseModel
            {
                ErrorCodes = { },
                Success = true
            };

            siteverifyService.SiteVerifyAsync("").Returns(Task.FromResult(response));

            var result = await siteverifyService.SiteVerifyAsync("");

            result.ErrorCodes.Should().BeNull();
            result.Success.Should().BeTrue();
        }

        [Fact]
        public async void Error_Codes_Should_Not_Be_Empty()
        {
            var siteverifyService = Substitute.For<ISiteverifyService>();

            var response = new ReCaptchaResponseModel
            {
                ErrorCodes = new[] { "" },
                Success = false
            };

            siteverifyService.SiteVerifyAsync("").Returns(Task.FromResult(response));

            var result = await siteverifyService.SiteVerifyAsync("");

            result.ErrorCodes.Should().NotBeEmpty();
            result.Success.Should().BeFalse();
        }

        [Theory]
        [AutoDbData]
        public void Error_Success_Should_Be_True_Hostname_testkey(Db db)
        {
            ISiteverifyService siteverifyService = new SiteverifyService();

            db.Configuration.Settings["Foundation.ReCaptcha.V2.SiteKey"] = "6LeIxAcTAAAAAJcZVRqyHh71UMIEGNQ_MXjiZKhI";
            db.Configuration.Settings["Foundation.ReCaptcha.V2.Secret"] = "6LeIxAcTAAAAAGG-vFI1TnRWxMZNFuojJ4WifJWe";

            var result = siteverifyService.SiteVerifyAsync("").ConfigureAwait(false).GetAwaiter().GetResult();

            result.ErrorCodes.Should().BeNull();
            result.ChallengeTs.Should().NotBeNullOrEmpty();
            result.Hostname.Should().Be("testkey.google.com");
            result.Success.Should().BeTrue();
        }

    }
}
