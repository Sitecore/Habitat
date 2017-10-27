using System.Threading.Tasks;
using Sitecore.Foundation.ReCaptcha.Models;
using Sitecore.Foundation.ReCaptcha.Services;
using NSubstitute;
using Xunit;
using FluentAssertions;

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

    }
}
