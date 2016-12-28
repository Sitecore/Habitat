namespace Sitecore.Feature.Accounts.Controllers
{
    using System;
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
    using Sitecore.Foundation.SitecoreExtensions.Services;

    public class AccountsController : Controller
    {
        private readonly IAccountRepository accountRepository;
        private readonly INotificationService notificationService;
        private readonly IAccountsSettingsService accountsSettingsService;
        private readonly IGetRedirectUrlService getRedirectUrlService;
        private readonly IUserProfileService userProfileService;
        private readonly IContactProfileService contactProfileService;

        public AccountsController() : this(new AccountRepository(new AccountTrackerService(new AccountsSettingsService(), new TrackerService())), new NotificationService(new AccountsSettingsService()), new AccountsSettingsService(), new GetRedirectUrlService(), new UserProfileService(), new ContactProfileService())
        {
        }

        public AccountsController(IAccountRepository accountRepository, INotificationService notificationService, IAccountsSettingsService accountsSettingsService, IGetRedirectUrlService getRedirectUrlService, IUserProfileService userProfileService, IContactProfileService contactProfileService)
        {
            this.accountRepository = accountRepository;
            this.notificationService = notificationService;
            this.accountsSettingsService = accountsSettingsService;
            this.getRedirectUrlService = getRedirectUrlService;
            this.userProfileService = userProfileService;
            this.contactProfileService = contactProfileService;
        }


        [RedirectAuthenticated]
        public ActionResult Register()
        {
            return this.View();
        }

        public static string UserAlreadyExistsError => DictionaryPhraseRepository.Current.Get("/Accounts/Register/User Already Exists", "A user with specified e-mail address already exists");

        [HttpPost]
        [ValidateModel]
        [RedirectAuthenticated]
        [ValidateRenderingId]
        public ActionResult Register(RegistrationInfo registrationInfo)
        {
            if (this.accountRepository.Exists(registrationInfo.Email))
            {
                this.ModelState.AddModelError(nameof(registrationInfo.Email), UserAlreadyExistsError);

                return this.View(registrationInfo);
            }

            try
            {
                this.accountRepository.RegisterUser(registrationInfo.Email, registrationInfo.Password, this.userProfileService.GetUserDefaultProfileId());
                this.contactProfileService?.SetPreferredEmail(registrationInfo.Email);

                var link = this.getRedirectUrlService.GetRedirectUrl(AuthenticationStatus.Authenticated);
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
            var loginInfo = new LoginInfo
                            {
                                ReturnUrl = returnUrl
                            };
            return this.View(loginInfo);
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
            var user = this.accountRepository.Login(loginInfo.Email, loginInfo.Password);
            if (user == null)
            {
                this.ModelState.AddModelError("invalidCredentials", DictionaryPhraseRepository.Current.Get("/Accounts/Login/User Not Found", "Username or password is not valid."));
                return this.View(loginInfo);
            }

            if (!this.userProfileService.ValidateUser(user))
            {
                this.accountRepository.Logout();
                this.ModelState.AddModelError("invalidCredentials", DictionaryPhraseRepository.Current.Get("/Accounts/Login/Invalid User", "Sorry, your user details cannot be used on this website."));
                return this.View(loginInfo);
            }
            var redirectUrl = loginInfo.ReturnUrl;
            if (string.IsNullOrEmpty(redirectUrl))
            {
                redirectUrl = this.getRedirectUrlService.GetRedirectUrl(AuthenticationStatus.Authenticated);
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
            this.accountRepository.Logout();

            return this.Redirect(Context.Site.GetRootItem().Url());
        }

        private static string ForgotPasswordEmailNotConfigured => DictionaryPhraseRepository.Current.Get("/Accounts/Forgot Password/Email Not Configured", "The Forgot Password E-mail has not been configured");

        [RedirectAuthenticated]
        public ActionResult ForgotPassword()
        {
            try
            {
                this.accountsSettingsService.GetForgotPasswordMailTemplate();
            }
            catch (Exception)
            {
                return this.InfoMessage(InfoMessage.Error(ForgotPasswordEmailNotConfigured));
            }

            return this.View();
        }

        private static string UserDoesNotExistError => DictionaryPhraseRepository.Current.Get("/Accounts/Forgot Password/User Does Not Exist", "User with specified e-mail address does not exist");

        [HttpPost]
        [ValidateModel]
        [RedirectAuthenticated]
        public ActionResult ForgotPassword(PasswordResetInfo model)
        {
            if (!this.accountRepository.Exists(model.Email))
            {
                this.ModelState.AddModelError(nameof(model.Email), UserDoesNotExistError);

                return this.View(model);
            }

            try
            {
                var newPassword = this.accountRepository.RestorePassword(model.Email);
                this.notificationService.SendPassword(model.Email, newPassword);
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
                return this.View(this.userProfileService.GetEmptyProfile());
            }

            if (!this.userProfileService.ValidateUser(Context.User))
            {
                return this.ProfileMismatchMessage;
            }

            var profile = this.userProfileService.GetProfile(Context.User.Profile);

            return this.View(profile);
        }

        [HttpPost]
        [RedirectUnauthenticated]
        public virtual ActionResult EditProfile(EditProfile profile)
        {
            if (this.userProfileService.GetUserDefaultProfileId() != Context.User.Profile.ProfileItemId)
            {
                return this.ProfileMismatchMessage;
            }

            if (!this.userProfileService.ValidateProfile(profile, this.ModelState))
            {
                profile.InterestTypes = this.userProfileService.GetInterests();
                return this.View(profile);
            }

            this.userProfileService.SetProfile(Context.User.Profile, profile);
            this.contactProfileService?.SetProfile(profile);

            this.Session["EditProfileMessage"] = new InfoMessage(DictionaryPhraseRepository.Current.Get("/Accounts/Edit Profile/Edit Profile Success", "Profile was successfully updated"));
            return this.Redirect(this.Request.RawUrl);
        }

        private ViewResult ProfileMismatchMessage
        {
            get
            {
                return this.InfoMessage(InfoMessage.Error(DictionaryPhraseRepository.Current.Get("/Accounts/Edit Profile/Profile Mismatch", "There was a internal error with your user profile. Please contact support.")));
            }
        }
    }
}