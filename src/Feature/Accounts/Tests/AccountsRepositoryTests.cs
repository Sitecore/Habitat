namespace Sitecore.Feature.Accounts.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Web.Security;
    using FluentAssertions;
    using NSubstitute;
    using NSubstitute.Extensions;
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
    using Sitecore.Foundation.Accounts.Pipelines;
    using Sitecore.Foundation.Testing.Attributes;
    using Sitecore.Security.Accounts;
    using Sitecore.Security.Authentication;
    using Sitecore.Security.Domains;
    using Xunit;

    public class AccountsRepositoryTests
    {
        [Theory]
        [AutoFakeUserData]
        public void RestorePassword_ValidUser_ShouldCallResetPassword(FakeMembershipUser user, MembershipProvider membershipProvider, AccountRepository repo)
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
        [AutoFakeUserData]
        public void RestorePassword_ValidUser_ShouldReturnsNewPassword(FakeMembershipUser user, MembershipProvider membershipProvider, AccountRepository repo)
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
        public void Exists_UserExists_ShouldReturnTrue(FakeMembershipUser user, MembershipProvider membershipProvider, AccountRepository repo)
        {
            membershipProvider.GetUser(@"somedomain\John", Arg.Any<bool>()).Returns(user);

            var context = new FakeSiteContext(new StringDictionary
                                              {
                                                  {"domain", "somedomain"}
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
        public void Exists_UserDoesNotExist_ShouldReturnFalse(FakeMembershipUser user, MembershipProvider membershipProvider, AccountRepository repo)
        {
            membershipProvider.GetUser(Arg.Any<string>(), Arg.Any<bool>()).Returns((MembershipUser)null);
            membershipProvider.GetUser(@"somedomain\John", Arg.Any<bool>()).Returns(user);

            var context = new FakeSiteContext(new StringDictionary
                                              {
                                                  {"domain", "somedomain"}
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
        public void Login_UserIsNotLoggedIn_ShouldReturnFalse(FakeMembershipUser user, AuthenticationProvider authenticationProvider, AccountRepository repo)
        {
            authenticationProvider.Login(@"somedomain\John", Arg.Any<string>(), Arg.Any<bool>()).Returns(false);

            var context = new FakeSiteContext(new StringDictionary
                                              {
                                                  {"domain", "somedomain"}
                                              });
            using (new Switcher<Domain, Domain>(new Domain("somedomain")))
            {
                using (new AuthenticationSwitcher(authenticationProvider))
                {
                    var loginResult = repo.Login("John", "somepassword");
                    loginResult.Should().BeNull();
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
                           new object[] {null, fixture.Create<string>(), fixture.Create<string>()},
                           new object[] {fixture.Create<string>(), null, fixture.Create<string>()}
                       };
            }
        }

        [Theory]
        [MemberData(nameof(RegistrationInfosArgumentNull))]
        public void RegisterUser_NullEmailOrPassword_ShouldThrowArgumentException(string email, string password, string profileId)
        {
            var repository = new AccountRepository(Substitute.For<PipelineService>());
            repository.Invoking(x => x.RegisterUser(email, password, profileId)).ShouldThrow<ArgumentNullException>();
        }
    }
}