using System;
using System.Collections.Generic;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Security;
using FluentAssertions;
using Habitat.Accounts.Controllers;
using Habitat.Accounts.Models;
using Habitat.Accounts.Repositories;
using Moq;
using Ploeh.AutoFixture;
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
using Sitecore.Security.Accounts;
using Sitecore.Security.Authentication;
using Sitecore.Security.Domains;
using Sitecore.Shell.Framework.Commands.Masters;
using Sitecore.Sites;
using Xunit;
using Xunit.Extensions;

namespace Habitat.Accounts.Tests
{
  public class AccountsRepositoryTests
  {
    [Theory]
    [AutoDbData]
    public void RestorePasswordShouldReturnsNewPassword(Mock<FakeMembershipUser> user, Mock<MembershipProvider> membershipProvider, AccountRepository repo)
    {
      user.Setup(x => x.ProviderName).Returns("fake");
      membershipProvider.Setup(x => x.ResetPassword(It.IsAny<string>(), It.IsAny<string>())).Returns("new password");
      membershipProvider.Setup(x => x.Name).Returns("name");
      membershipProvider.Setup(x => x.GetUser(It.IsAny<string>(), It.IsAny<bool>())).Returns(user.Object);

      using (new Sitecore.FakeDb.Security.Web.MembershipSwitcher(membershipProvider.Object))
      {
        repo.RestorePassword(@"extranet\John").Should().Be("new password");
      }
    }

    [Theory, AutoDbData]
    public void ExistsShouldReturnTrueIfUserExists(FakeMembershipUser user, Mock<MembershipProvider> membershipProvider, AccountRepository repo)
    {
      membershipProvider.Setup(x => x.GetUser(@"somedomain\John", It.IsAny<bool>())).Returns(user);

      var context = new FakeSiteContext(new StringDictionary
      {
        {"domain","somedomain" }
      });
      using (new Switcher<Domain, Domain>(new Domain("somedomain")))
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

    public static IEnumerable<object[]> RegistrationInfosArgumentNull
    {
      get
      {
        var fixture = new Fixture();
        return new List<object[]>()
        {
            new RegistrationInfo[]{null},
            new []{fixture.Build<RegistrationInfo>().With(x=>x.Email,null).Create()},
            new []{fixture.Build<RegistrationInfo>().With(x=>x.Password,null).Create()},
            new []{fixture.Build<RegistrationInfo>().With(x=>x.ConfirmPassword,null).Create()},
        };
      }
    }

    [Theory, MemberData(nameof(RegistrationInfosArgumentNull))]
    public void RegisterShouldThrowArgumentException(RegistrationInfo registrationInfo)
    {
      var repository = new AccountRepository();
      repository.Invoking(x => x.RegisterUser(registrationInfo)).ShouldThrow<ArgumentNullException>();
    }

    [Theory, AutoDbData]
    public void RegisterShouldCreateUserWithEmailAndPassword(Mock<FakeMembershipUser> user, Mock<MembershipProvider> membershipProvider, Mock<AuthenticationProvider> authenticationProvider, RegistrationInfo registrationInfo, AccountRepository repository)
    {
      user.Setup(x => x.ProviderName).Returns("fake");
      user.Setup(x => x.UserName).Returns("name");
      MembershipCreateStatus status;
      membershipProvider.Setup(x => x.CreateUser(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<object>(), out status)).Returns(user.Object);
      membershipProvider.Setup(x => x.GetUser(It.IsAny<string>(), It.IsAny<bool>())).Returns(user.Object);

      using (new Switcher<Domain, Domain>(new Domain("somedomain")))
      using (new Sitecore.FakeDb.Security.Web.MembershipSwitcher(membershipProvider.Object))
      using (new Sitecore.Security.Authentication.AuthenticationSwitcher(authenticationProvider.Object))
      {
        repository.RegisterUser(registrationInfo);
        membershipProvider.Verify(x => x.CreateUser($@"somedomain\{registrationInfo.Email}", registrationInfo.Password, It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<object>(), out status), Times.Once());
      }
    }

    [Theory, AutoDbData]
    public void RegisterShouldCreateLoginUser(Mock<FakeMembershipUser> user, Mock<MembershipProvider> membershipProvider, Mock<AuthenticationProvider> authenticationProvider, RegistrationInfo registrationInfo, AccountRepository repository)
    {
      user.Setup(x => x.ProviderName).Returns("fake");
      user.Setup(x => x.UserName).Returns("name");
      MembershipCreateStatus status;
      membershipProvider.Setup(x => x.CreateUser(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<object>(), out status)).Returns(user.Object);
      membershipProvider.Setup(x => x.GetUser(It.IsAny<string>(), It.IsAny<bool>())).Returns(user.Object);

      using (new Switcher<Domain, Domain>(new Domain("somedomain")))
      using (new Sitecore.FakeDb.Security.Web.MembershipSwitcher(membershipProvider.Object))
      using (new Sitecore.Security.Authentication.AuthenticationSwitcher(authenticationProvider.Object))
      {
        repository.RegisterUser(registrationInfo);
        authenticationProvider.Verify(x => x.Login(It.Is<User>(u=>u.Name == $@"somedomain\{registrationInfo.Email}")));
      }
    }
  }
}