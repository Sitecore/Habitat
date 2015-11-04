using System.Web.Mvc;
using FluentAssertions;
using Habitat.Accounts.Controllers;
using Habitat.Accounts.Repositories;
using Moq;
using Sitecore;
using Sitecore.Collections;
using Sitecore.Data;
using Sitecore.FakeDb;
using Sitecore.FakeDb.Sites;
using Sitecore.Sites;
using Xunit;

namespace Habitat.Accounts.Tests
{
  public class AccountControllerTests
  {
    [Theory]
    [AutoDbData]
    public void LogoutShouldCallSitecoreLogout(Db db, Mock<IAccountRepository> repo)
    {
      var fakeSite = new FakeSiteContext(new StringDictionary
      {
        {"rootPath", "root"},
        {"startItem", "item"}
      });
      using (new SiteContextSwitcher(fakeSite))
      {
        var ctrl = new AccountsController(repo.Object);
        ctrl.Logout();
        repo.Verify(x => x.Logout(), Times.Once());
      }
    }

    [Theory]
    [AutoDbData]
    public void LogoutShouldRedirectUserToHomePage(DbItem item, Database db, Mock<IAccountRepository> repo)
    {
      var fakeSite = new FakeSiteContext(new StringDictionary
      {
        {"rootPath", "root"},
        {"startItem", "item"}
      });

      using (new SiteContextSwitcher(fakeSite))
      {
        var ctrl = new AccountsController(repo.Object);
        var result = ctrl.Logout();
        result.Should().BeOfType<RedirectResult>();
        (result as RedirectResult).Url.Should().Be(Context.Site.StartPath);
      }
    }
  }
}