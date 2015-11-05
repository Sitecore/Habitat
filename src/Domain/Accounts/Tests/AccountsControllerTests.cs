using System.Web.Mvc;
using FluentAssertions;
using Habitat.Accounts.Controllers;
using Habitat.Accounts.Repositories;
using Moq;
using Sitecore;
using Sitecore.Collections;
using Sitecore.Data;
using Sitecore.FakeDb;
using Sitecore.FakeDb.AutoFixture;
using Sitecore.FakeDb.Sites;
using Sitecore.Globalization;
using Sitecore.Sites;
using Xunit;

namespace Habitat.Accounts.Tests
{
  public class AccountsControllerTests
  {
    [Theory]
    [AutoDbData]
    public void LogoutShouldCallSitecoreLogout(Database db, [Content]DbItem item, Mock<IAccountRepository> repo)
    {

      var fakeSite = new FakeSiteContext(new StringDictionary
      {
        {"rootPath", "/sitecore/content"},
        {"startItem", item.Name}
      }) as SiteContext;
      fakeSite.Database = db;
      using (new SiteContextSwitcher(fakeSite))
      {
        var ctrl = new AccountsController(repo.Object);
        ctrl.Logout();
        repo.Verify(x => x.Logout(), Times.Once());
      }
    }

    [Theory]
    [AutoDbData]
    public void LogoutShouldRedirectUserToHomePage(Database db, [Content]DbItem item, Mock<IAccountRepository> repo)
    {
      var fakeSite = new FakeSiteContext(new StringDictionary
      {
        {"rootPath", "/sitecore/content"},
        {"startItem", item.Name}
      }) as SiteContext;
      fakeSite.Database = db;
      Language.Current = Language.Invariant;

      using (new SiteContextSwitcher(fakeSite))
      {
        var ctrl = new AccountsController(repo.Object);
        var result = ctrl.Logout();
        result.Should().BeOfType<RedirectResult>();
        (result as RedirectResult).Url.Should().Be("/");
      }
    }
  }
}