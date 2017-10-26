namespace Sitecore.Feature.Accounts.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Net.Mail;
    using System.Reflection;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Security;
    using FluentAssertions;
    using NSubstitute;
    using NSubstitute.ExceptionExtensions;
    using Ploeh.AutoFixture.AutoNSubstitute;
    using Ploeh.AutoFixture.Xunit2;
    using Sitecore.Collections;
    using Sitecore.Data;
    using Sitecore.Data.Items;
    using Sitecore.FakeDb;
    using Sitecore.FakeDb.AutoFixture;
    using Sitecore.FakeDb.Sites;
    using Sitecore.Feature.Accounts.Controllers;
    using Sitecore.Feature.Accounts.Models;
    using Sitecore.Feature.Accounts.Repositories;
    using Sitecore.Feature.Accounts.Services;
    using Sitecore.Foundation.Alerts;
    using Sitecore.Foundation.Alerts.Models;
    using Sitecore.Foundation.Dictionary.Repositories;
    using Sitecore.Foundation.Testing;
    using Sitecore.Foundation.Testing.Attributes;
    using Sitecore.Globalization;
    using Sitecore.Security;
    using Sitecore.Security.Accounts;
    using Sitecore.Sites;
    using Xunit;

    public class AccountsControllerTests
    {
        public AccountsControllerTests()
        {
            HttpContext.Current = HttpContextMockFactory.Create();
            var dictionaryPhraseRepository = Substitute.For<IDictionaryPhraseRepository>();
            dictionaryPhraseRepository.Get(Arg.Any<string>(), Arg.Any<string>()).Returns(x => x[1] as string);
            HttpContext.Current.Items["DictionaryPhraseRepository.Current"] = dictionaryPhraseRepository;
        }

        [Theory]
        [AutoDbData]
        public void Logout_ShouldCallSitecoreLogout(Database db, [Content] DbItem item, IAccountRepository repo, INotificationService ns, AccountsSettingsService acc)
        {
            var fakeSite = new FakeSiteContext(new StringDictionary
                                               {
                                                   {"rootPath", "/sitecore/content"},
                                                   {"startItem", item.Name}
                                               }) as SiteContext;
            fakeSite.Database = db;
            using (new SiteContextSwitcher(fakeSite))
            {
                var ctrl = new AccountsController(repo, ns, acc, null, null, null);
                ctrl.Logout();
                repo.Received(1).Logout();
            }
        }

        [Theory]
        [AutoDbData]
        public void Logout_ShouldRedirectUserToHomePage(Database db, [Content] DbItem item, IAccountRepository repo, INotificationService ns, AccountsSettingsService acc)
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
                var ctrl = new AccountsController(repo, ns, acc, null, null, null);
                var result = ctrl.Logout();
                result.Should().BeOfType<RedirectResult>().Which.Url.Should().Be("/");
            }
        }

        [Theory]
        [AutoDbData]
        public void _Login_NotValid_ShouldReturnView(IAccountRepository repo, [NoAutoProperties] AccountsController controller, LoginInfo info)
        {
            var result = controller._Login(info);
            result.Should().BeOfType<ViewResult>();
        }

        [Theory]
        [AutoDbData]
        public void _Login_LoggedIn_ShouldRedirect(Database db, [Content] DbItem item, User user, [Frozen] IAccountRepository repo, LoginInfo info, INotificationService service, AccountsSettingsService accountSetting, IUserProfileService userProfileService)
        {
            userProfileService.ValidateUser(Arg.Any<User>()).ReturnsForAnyArgs(true);
            var controller = new AccountsController(repo, service, accountSetting, null, userProfileService, null);
            repo.Login(string.Empty, string.Empty).ReturnsForAnyArgs(x => user);
            var result = controller._Login(info);
            result.Should().BeOfType<JsonResult>();
            ((result as JsonResult).Data as LoginResult).RedirectUrl.Should().BeEquivalentTo(info.ReturnUrl);
        }

        [Theory]
        [AutoDbData]
        public void Register_ShouldReturnViewWithoutModel(Database db, [Content] DbItem item, [Frozen] IAccountRepository repo, [NoAutoProperties] AccountsController controller)
        {
            var fakeSite = new FakeSiteContext(new StringDictionary
                                               {
                                                   {"displayMode", "normal"}
                                               }) as SiteContext;
            using (new SiteContextSwitcher(fakeSite))
            {
                var result = controller.Register();
                result.Should().BeOfType<ViewResult>().Which.Model.Should().BeNull();
            }
        }

        [Theory]
        [AutoDbData]
        public void Login_ValidaData_ShouldRedirectToReturnUrl([Frozen] IAccountRepository repo, LoginInfo info, User user, INotificationService service, IAccountsSettingsService accountSetting, IUserProfileService userProfileService)
        {
            userProfileService.ValidateUser(Arg.Any<User>()).ReturnsForAnyArgs(true);
            var controller = new AccountsController(repo, service, accountSetting, null, userProfileService, null);
            repo.Login(string.Empty, string.Empty).ReturnsForAnyArgs(x => user);
            var result = controller.Login(info);
            result.Should().BeOfType<RedirectResult>().Which.Url.Should().Be(info.ReturnUrl);
        }

        [Theory]
        [AutoDbData]
        public void Login_InvalidData_ShouldReturnViewModel([Frozen] IAccountRepository repo, LoginInfo info, INotificationService service, IAccountsSettingsService accountSetting)
        {
            var controller = new AccountsController(repo, service, accountSetting, null, null, null);
            controller.ModelState.AddModelError("Error", "Error");
            var result = controller.Login(info);
            result.Should().BeOfType<ViewResult>();
        }


        [Theory]
        [AutoDbData]
        public void Login_InvalidData_ShouldAddErrorToModelState([Frozen] IAccountRepository repo, LoginInfo info, INotificationService service, IAccountsSettingsService accountSetting)
        {
            repo.Login(string.Empty, string.Empty).ReturnsForAnyArgs(x => null);
            var controller = new AccountsController(repo, service, accountSetting, null, null, null);
            var result = controller.Login(info);
            controller.ModelState.IsValid.Should().BeFalse();
            controller.ModelState.Keys.Should().Contain("invalidCredentials");
        }

        [Theory]
        [AutoDbData]
        public void ForgotPassword_ValidData_ShouldReturnViewWithoutModel([Frozen] IAccountRepository repo, INotificationService service, IAccountsSettingsService accountsSettings, MailMessage mailMessage)
        {
            accountsSettings.GetForgotPasswordMailTemplate().ReturnsForAnyArgs(x => mailMessage);
            var controller = new AccountsController(repo, service, accountsSettings, null, null, null);

            var fakeSite = new FakeSiteContext(new StringDictionary
                                               {
                                                   {"displayMode", "normal"}
                                               }) as SiteContext;
            using (new SiteContextSwitcher(fakeSite))
            {
                var result = controller.ForgotPassword();
                result.Should().BeOfType<ViewResult>().Which.Model.Should().BeNull();
            }
        }

        [Theory]
        [AutoDbData]
        public void Register_InvalidData_ShouldReturnModel(Database db, [Content] DbItem item, RegistrationInfo registrationInfo, [Frozen] IAccountRepository repo, [NoAutoProperties] AccountsController controller)
        {
            var fakeSite = new FakeSiteContext(new StringDictionary
                                               {
                                                   {"displayMode", "normal"}
                                               }) as SiteContext;
            using (new SiteContextSwitcher(fakeSite))
            {
                controller.ModelState.AddModelError("Error", "Error");

                var result = controller.Register(registrationInfo);
                result.Should().BeOfType<ViewResult>().Which.Model.Should().Be(registrationInfo);
            }
        }


        [Theory]
        [AutoDbData]
        public void ForgotPassword_NotValidPassword_ShouldReturnModel(PasswordResetInfo model, [Frozen] IAccountRepository repo, [NoAutoProperties] AccountsController controller)
        {
            var fakeSite = new FakeSiteContext(new StringDictionary
                                               {
                                                   {"displayMode", "normal"}
                                               }) as SiteContext;
            using (new SiteContextSwitcher(fakeSite))
            {
                repo.RestorePassword(Arg.Any<string>()).Returns("new password");
                repo.Exists(Arg.Any<string>()).Returns(true);
                controller.ModelState.AddModelError("Error", "Error");
                var result = controller.ForgotPassword(model);
                result.Should().BeOfType<ViewResult>().Which.Model.Should().Be(model);
            }
        }


        [Theory]
        [AutoDbData]
        public void ForgotPassword_ValidPassword_ShouldReturnSuccessView([Frozen] IAccountRepository repo, INotificationService ns, PasswordResetInfo model, AccountsSettingsService accountSetting, InfoMessage info)
        {
            var fakeSite = new FakeSiteContext(new StringDictionary
                                               {
                                                   {"displayMode", "normal"}
                                               }) as SiteContext;
            using (new SiteContextSwitcher(fakeSite))
            {
                var controller = new AccountsController(repo, ns, accountSetting, null, null, null);
                repo.RestorePassword(Arg.Any<string>()).Returns("new password");
                repo.Exists(Arg.Any<string>()).Returns(true);
                var result = controller.ForgotPassword(model);
                result.Should().BeOfType<ViewResult>().Which.ViewName.Should().Be(Constants.InfoMessageView);
            }
        }

        [Theory]
        [AutoDbData]
        public void ForgotPassword_ShouldCatchAndReturnViewWithError(PasswordResetInfo model, [Frozen] IAccountRepository repo, INotificationService notificationService, AccountsSettingsService settingService)
        {
            var fakeSite = new FakeSiteContext(new StringDictionary
                                               {
                                                   {"displayMode", "normal"}
                                               }) as SiteContext;
            using (new SiteContextSwitcher(fakeSite))
            {
                repo.RestorePassword(Arg.Any<string>()).ThrowsForAnyArgs(new Exception("Error"));
                repo.Exists(Arg.Any<string>()).Returns(true);
                var controller = new AccountsController(repo, notificationService, settingService, null, null, null);
                var result = controller.ForgotPassword(model);
                result.Should().BeOfType<ViewResult>().Which.Model.Should().Be(model);
                result.Should().BeOfType<ViewResult>().Which.ViewData.ModelState.Should().ContainKey(nameof(model.Email)).WhichValue.Errors.Should().Contain(x => x.ErrorMessage == "Error");
            }
        }

        [Theory]
        [AutoDbData]
        public void ForgotPassword_UserDoesNotExist_ShouldReturnModel(PasswordResetInfo model, [Frozen] IAccountRepository repo)
        {
            var fakeSite = new FakeSiteContext(new StringDictionary
                                               {
                                                   {"displayMode", "normal"}
                                               }) as SiteContext;
            using (new SiteContextSwitcher(fakeSite))
            {
                repo.RestorePassword(Arg.Any<string>()).Returns("new password");
                repo.Exists(Arg.Any<string>()).Returns(false);
                var controller = new AccountsController(repo, null, null, null, null, null);
                var result = controller.ForgotPassword(model);
                result.Should().BeOfType<ViewResult>().Which.Model.Should().Be(model);
                result.Should().BeOfType<ViewResult>().Which.ViewData.ModelState.Should().ContainKey(nameof(model.Email)).WhichValue.Errors.Should().Contain(x => x.ErrorMessage == "User with specified e-mail address does not exist");
            }
        }

        [Theory]
        [AutoDbData]
        public void Register_SameUserExists_ShouldReturnModelWithError(Database db, [Content] DbItem item, RegistrationInfo registrationInfo, [Frozen] IAccountRepository repo, [NoAutoProperties] AccountsController controller)
        {
            var fakeSite = new FakeSiteContext(new StringDictionary
                                               {
                                                   {"rootPath", "/sitecore/content"},
                                                   {"startItem", item.Name}
                                               }) as SiteContext;
            fakeSite.Database = db;
            Language.Current = Language.Invariant;

            using (new SiteContextSwitcher(fakeSite))
                using (new UserSwitcher($@"extranet\{registrationInfo.Email}", false))
                {
                    var result = controller.Register(registrationInfo);
                    result.Should().BeOfType<ViewResult>().Which.Model.Should().Be(registrationInfo);
                    result.Should().BeOfType<ViewResult>().Which.ViewData.ModelState.Should().ContainKey(nameof(registrationInfo.Email)).WhichValue.Errors.Should().Contain(x => x.ErrorMessage == "A user with specified e-mail address already exists");
                }
        }

        [Theory]
        [AutoDbData]
        public void Register_RegistrationThrowsMembershipException_ShouldReturnError(Database db, [Content] DbItem item, Item profileItem, RegistrationInfo registrationInfo, MembershipCreateUserException exception, [Frozen] IAccountRepository repo, [Frozen] INotificationService notifyService, [Frozen] AccountsSettingsService accountsSettingsService, [Frozen] IUserProfileService userProfileService)
        {
            repo.When(x => x.RegisterUser(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>())).Do(x => { throw new MembershipCreateUserException(); });
            userProfileService.GetUserDefaultProfileId().Returns(profileItem.ID.ToString());

            var controller = new AccountsController(repo, notifyService, accountsSettingsService, null, userProfileService, null);

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
                result.Should().BeOfType<ViewResult>().Which.Model.Should().Be(registrationInfo);
                result.Should().BeOfType<ViewResult>().Which.ViewData.ModelState.Should().ContainKey(nameof(registrationInfo.Email)).WhichValue.Errors.Should().Contain(x => x.ErrorMessage == exception.Message);
            }
        }

        [Theory]
        [AutoDbData]
        public void EditProfile_EditMode_ShouldReturnEmptyView(Database db, IUserProfileService userProfileService)
        {
            userProfileService.GetEmptyProfile().Returns(new EditProfile());

            var fakeSite = new FakeSiteContext(new StringDictionary
                                               {
                                                   {"rootPath", "/sitecore/content"}
                                               }) as SiteContext;
            fakeSite.Database = db;
            typeof(SiteContext).GetField("displayMode", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(fakeSite, DisplayMode.Edit);

            Language.Current = Language.Invariant;

            using (new SiteContextSwitcher(fakeSite))
            {
                var accounController = new AccountsController(null, null, null, null, userProfileService, null);
                var result = accounController.EditProfile();
                result.Should().BeOfType<ViewResult>().Which.Model.ShouldBeEquivalentTo(new EditProfile());
            }
        }

        [Theory]
        [AutoDbData]
        public void EditProfile_DefaultState_ShouldReturnProfileModel(string profileItemId, [Substitute] EditProfile editProfile, IUserProfileService userProfileService)
        {
            var user = Substitute.For<User>("extranet/John", true);
            user.Profile.Returns(Substitute.For<UserProfile>());
            user.Profile.ProfileItemId = profileItemId;
            userProfileService.GetUserDefaultProfileId().Returns(profileItemId);
            userProfileService.GetProfile(Arg.Any<UserProfile>()).Returns(editProfile);
            userProfileService.ValidateUser(Arg.Any<User>()).ReturnsForAnyArgs(true);

            var fakeSite = new FakeSiteContext(new StringDictionary
                                               {
                                                   {"displayMode", "normal"},
                                                   {"domain", "extranet"}
                                               }) as SiteContext;

            using (new SiteContextSwitcher(fakeSite))
                using (new UserSwitcher(user))
                {
                    var accounController = new AccountsController(null, null, null, null, userProfileService, null);
                    var result = accounController.EditProfile();
                    result.Should().BeOfType<ViewResult>().Which.Model.Should().Be(editProfile);
                }
        }

        [Theory]
        [AutoDbData]
        public void EditProfile_ProfileDoesntMatch_ShouldReturnInfoMessage(string siteProfileId, string profileItemId, IUserProfileService userProfileService)
        {
            var user = Substitute.For<User>("extranet/John", true);
            user.Profile.Returns(Substitute.For<UserProfile>());
            user.Profile.ProfileItemId = profileItemId;
            userProfileService.GetUserDefaultProfileId().Returns(siteProfileId);

            var fakeSite = new FakeSiteContext(new StringDictionary
                                               {
                                                   {"displayMode", "normal"}
                                               }) as SiteContext;

            using (new SiteContextSwitcher(fakeSite))
                using (new UserSwitcher(user))
                {
                    var accounController = new AccountsController(null, null, null, null, userProfileService, null);
                    var result = accounController.EditProfile();
                    result.Should().BeOfType<ViewResult>().Which.Model.Should().BeOfType<InfoMessage>().Which.Type.Should().Be(InfoMessage.MessageType.Error);
                }
        }

        [Theory]
        [AutoDbData]
        public void EditProfilePost_ProfileDoesntMatch_ShouldReturnInfoMessage(string siteProfileId, string profileItemId, [Substitute] EditProfile editProfile, IUserProfileService userProfileService)
        {
            var user = Substitute.For<User>("extranet/John", true);
            user.Profile.Returns(Substitute.For<UserProfile>());
            user.Profile.ProfileItemId = profileItemId;
            userProfileService.GetUserDefaultProfileId().Returns(siteProfileId);

            var fakeSite = new FakeSiteContext(new StringDictionary
                                               {
                                                   {"displayMode", "normal"}
                                               }) as SiteContext;

            using (new SiteContextSwitcher(fakeSite))
                using (new UserSwitcher(user))
                {
                    var accounController = new AccountsController(null, null, null, null, userProfileService, null);
                    var result = accounController.EditProfile(editProfile);
                    result.Should().BeOfType<ViewResult>().Which.Model.Should().BeOfType<InfoMessage>().Which.Type.Should().Be(InfoMessage.MessageType.Error);
                }
        }

        [Theory]
        [AutoDbData]
        public void EditProfilePost_InvalidData_ShouldReturnModelErrors(FakeSiteContext siteContext, ModelStateDictionary modelState, string profileItemId, IEnumerable<string> interests, [Substitute] EditProfile editProfile, IUserProfileService userProfileService)
        {
            var user = Substitute.For<User>("extranet/John", true);
            user.Profile.Returns(Substitute.For<UserProfile>());
            user.Profile.ProfileItemId = profileItemId;
            userProfileService.GetUserDefaultProfileId().Returns(profileItemId);
            userProfileService.GetInterests().Returns(interests);
            userProfileService.ValidateProfile(Arg.Any<EditProfile>(), Arg.Do<ModelStateDictionary>(x => x.AddModelError("key", "error"))).Returns(false);

            using (new SiteContextSwitcher(siteContext))
                using (new UserSwitcher(user))
                {
                    var accounController = new AccountsController(null, null, null, null, userProfileService, null);
                    var result = accounController.EditProfile(editProfile);
                    result.Should().BeOfType<ViewResult>().Which.ViewData.ModelState.Should().ContainKey("key").WhichValue.Errors.Should().Contain(e => e.ErrorMessage == "error");
                }
        }

        [Theory]
        [AutoDbData]
        public void EditProfilePost_ValidData_ShouldUpdateProfile(FakeSiteContext siteContext, string profileItemId, [Substitute] EditProfile editProfile, [Frozen] IUserProfileService userProfileService)
        {
            var user = Substitute.For<User>("extranet/John", true);
            user.Profile.Returns(Substitute.For<UserProfile>());
            user.Profile.ProfileItemId = profileItemId;
            userProfileService.GetUserDefaultProfileId().Returns(profileItemId);
            userProfileService.ValidateProfile(Arg.Any<EditProfile>(), Arg.Any<ModelStateDictionary>()).Returns(true);

            using (new SiteContextSwitcher(siteContext))
                using (new UserSwitcher(user))
                {
                    var accountsController = new AccountsController(null, null, null, null, userProfileService, null);
                    accountsController.ControllerContext = Substitute.For<ControllerContext>();
                    accountsController.ControllerContext.HttpContext.Returns(Substitute.For<HttpContextBase>());
                    accountsController.ControllerContext.HttpContext.Session.Returns(Substitute.For<HttpSessionStateBase>());
                    accountsController.ControllerContext.HttpContext.Request.Returns(Substitute.For<HttpRequestBase>());
                    accountsController.ControllerContext.HttpContext.Request.RawUrl.Returns("/");

                    var result = accountsController.EditProfile(editProfile);
                    userProfileService.Received(1).SetProfile(user.Profile, editProfile);
                    accountsController.Session["EditProfileMessage"].Should().BeOfType<InfoMessage>().Which.Type.Should().Be(InfoMessage.MessageType.Info);
                    result.Should().BeOfType<RedirectResult>();
                }
        }
    }
}