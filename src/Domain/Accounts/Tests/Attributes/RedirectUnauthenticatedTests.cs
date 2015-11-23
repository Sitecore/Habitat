namespace Habitat.Accounts.Tests.Attributes
{
  using System.Web.Mvc;
  using FluentAssertions;
  using Habitat.Accounts.Attributes;
  using Habitat.Accounts.Tests.Extensions;
  using Ploeh.AutoFixture.AutoNSubstitute;
  using Sitecore.Collections;
  using Sitecore.Data;
  using Sitecore.FakeDb;
  using Sitecore.FakeDb.AutoFixture;
  using Sitecore.FakeDb.Sites;
  using Sitecore.Globalization;
  using Sitecore.Sites;
  using Xunit;

  public class RedirectUnauthenticatedTests
  {
    [Theory]
    [AutoDbData]
    public void ShouldNotRedirectAuthenticatedUser([Substitute]AuthorizationContext filterContext, RedirectUnauthenticatedAttribute redirectUnauthenticated)
    {
      using (new Sitecore.Security.Accounts.UserSwitcher(@"extranet\John", true))
      {
        redirectUnauthenticated.OnAuthorization(filterContext);
        filterContext.Result.Should().BeNull();
      }
    }

    [Theory]
    [AutoDbData]
    public void ShouldRedirectUnauthenticatedUser(Database db, [Content] DbItem item,[Substitute]AuthorizationContext filterContext, RedirectUnauthenticatedAttribute redirectUnauthenticated)
    {
      var fakeSite = new FakeSiteContext(new StringDictionary
      {
        {
          "rootPath", "/sitecore/content"
        },
        {
          "startItem", item.Name
        }
      }) as SiteContext;
      fakeSite.Database = db;
      Language.Current = Language.Invariant;

      using (new SiteContextSwitcher(fakeSite))
      using (new Sitecore.Security.Accounts.UserSwitcher(@"extranet\John", false))
      {
        redirectUnauthenticated.OnAuthorization(filterContext);
        filterContext.Result.Should().BeOfType<RedirectResult>().Which.Url.Should().Be("/");
      }
    }
  }
}
