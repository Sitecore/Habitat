namespace Sitecore.Feature.Accounts.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using System.Web.Security;
    using Sitecore.Diagnostics;
    using Sitecore.Feature.Accounts.Attributes;
    using Sitecore.Feature.Accounts.Models;
    using Sitecore.Feature.Accounts.Repositories;
    using Sitecore.Feature.Accounts.Services;
    using Sitecore.Foundation.Alerts.Extensions;
    using Sitecore.Foundation.Alerts.Models;
    using Sitecore.Foundation.Dictionary.Repositories;
    using Sitecore.Foundation.SitecoreExtensions.Attributes;
    using Sitecore.Foundation.SitecoreExtensions.Extensions;

    public class AccountsController : Controller
    {
        public AccountsController(IAccountRepository accountRepository, INotificationService notificationService, IAccountsSettingsService accountsSettingsService, IGetRedirectUrlService getRedirectUrlService, IUserProfileService userProfileService, IFedAuthLoginButtonRepository fedAuthLoginRepository)
        {
            this.FedAuthLoginRepository = fedAuthLoginRepository;
            this.AccountRepository = accountRepository;
            this.NotificationService = notificationService;
            this.AccountsSettingsService = accountsSettingsService;
            this.GetRedirectUrlService = getRedirectUrlService;
            this.UserProfileService = userProfileService;
        }

        private IFedAuthLoginButtonRepository FedAuthLoginRepository { get; }
        private IAccountRepository AccountRepository { get; }
        private INotificationService NotificationService { get; }
        private IAccountsSettingsService AccountsSettingsService { get; }
        private IGetRedirectUrlService GetRedirectUrlService { get; }
        private IUserProfileService UserProfileService { get; }

        public static string UserAlreadyExistsError => DictionaryPhraseRepository.Current.Get("/Accounts/Register/User Already Exists", "A user with specified e-mail address already exists");

        private static string ForgotPasswordEmailNotConfigured => DictionaryPhraseRepository.Current.Get("/Accounts/Forgot Password/Email Not Configured", "The Forgot Password E-mail has not been configured");

        private static string UserDoesNotExistError => DictionaryPhraseRepository.Current.Get("/Accounts/Forgot Password/User Does Not Exist", "User with specified e-mail address does not exist");


        [RedirectAuthenticated]
        public ActionResult Register()
        {
            return this.View();
        }


        public ActionResult AccountsMenu()
        {
            var isLoggedIn = Context.IsLoggedIn && Context.PageMode.IsNormal;
            var accountsMenuInfo = new AccountsMenuInfo
            {
                IsLoggedIn = isLoggedIn,
                LoginInfo = !isLoggedIn ? this.CreateLoginInfo() : null,
                UserFullName = isLoggedIn ? Context.User.Profile.FullName : null,
                UserEmail = isLoggedIn ? Context.User.Profile.Email : null,
                AccountsDetailsPageUrl = this.AccountsSettingsService.GetPageLinkOrDefault(Context.Item, Templates.AccountsSettings.Fields.AccountsDetailsPage)
            };
            return this.View(accountsMenuInfo);
        }

        private LoginInfo CreateLoginInfo(string returnUrl = null)
        {
            return new LoginInfo
            {
                ReturnUrl = returnUrl,
                LoginButtons = this.FedAuthLoginRepository.GetAll()
            };
        }

        [HttpPost]
        [ValidateModel]
        [RedirectAuthenticated]
        [ValidateRenderingId]
        public ActionResult Register(RegistrationInfo registrationInfo)
        {
            if (this.AccountRepository.Exists(registrationInfo.Email))
            {
                this.ModelState.AddModelError(nameof(registrationInfo.Email), UserAlreadyExistsError);

                return this.View(registrationInfo);
            }

            try
            {
                this.AccountRepository.RegisterUser(registrationInfo.Email, registrationInfo.Password, this.UserProfileService.GetUserDefaultProfileId());

                var link = this.GetRedirectUrlService.GetRedirectUrl(AuthenticationStatus.Authenticated);
                return this.Redirect(link);
            }
            catch (MembershipCreateUserException ex)
            {
                Log.Error($"Can't create user with {registrationInfo.Email}", ex, this);
                this.ModelState.AddModelError(nameof(registrationInfo.Email), ex.Message);

                return this.View(registrationInfo);
            }
        }

        [RedirectAuthenticated]
        public ActionResult Login(string returnUrl = null)
        {
            return this.View(this.CreateLoginInfo(returnUrl));
        }

        public ActionResult LoginTeaser()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateModel]
        [ValidateRenderingId]
        public ActionResult Login(LoginInfo loginInfo)
        {
            return this.Login(loginInfo, redirectUrl => new RedirectResult(redirectUrl));
        }

        protected virtual ActionResult Login(LoginInfo loginInfo, Func<string, ActionResult> redirectAction)
        {
            var user = this.AccountRepository.Login(loginInfo.Email, loginInfo.Password);
            if (user == null)
            {
                this.ModelState.AddModelError("invalidCredentials", DictionaryPhraseRepository.Current.Get("/Accounts/Login/User Not Found", "Username or password is not valid."));
                return this.View(loginInfo);
            }

            var redirectUrl = loginInfo.ReturnUrl;
            if (string.IsNullOrEmpty(redirectUrl))
            {
                redirectUrl = this.GetRedirectUrlService.GetRedirectUrl(AuthenticationStatus.Authenticated);
            }

            return redirectAction(redirectUrl);
        }

        [HttpPost]
        [ValidateModel]
        public ActionResult _Login(LoginInfo loginInfo)
        {
            return this.Login(loginInfo, redirectUrl => this.Json(new LoginResult
            {
                RedirectUrl = redirectUrl
            }));
        }

        [HttpPost]
        [ValidateModel]
        public ActionResult LoginTeaser(LoginInfo loginInfo)
        {
            return this.Login(loginInfo, redirectUrl => new RedirectResult(redirectUrl));
        }

        [HttpPost]
        public ActionResult Logout()
        {
            this.AccountRepository.Logout();

            return this.Redirect(Context.Site.GetRootItem().Url());
        }

        [RedirectAuthenticated]
        public ActionResult ForgotPassword()
        {
            try
            {
                this.AccountsSettingsService.GetForgotPasswordMailTemplate();
            }
            catch (Exception)
            {
                return this.InfoMessage(InfoMessage.Error(ForgotPasswordEmailNotConfigured));
            }

            return this.View();
        }

        [HttpPost]
        [ValidateModel]
        [RedirectAuthenticated]
        public ActionResult ForgotPassword(PasswordResetInfo model)
        {
            if (!this.AccountRepository.Exists(model.Email))
            {
                this.ModelState.AddModelError(nameof(model.Email), UserDoesNotExistError);

                return this.View(model);
            }

            try
            {
                var newPassword = this.AccountRepository.RestorePassword(model.Email);
                this.NotificationService.SendPassword(model.Email, newPassword);
                return this.InfoMessage(InfoMessage.Success(DictionaryPhraseRepository.Current.Get("/Accounts/Forgot Password/Reset Password Success", "Your password has been reset.")));
            }
            catch (Exception ex)
            {
                Log.Error($"Can't reset password for user {model.Email}", ex, this);
                this.ModelState.AddModelError(nameof(model.Email), ex.Message);

                return this.View(model);
            }
        }

        [RedirectUnauthenticated]
        public ActionResult EditProfile()
        {
            if (!Context.PageMode.IsNormal)
            {
                return this.View(this.UserProfileService.GetEmptyProfile());
            }

            var profile = this.UserProfileService.GetProfile(Context.User);

            return this.View(profile);
        }

        [HttpPost]
        [RedirectUnauthenticated]
        public virtual ActionResult EditProfile(EditProfile profile)
        {
            if (!string.IsNullOrEmpty(profile.Interest) && !this.UserProfileService.GetInterests().Contains(profile.Interest))
            {
                this.ModelState.AddModelError(nameof(profile.Interest), DictionaryPhraseRepository.Current.Get("/Accounts/Edit Profile/Interest Not Found", "Please select an interest from the list."));
                profile.InterestTypes = this.UserProfileService.GetInterests();
            }
            if (!this.ModelState.IsValid)
            {
                return this.View(profile);
            }

            this.UserProfileService.SaveProfile(Context.User.Profile, profile);

            this.Session["EditProfileMessage"] = new InfoMessage(DictionaryPhraseRepository.Current.Get("/Accounts/Edit Profile/Edit Profile Success", "Profile was successfully updated"));
            return this.Redirect(this.Request.RawUrl);
        }
    }
}