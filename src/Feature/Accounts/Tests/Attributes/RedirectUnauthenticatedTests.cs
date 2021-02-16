namespace Sitecore.Feature.Accounts.Tests.Attributes
{
    using System.Web.Mvc;
    using FluentAssertions;
    using NSubstitute;
    using Ploeh.AutoFixture.AutoNSubstitute;
    using Sitecore.Feature.Accounts.Attributes;
    using Sitecore.Feature.Accounts.Services;
    using Sitecore.Foundation.Testing.Attributes;
    using Xunit;

    public class RedirectUnauthenticatedTests
  {
    [Theory]
    [AutoDbData]
    public void ShouldNotRedirectAuthenticatedUser([Substitute]AuthorizationContext filterContext)
    {
      var urlService = Substitute.For<IGetRedirectUrlService>();
      var redirectUnauthenticated = new RedirectUnauthenticatedAttribute(urlService);
      using (new Sitecore.Security.Accounts.UserSwitcher(@"extranet\John", true))
      {
        redirectUnauthenticated.OnAuthorization(filterContext);
        filterContext.Result.Should().BeNull();
      }
    }

    [Theory]
    [AutoDbData]
    public void ShouldRedirectUnauthenticatedUser([Substitute]AuthorizationContext filterContext, string url)
    {
      var urlService = Substitute.For<IGetRedirectUrlService>();
      urlService.GetRedirectUrl(AuthenticationStatus.Unauthenticated, Arg.Any<string>()).Returns(url);
      var redirectUnauthenticated = new RedirectUnauthenticatedAttribute(urlService);
      using (new Sitecore.Security.Accounts.UserSwitcher(@"extranet\John", false))
      {
        redirectUnauthenticated.OnAuthorization(filterContext);
        filterContext.Result.Should().BeOfType<RedirectResult>().Which.Url.Should().Be(url);
      }
    }
  }
}
