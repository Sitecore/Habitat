namespace Sitecore.Feature.Accounts.Tests.Attributes
{
  using System.Web.Mvc;
  using FluentAssertions;
  using NSubstitute;
  using Ploeh.AutoFixture.AutoNSubstitute;
  using Ploeh.AutoFixture.Xunit2;
  using Sitecore.Collections;
  using Sitecore.Data;
  using Sitecore.Data.Items;
  using Sitecore.FakeDb;
  using Sitecore.FakeDb.AutoFixture;
  using Sitecore.FakeDb.Sites;
  using Sitecore.Feature.Accounts.Attributes;
  using Sitecore.Feature.Accounts.Services;
  using Sitecore.Feature.Accounts.Tests.Extensions;
  using Sitecore.Foundation.Testing.Attributes;
  using Sitecore.Sites;
  using Xunit;

  public class AccountsRedirectAuthenticatedAttributeTests
  {
    [Theory]
    [AutoDbData]
    public void OnActionExecuting_AuthenticatedUser_ShouldRedirectToAfterLoginPage(Database db, [Content] DbItem item, string afterLoginLink, [Frozen]IAccountsSettingsService accountsSettingsService, [Substitute]ActionExecutingContext filterContext, [Greedy]AccountsRedirectAuthenticatedAttribute redirectAuthenticatedAttribute)
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

      accountsSettingsService.GetPageLinkOrDefault(Arg.Any<Item>(), Templates.AccountsSettings.Fields.AfterLoginPage, Arg.Any<Item>()).Returns(afterLoginLink);
      //Act
      using (new SiteContextSwitcher(siteContext))
      using (new Sitecore.Security.Accounts.UserSwitcher(@"extranet\John", true))
      {
        redirectAuthenticatedAttribute.OnActionExecuting(filterContext);
      }

      //Assert      
      filterContext.Result.Should().BeOfType<RedirectResult>().Which.Url.Should().Be(afterLoginLink);
    }

    [Theory]
    [AutoDbData]
    public void OnActionExecuting_RedirectEqualsCurrent_ShouldRedirectToRootPage(Database db, [Content] DbItem item, string afterLoginLink, [Frozen]IAccountsSettingsService accountsSettingsService, [Substitute]ActionExecutingContext filterContext, [Greedy]AccountsRedirectAuthenticatedAttribute redirectAuthenticatedAttribute)
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

      accountsSettingsService.GetPageLinkOrDefault(Arg.Any<Item>(), Templates.AccountsSettings.Fields.AfterLoginPage, Arg.Any<Item>()).Returns(afterLoginLink);
      filterContext.HttpContext.Request.RawUrl.Returns(afterLoginLink);

      //Act
      using (new SiteContextSwitcher(siteContext))
      using (new Sitecore.Security.Accounts.UserSwitcher(@"extranet\John", true))
      {
        redirectAuthenticatedAttribute.OnActionExecuting(filterContext);
      }

      //Assert      
      filterContext.Result.Should().BeOfType<RedirectResult>().Which.Url.Should().NotBe(afterLoginLink);
    }
  }
}
