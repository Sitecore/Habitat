using System.Web.Mvc;
using System.Web.Security;
using FluentAssertions;
using Habitat.Accounts.Controllers;
using Habitat.Accounts.Repositories;
using Moq;
using Sitecore;
using Sitecore.Collections;
using Sitecore.Common;
using Sitecore.Data;
using Sitecore.Data.Proxies;
using Sitecore.FakeDb;
using Sitecore.FakeDb.AutoFixture;
using Sitecore.FakeDb.Security.Accounts;
using Sitecore.FakeDb.Sites;
using Sitecore.Globalization;
using Sitecore.Security.Domains;
using Sitecore.Shell.Framework.Commands.Masters;
using Sitecore.Sites;
using Xunit;

namespace Habitat.Accounts.Tests
{
  public class AccountsRepositoryTests
  {
    [Theory]
    [AutoDbData]
    public void RestorePasswordShouldReturnsNewPassword(Mock<FakeMembershipUser> user, Mock<MembershipProvider> membershipProvider, AccountRepository repo)
    {
      user.Setup(x => x.ProviderName).Returns("fake");
      membershipProvider.Setup(x=>x.ResetPassword(It.IsAny<string>(), It.IsAny<string>())).Returns("new password");
      membershipProvider.Setup(x=>x.Name).Returns("name");
      membershipProvider.Setup(x=>x.GetUser(It.IsAny<string>(), It.IsAny<bool>())).Returns(user.Object);

      using (new Sitecore.FakeDb.Security.Web.MembershipSwitcher(membershipProvider.Object))
      {
        repo.RestorePassword(@"extranet\John").Should().Be("new password");
      }
    }

    [Theory, AutoDbData]
    public void ExistsShouldReturnTrueIfUserExists(FakeMembershipUser user, Mock<MembershipProvider> membershipProvider, AccountRepository repo)
    {
      membershipProvider.Setup(x=>x.GetUser(@"somedomain\John", It.IsAny<bool>())).Returns(user);
      
      var context = new FakeSiteContext(new StringDictionary
      {
        {"domain","somedomain" }
      });
      using(new Switcher<Domain,Domain>(new Domain("somedomain")))
      using (new Sitecore.FakeDb.Security.Web.MembershipSwitcher(membershipProvider.Object))
      {
        var exists = repo.Exists("John");
        exists.Should().BeTrue();
      }
    }

    [Theory, AutoDbData]
    public void ExistsShouldReturnFalseIfUserNotExists(FakeMembershipUser user, Mock<MembershipProvider> membershipProvider, AccountRepository repo)
    {
      membershipProvider.Setup(x => x.GetUser(@"somedomain\John", It.IsAny<bool>())).Returns(user);

      var context = new FakeSiteContext(new StringDictionary
      {
        {"domain","somedomain" }
      });
      using (new Switcher<Domain, Domain>(new Domain("somedomain")))
      using (new Sitecore.FakeDb.Security.Web.MembershipSwitcher(membershipProvider.Object))
      {
        var exists = repo.Exists("Smith");
        exists.Should().BeFalse();
      }
    }

  }
}