namespace Habitat.Accounts.Controllers
{
  using System;
  using System.Linq;
  using System.Web.Mvc;
  using System.Web.Security;
  using Habitat.Accounts.Attributes;
  using Habitat.Accounts.Models;
  using Habitat.Accounts.Repositories;
  using Habitat.Accounts.Services;
  using Habitat.Accounts.Texts;
  using Habitat.Framework.SitecoreExtensions.Extensions;
  using Sitecore;
  using Sitecore.Diagnostics;

  public class AccountsController : Controller
  {
    private readonly IAccountRepository accountRepository;
    private readonly INotificationService notificationService;
    private readonly IAccountsSettingsService accountsSettingsService;
    private readonly IUserProfileService userProfileService;

    public AccountsController() : this(new AccountRepository(), new NotificationService(new AccountsSettingsService()), new AccountsSettingsService(), new UserProfileService())
    {
    }

    public AccountsController(IAccountRepository accountRepository, INotificationService notificationService, IAccountsSettingsService accountsSettingsService,IUserProfileService  userProfileService)
    {
      this.accountRepository = accountRepository;
      this.notificationService = notificationService;
      this.accountsSettingsService = accountsSettingsService;
      this.userProfileService = userProfileService;
    }

    [HttpGet]
    [RedirectAuthenticated]
    public ActionResult Register()
    {
      return this.View();
    }

    [HttpPost]
    [RedirectAuthenticated]
    [ValidateModel]
    public ActionResult Register(RegistrationInfo registrationInfo)
    {
      if (this.accountRepository.Exists(registrationInfo.Email))
      {
        this.ModelState.AddModelError(nameof(registrationInfo.Email), Errors.UserAlreadyExists);

        return this.View(registrationInfo);
      }

      try
      {
        var profileItem = this.userProfileService.GetUserDefaultProfile();
        this.accountRepository.RegisterUser(registrationInfo.Email,registrationInfo.Password, profileItem.ID.ToString());

        var link = this.accountsSettingsService.GetPageLinkOrDefault(Context.Item, Templates.AccountsSettings.Fields.AfterLoginPage, Context.Site.GetRootItem());
        return this.Redirect(link);
      }
      catch (MembershipCreateUserException ex)
      {
        Log.Error($"Can't create user with {registrationInfo.Email}", ex, this);
        this.ModelState.AddModelError(nameof(registrationInfo.Email), ex.Message);

        return this.View(registrationInfo);
      }
    }

    [HttpGet]
    [RedirectAuthenticated]
    public ActionResult Login()
    {
      return this.View();
    }

    [HttpPost]
    [ValidateModel]
    public ActionResult Login(LoginInfo loginInfo)
    {
      return this.Login(loginInfo, redirectUrl => new RedirectResult(redirectUrl));
    }

    protected virtual ActionResult Login(LoginInfo loginInfo, Func<string, ActionResult> redirectAction)
    {
      var result = this.accountRepository.Login(loginInfo.Email, loginInfo.Password);
      if (result)
      {
        var redirectUrl = loginInfo.ReturnUrl;
        if (string.IsNullOrEmpty(redirectUrl))
        {
          redirectUrl = this.accountsSettingsService.GetPageLinkOrDefault(Context.Item, Templates.AccountsSettings.Fields.AfterLoginPage, Context.Site.GetRootItem());
        }

        return redirectAction(redirectUrl);
      }

      this.ModelState.AddModelError("invalidCredentials", "Username or password is not valid.");

      return this.View(loginInfo);
    }

    [HttpPost]
    public ActionResult LoginDialog(LoginInfo loginInfo)
    {
      return this.Login(loginInfo, redirectUrl => this.Json(new LoginResult
                                                            {
                                                              RedirectUrl = redirectUrl
                                                            }));
    }

    [HttpPost]
    public ActionResult Logout()
    {
      this.accountRepository.Logout();

      return this.Redirect(Context.Site.GetRootItem().Url());
    }

    [HttpGet]
    [RedirectAuthenticated]
    public ActionResult ForgotPassword()
    {
      return this.View();
    }

    [HttpPost]
    [ValidateModel]
    [RedirectAuthenticated]
    public ActionResult ForgotPassword(PasswordResetInfo model)
    {
      if (!this.accountRepository.Exists(model.Email))
      {
        this.ModelState.AddModelError(nameof(model.Email), Errors.UserDoesNotExist);

        return this.View(model);
      }

      try
      {
        var newPassword = this.accountRepository.RestorePassword(model.Email);
        this.notificationService.SendPassword(model.Email, newPassword);
        return this.View("InfoMessage", new InfoMessage(Captions.ResetPasswordSuccess));
      }
      catch (Exception ex)
      {
        Log.Error($"Can't reset password for user {model.Email}", ex, this);
        this.ModelState.AddModelError(nameof(model.Email), ex.Message);

        return this.View(model);
      }
    }

    [HttpGet]
    [RedirectUnauthenticated]
    public ActionResult EditProfile()
    {
      var interests = this.userProfileService.GetInterests();
      if (Context.PageMode.IsPageEditor)
      {
        return this.View(new EditProfile(interests));
      }

      var siteProfileId = this.userProfileService.GetUserDefaultProfile().ID.ToString();
      if (siteProfileId != Sitecore.Context.User.Profile.ProfileItemId)
      {
        return null;
      }

      var profile = this.userProfileService.GetProfile(Context.User.Profile);

      return this.View(new EditProfile(profile, interests));
    }

    [HttpPost]
    [RedirectUnauthenticated]
    [ValidateModel]
    public virtual ActionResult EditProfile(EditProfile editProfile)
    {
      this.ValidateModel(editProfile);
      var interests = this.userProfileService.GetInterests().ToList();
      if (!interests.Contains(editProfile.Interest))
      {
        this.ModelState.AddModelError(nameof(editProfile.Interest),Errors.WrongInterest);
        editProfile.InterestTypes = interests;
        return this.View(editProfile);
      }

      this.userProfileService.SetProfile(Context.User.Profile, editProfile.GetProperties());

      return this.View();
    }
  }
}