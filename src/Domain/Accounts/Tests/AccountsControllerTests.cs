using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Security;
using FluentAssertions;
using Habitat.Accounts.Controllers;
using Habitat.Accounts.Models;
using Habitat.Accounts.Repositories;
using Habitat.Accounts.Texts;
using Moq;
using Ploeh.AutoFixture.Xunit2;
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
        result.Should().BeOfType<RedirectResult>().Which.Url.Should().Be("/");
      }
    }

    [Theory, AutoDbData]
    public void RegisterShouldReturnViewWithoudModel([Frozen] Mock<IAccountRepository> repo, [NoAutoProperties] AccountsController controller)
    {
      var result = controller.Register();
      result.Should().BeOfType<ViewResult>().Which.Model.Should().BeNull();
    }

    [Theory, AutoDbData]
    public void RegisterShouldRedirectToHomePageIfUserLoggedIn(Database db, [Content] DbItem item, RegistrationInfo registrationInfo, [Frozen]Mock<IAccountRepository> repo, [NoAutoProperties]AccountsController controller)
    {
      var fakeSite = new FakeSiteContext(new StringDictionary
      {
        {"rootPath", "/sitecore/content"},
        {"startItem", item.Name}
      }) as SiteContext;
      fakeSite.Database = db;
      Language.Current = Language.Invariant;

      using (new SiteContextSwitcher(fakeSite))
      using (new Sitecore.Security.Accounts.UserSwitcher(@"extranet\John", true))
      {
        var result = controller.Register(registrationInfo);
        result.Should().BeOfType<RedirectResult>().Which.Url.Should().Be("/");
      }
    }

    [Theory, AutoDbData]
    public void RegisterShouldReturnModelIfItsNotValid(RegistrationInfo registrationInfo, [Frozen]Mock<IAccountRepository> repo, [NoAutoProperties]AccountsController controller)
    {
      controller.ModelState.AddModelError("Error", "Error");

      var result = controller.Register(registrationInfo);
      result.Should().BeOfType<ViewResult>().Which.Model.Should().Be(registrationInfo);
    }

    [Theory, AutoDbData]
    public void RegisterShouldReturnModelWithErrorIfSameUserExists(RegistrationInfo registrationInfo, [Frozen]Mock<IAccountRepository> repo, [NoAutoProperties]AccountsController controller)
    {
      using (new Sitecore.Security.Accounts.UserSwitcher($@"extranet\{registrationInfo.Email}", false))
      {
        var result = controller.Register(registrationInfo);
        result.Should().BeOfType<ViewResult>().Which.Model.Should().Be(registrationInfo);
        result.Should().BeOfType<ViewResult>().Which.ViewData.ModelState.Should().ContainKey(nameof(registrationInfo.Email))
          .WhichValue.Errors.Should().Contain(x => x.ErrorMessage == Errors.UserAlreadyExists);
      }
    }

    [Theory, AutoDbData]
    public void RegisterShouldReturnErrorIfRegistrationThrowsMembershipException(RegistrationInfo registrationInfo, MembershipCreateUserException exception, [Frozen]Mock<IAccountRepository> repo)
    {
      repo.Setup(x => x.RegisterUser(It.IsAny<RegistrationInfo>())).Throws(exception);
      var controller = new AccountsController(repo.Object);

      var result = controller.Register(registrationInfo);
      result.Should().BeOfType<ViewResult>().Which.Model.Should().Be(registrationInfo);
      result.Should().BeOfType<ViewResult>().Which.ViewData.ModelState.Should().ContainKey(nameof(registrationInfo.Email))
        .WhichValue.Errors.Should().Contain(x => x.ErrorMessage == exception.Message);
    }

    [Theory, AutoDbData]
    public void RegisterShouldCallRegisterUserAndRedirectToHomePage(Database db, [Content] DbItem item, RegistrationInfo registrationInfo, [Frozen]Mock<IAccountRepository> repo)
    {
      repo.Setup(x => x.Exists(It.IsAny<string>())).Returns(false);
      var controller = new AccountsController(repo.Object);

      var fakeSite = new FakeSiteContext(new StringDictionary
      {
        {"rootPath", "/sitecore/content"},
        {"startItem", item.Name}
      }) as SiteContext;
      fakeSite.Database = db;
      Language.Current = Language.Invariant;

      using (new SiteContextSwitcher(fakeSite))
      {
        var result = controller.Register(registrationInfo);
        result.Should().BeOfType<RedirectResult>().Which.Url.Should().Be("/");

        repo.Verify(x=>x.RegisterUser(registrationInfo),Times.Once());
      }
    }
  }
}