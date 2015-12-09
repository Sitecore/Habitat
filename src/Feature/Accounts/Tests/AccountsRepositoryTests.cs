namespace Sitecore.Feature.Accounts.Tests
{
  using System;
  using System.Collections.Generic;
  using System.Web.Security;
  using FluentAssertions;
  using NSubstitute;
  using Ploeh.AutoFixture;
  using Ploeh.AutoFixture.AutoNSubstitute;
  using Ploeh.AutoFixture.Xunit2;
  using Sitecore.Collections;
  using Sitecore.Common;
  using Sitecore.FakeDb.Security.Accounts;
  using Sitecore.FakeDb.Security.Web;
  using Sitecore.FakeDb.Sites;
  using Sitecore.Feature.Accounts.Models;
  using Sitecore.Feature.Accounts.Repositories;
  using Sitecore.Feature.Accounts.Services;
  using Sitecore.Feature.Accounts.Tests.Extensions;
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

    [Theory]
    [AutoDbData]
    public void LoginShouldReturnTrueIfUserIsLoggedIn(FakeMembershipUser user, AuthenticationProvider authenticationProvider, AccountRepository repo)
    {
      authenticationProvider.Login(@"somedomain\John", Arg.Any<string>(), Arg.Any<bool>()).Returns(true);

      var context = new FakeSiteContext(new StringDictionary
      {
        {
          "domain", "somedomain"
        }
      });
      using (new Switcher<Domain, Domain>(new Domain("somedomain")))
      {
        using (new AuthenticationSwitcher(authenticationProvider))
        {
          var loginResult = repo.Login("John", "somepassword");
          loginResult.Should().BeTrue();
        }
      }
    }

    [Theory]
    [AutoDbData]
    public void LoginShouldTriggerLoginEventIfUserIsLoggedIn(FakeMembershipUser user, [Frozen]IAccountTrackerService accountTrackerService, AuthenticationProvider authenticationProvider, AccountRepository repo)
    {
      authenticationProvider.Login(@"somedomain\John", Arg.Any<string>(), Arg.Any<bool>()).Returns(true);

      var context = new FakeSiteContext(new StringDictionary
      {
        {
          "domain", "somedomain"
        }
      });
      using (new Switcher<Domain, Domain>(new Domain("somedomain")))
      {
        using (new AuthenticationSwitcher(authenticationProvider))
        {
          var loginResult = repo.Login("John", "somepassword");
          accountTrackerService.Received(1).TrackLogin("somedomain\\John");
        }
      }
    }

    [Theory]
    [AutoDbData]
    public void LoginShouldReturnFalseIfUserIsNotLoggedIn(FakeMembershipUser user, AuthenticationProvider authenticationProvider, AccountRepository repo)
    {
      authenticationProvider.Login(@"somedomain\John", Arg.Any<string>(), Arg.Any<bool>()).Returns(false);

      var context = new FakeSiteContext(new StringDictionary
      {
        {
          "domain", "somedomain"
        }
      });
      using (new Switcher<Domain, Domain>(new Domain("somedomain")))
      {
        using (new AuthenticationSwitcher(authenticationProvider))
        {
          var loginResult = repo.Login("John", "somepassword");
          loginResult.Should().BeFalse();
        }
      }
    }

    [Theory]
    [AutoDbData]
    public void LoginShouldNotTrackLoginEventIfUserIsNotLoggedIn(FakeMembershipUser user, [Frozen]IAccountTrackerService accountTrackerService, AuthenticationProvider authenticationProvider, AccountRepository repo)
    {
      authenticationProvider.Login(@"somedomain\John", Arg.Any<string>(), Arg.Any<bool>()).Returns(false);

      var context = new FakeSiteContext(new StringDictionary
      {
        {
          "domain", "somedomain"
        }
      });
      using (new Switcher<Domain, Domain>(new Domain("somedomain")))
      {
        using (new AuthenticationSwitcher(authenticationProvider))
        {
          var loginResult = repo.Login("John", "somepassword");
          accountTrackerService.DidNotReceive().TrackLogin(Arg.Any<string>());
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
          new[]
          {
            null,fixture.Create<string>(),fixture.Create<string>()
          },
          new[]
          {
            fixture.Create<string>(),null,fixture.Create<string>()
          }
        };
      }
    }

    [Theory]
    [MemberData(nameof(RegistrationInfosArgumentNull))]
    public void RegisterShouldThrowArgumentException(string email, string password, string profileId)
    {
      var repository = new AccountRepository(Substitute.For<IAccountTrackerService>());
      repository.Invoking(x => x.RegisterUser(email,password, profileId)).ShouldThrow<ArgumentNullException>();
    }

    [Theory]
    [AutoDbData]
    public void RegisterShouldCreateUserWithEmailAndPassword(FakeMembershipUser user, MembershipProvider membershipProvider, RegistrationInfo registrationInfo, string userProfile, AccountRepository repository)
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
          repository.RegisterUser(registrationInfo.Email,registrationInfo.Password, userProfile);
          membershipProvider.Received(1).CreateUser($@"somedomain\{registrationInfo.Email}", registrationInfo.Password, Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<bool>(), Arg.Any<object>(), out status);
        }
      }
    }

    [Theory]
    [AutoDbData]
    public void RegisterShouldCreateLoginUser(FakeMembershipUser user, [Substitute] MembershipProvider membershipProvider, [Substitute] AuthenticationProvider authenticationProvider, RegistrationInfo registrationInfo, AccountRepository repository, string profileId)
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
            repository.RegisterUser(registrationInfo.Email, registrationInfo.Password, profileId);
            authenticationProvider.Received(1).Login(Arg.Is<string>(u => u == $@"somedomain\{registrationInfo.Email}"), Arg.Is<string>(p=>p== registrationInfo.Password), Arg.Any<bool>());
          }
        }
      }
    }

    [Theory]
    [AutoDbData]
    public void RegisterShouldTrackLoginAndRegisterEvents(FakeMembershipUser user, [Substitute]MembershipProvider membershipProvider, [Substitute]AuthenticationProvider authenticationProvider, RegistrationInfo registrationInfo, [Frozen]IAccountTrackerService accountTrackerService, AccountRepository repository, string profileId)
    {
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
            repository.RegisterUser(registrationInfo.Email, registrationInfo.Password, profileId);
            accountTrackerService.Received(1).TrackRegister();
          }
        }
      }
    }

    [Theory]
    [AutoDbData]
    public void LogoutShouldLogoutUser(User user, MembershipProvider membershipProvider, RegistrationInfo registrationInfo, AccountRepository repository)
    {
      var authenticationProvider = Substitute.For<AuthenticationProvider>();
      authenticationProvider.GetActiveUser().Returns(user);
      using (new AuthenticationSwitcher(authenticationProvider))
      {
          repository.Logout();
          authenticationProvider.Received(1).Logout();
      }
    }
  }
}