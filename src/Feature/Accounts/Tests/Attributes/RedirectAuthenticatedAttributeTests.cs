namespace Sitecore.Feature.Accounts.Tests.Attributes
{
  using System.Reflection;
  using System.Web.Mvc;
  using FluentAssertions;
  using NSubstitute;
  using Ploeh.AutoFixture.AutoNSubstitute;
  using Sitecore.Collections;
  using Sitecore.Data;
  using Sitecore.FakeDb;
  using Sitecore.FakeDb.AutoFixture;
  using Sitecore.FakeDb.Sites;
  using Sitecore.Feature.Accounts.Attributes;
  using Sitecore.Feature.Accounts.Services;
  using Sitecore.Feature.Accounts.Tests.Extensions;
  using Sitecore.Foundation.Testing.Attributes;
  using Sitecore.Sites;
  using Xunit;

  public class RedirectAuthenticatedAttributeTests
  {
    [Theory]
    [AutoDbData]
    public void OnActionExecuting_NotNormalMode_ShouldNotRedirect(FakeSiteContext siteContext, [Substitute]ActionExecutingContext filterContext)
    {
      //Arrange
      typeof(SiteContext).GetField("displayMode", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(siteContext, DisplayMode.Edit);
      var urlService = Substitute.For<IGetRedirectUrlService>();
      var redirectAuthenticatedAttribute = new RedirectAuthenticatedAttribute(urlService);

      //Act
      using (new SiteContextSwitcher(siteContext))
      {
        redirectAuthenticatedAttribute.OnActionExecuting(filterContext);
      }

      //Assert
      filterContext.Result.Should().BeNull();
    }

    [Theory]
    [AutoDbData]
    public void OnActionExecuting_NotAuthenticatedUser_ShouldNotRedirect(FakeSiteContext siteContext, [Substitute]ActionExecutingContext filterContext)
    {
      //Arrange
      typeof(SiteContext).GetField("displayMode", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(siteContext, DisplayMode.Normal);
      var urlService = Substitute.For<IGetRedirectUrlService>();
      var redirectAuthenticatedAttribute = new RedirectAuthenticatedAttribute(urlService);
      
      //Act
      using (new SiteContextSwitcher(siteContext))
      using (new Sitecore.Security.Accounts.UserSwitcher(@"extranet\John", false))
      {
        redirectAuthenticatedAttribute.OnActionExecuting(filterContext);
      }

      //Assert
      filterContext.Result.Should().BeNull();
    }

    [Theory]
    [AutoDbData]
    public void OnActionExecuting_AuthenticatedUser_ShouldRedirect(Database db, [Content] DbItem item, [Substitute]ActionExecutingContext filterContext, string url)
    {
      //Arrange
      var siteContext = new FakeSiteContext(new StringDictionary
      {
        {
          "rootPath", "/sitecore/content"
        },
        {
          "startItem", item.Name
        }
      }) as SiteContext;
      siteContext.Database = db;
      var urlService = Substitute.For<IGetRedirectUrlService>();
      urlService.GetRedirectUrl(AuthenticationStatus.Authenticated).Returns(url);
      var redirectAuthenticatedAttribute = new RedirectAuthenticatedAttribute(urlService);

      //Act
      using (new SiteContextSwitcher(siteContext))
      using (new Sitecore.Security.Accounts.UserSwitcher(@"extranet\John", true))
      {
        redirectAuthenticatedAttribute.OnActionExecuting(filterContext);
      }

      //Assert
      filterContext.Result.Should().BeOfType<RedirectResult>();
      ((RedirectResult)filterContext.Result).Url.Should().Be(url);
    }
  }
}
