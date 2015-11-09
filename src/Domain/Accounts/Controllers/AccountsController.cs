using System;
using System.Web.Mvc;
using System.Web.Security;
using Habitat.Accounts.Models;
using Habitat.Accounts.Repositories;
using Habitat.Accounts.Services;
using Habitat.Accounts.Texts;
using Habitat.Framework.SitecoreExtensions.Extensions;
using Sitecore;
using Sitecore.Diagnostics;

namespace Habitat.Accounts.Controllers
{
  public class AccountsController : Controller
  {
    private readonly IAccountRepository accountRepository;
    private readonly INotificationService notificationService;

    public AccountsController() : this(new AccountRepository(), new NotificationService(new AccountsSettingsService()))
    {
    }

    public AccountsController(IAccountRepository accountRepository, INotificationService notificationService)
    {
      this.accountRepository = accountRepository;
      this.notificationService = notificationService;
    }

    [HttpGet]
    public ActionResult Register()
    {
      return this.View();
    }

    [HttpPost]
    public ActionResult Register(RegistrationInfo registrationInfo)
    {
      if (!Context.IsLoggedIn)
      {
        if (!this.ModelState.IsValid)
        {
          return this.View(registrationInfo);
        }

        if (this.accountRepository.Exists(registrationInfo.Email))
        {
          this.ModelState.AddModelError(nameof(registrationInfo.Email), Errors.UserAlreadyExists);

          return this.View(registrationInfo);
        }

        try
        {
          this.accountRepository.RegisterUser(registrationInfo);
        }
        catch (MembershipCreateUserException ex)
        {
          Log.Error($"Can't create user with {registrationInfo.Email}", ex, this);
          this.ModelState.AddModelError(nameof(registrationInfo.Email), ex.Message);

          return this.View(registrationInfo);
        }
      }

      return this.Redirect(Context.Site.GetRoot().Url());
    }

    [HttpGet]
    public ActionResult Login()
    {
      if (Context.User.IsAuthenticated)
      {
        this.Response.Redirect("/");
      }

      return this.View();
    }

    [HttpPost]
    public ActionResult Login(LoginInfo loginInfo)
    {
      if (!this.ModelState.IsValid)
      {
        return this.View(loginInfo);
      }

      var result = this.accountRepository.Login(loginInfo.Email, loginInfo.Password);
      if (result)
      {
        var redirectUrl = loginInfo.ReturnUrl;
        if (string.IsNullOrEmpty(redirectUrl))
        {
          redirectUrl = "/";
        }

        this.Response.Redirect(redirectUrl);
      }
      else
      {
        this.ModelState.AddModelError("UserName", "Username or password is not valid.");
      }

      return this.View(loginInfo);
    }

    [HttpPost]
    public ActionResult Logout()
    {
      this.accountRepository.Logout();

      return this.Redirect(Context.Site.GetRoot().Url());
    }

    [HttpGet]
    public ActionResult ForgotPassword()
    {
      return this.View();
    }

    [HttpPost]
    public ActionResult ForgotPassword(PasswordResetInfo model)
    {
      if (!this.ModelState.IsValid)
      {
        return this.View(model);
      }

      if (!this.accountRepository.Exists(model.Email))
      {
        this.ModelState.AddModelError(nameof(model.Email), Errors.UserDoesNotExist);

        return this.View(model);
      }

      try
      {
        var newPassword = this.accountRepository.RestorePassword(model.Email);
        this.notificationService.SendPassword(model.Email, newPassword);
        return this.View("ForgotPasswordSuccess");
      }
      catch (Exception ex)
      {
        Log.Error($"Can't reset password for user {model.Email}", ex, this);
        this.ModelState.AddModelError(nameof(model.Email), ex.Message);

        return this.View(model);
      }
    }
  }
}