namespace Habitat.Accounts.Tests
{
  using System;
  using System.Collections.Generic;
  using System.Web.Security;
  using FluentAssertions;
  using Habitat.Accounts.Models;
  using Habitat.Accounts.Repositories;
  using Habitat.Accounts.Tests.Extensions;
  using NSubstitute;
  using Ploeh.AutoFixture;
  using Sitecore.Collections;
  using Sitecore.Common;
  using Sitecore.FakeDb.Security.Accounts;
  using Sitecore.FakeDb.Security.Web;
  using Sitecore.FakeDb.Sites;
  using Sitecore.Security.Accounts;
  using Sitecore.Security.Authentication;
  using Sitecore.Security.Domains;
  using Xunit;

  public class AccountsRepositoryTests
  {
    [Theory]
    [AutoDbData]
    public void RestorePasswordShouldCallResetPassword(FakeMembershipUser user, MembershipProvider membershipProvider, AccountRepository repo)
    {
      user.ProviderName.Returns("fake");
      membershipProvider.ResetPassword(Arg.Any<string>(), Arg.Any<string>()).Returns("new password");
      membershipProvider.Name.Returns("name");
      membershipProvider.GetUser(Arg.Any<string>(), Arg.Any<bool>()).Returns(user);

      using (new MembershipSwitcher(membershipProvider))
      {
        repo.RestorePassword(@"extranet\John");
        membershipProvider.Received(1).ResetPassword(Arg.Any<string>(), Arg.Any<string>());
      }
    }


    [Theory]
    [AutoDbData]
    public void RestorePasswordShouldReturnsNewPassword(FakeMembershipUser user, MembershipProvider membershipProvider, AccountRepository repo)
    {
      user.ProviderName.Returns("fake");
      membershipProvider.ResetPassword(Arg.Any<string>(), Arg.Any<string>()).Returns("new password");
      membershipProvider.Name.Returns("fake");
      membershipProvider.GetUser(Arg.Any<string>(), Arg.Any<bool>()).Returns(user);

      using (new MembershipSwitcher(membershipProvider))
      {
        repo.RestorePassword(@"extranet\John").Should().Be("new password");
      }
    }

    [Theory]
    [AutoDbData]
    public void ExistsShouldReturnTrueIfUserExists(FakeMembershipUser user, MembershipProvider membershipProvider, AccountRepository repo)
    {
      membershipProvider.GetUser(@"somedomain\John", Arg.Any<bool>()).Returns(user);

      var context = new FakeSiteContext(new StringDictionary
      {
        {
          "domain", "somedomain"
        }
      });
      using (new Switcher<Domain, Domain>(new Domain("somedomain")))
      {
        using (new MembershipSwitcher(membershipProvider))
        {
          var exists = repo.Exists("John");
          exists.Should().BeTrue();
        }
      }
    }

    [Theory]
    [AutoDbData]
    public void ExistsShouldReturnFalseIfUserNotExists(FakeMembershipUser user, MembershipProvider membershipProvider, AccountRepository repo)
    {
      membershipProvider.GetUser(Arg.Any<string>(), Arg.Any<bool>()).Returns((MembershipUser)null);
      membershipProvider.GetUser(@"somedomain\John", Arg.Any<bool>()).Returns(user);

      var context = new FakeSiteContext(new StringDictionary
      {
        {
          "domain", "somedomain"
        }
      });
      using (new Switcher<Domain, Domain>(new Domain("somedomain")))
      {
        using (new MembershipSwitcher(membershipProvider))
        {
          var exists = repo.Exists("Smith");
          exists.Should().BeFalse();
        }
      }
    }

    public static IEnumerable<object[]> RegistrationInfosArgumentNull
    {
      get
      {
        var fixture = new Fixture();
        return new List<object[]>
        {
          new RegistrationInfo[]
          {
            null
          },
          new[]
          {
            fixture.Build<RegistrationInfo>().With(x => x.Email, null).Create()
          },
          new[]
          {
            fixture.Build<RegistrationInfo>().With(x => x.Password, null).Create()
          },
          new[]
          {
            fixture.Build<RegistrationInfo>().With(x => x.ConfirmPassword, null).Create()
          }
        };
      }
    }

    [Theory]
    [MemberData(nameof(RegistrationInfosArgumentNull))]
    public void RegisterShouldThrowArgumentException(RegistrationInfo registrationInfo)
    {
      var repository = new AccountRepository();
      repository.Invoking(x => x.RegisterUser(registrationInfo)).ShouldThrow<ArgumentNullException>();
    }

    [Theory]
    [AutoDbData]
    public void RegisterShouldCreateUserWithEmailAndPassword(FakeMembershipUser user, MembershipProvider membershipProvider, RegistrationInfo registrationInfo, AccountRepository repository)
    {
      user.ProviderName.Returns("fake");
      user.UserName.Returns("name");
      MembershipCreateStatus status;
      membershipProvider.CreateUser(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<bool>(), Arg.Any<object>(), out status).Returns(user);
      membershipProvider.GetUser(Arg.Any<string>(), Arg.Any<bool>()).Returns(user);

      using (new Switcher<Domain, Domain>(new Domain("somedomain")))
      {
        using (new MembershipSwitcher(membershipProvider))
        {
          repository.RegisterUser(registrationInfo);
          membershipProvider.Received(1).CreateUser($@"somedomain\{registrationInfo.Email}", registrationInfo.Password, Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<bool>(), Arg.Any<object>(), out status);
        }
      }
    }

    [Theory]
    [AutoDbData]
    public void RegisterShouldCreateLoginUser(FakeMembershipUser user, MembershipProvider membershipProvider, AuthenticationProvider authenticationProvider, RegistrationInfo registrationInfo, AccountRepository repository)
    {
      user.ProviderName.Returns("fake");
      user.UserName.Returns("name");
      MembershipCreateStatus status;
      membershipProvider.CreateUser(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<bool>(), Arg.Any<object>(), out status).Returns(user);
      membershipProvider.GetUser(Arg.Any<string>(), Arg.Any<bool>()).Returns(user);

      using (new Switcher<Domain, Domain>(new Domain("somedomain")))
      {
        using (new MembershipSwitcher(membershipProvider))
        {
          using (new AuthenticationSwitcher(authenticationProvider))
          {
            repository.RegisterUser(registrationInfo);
            authenticationProvider.Received(1).Login(Arg.Is<User>(u => u.Name == $@"somedomain\{registrationInfo.Email}"));
          }
        }
      }
    }
  }
}