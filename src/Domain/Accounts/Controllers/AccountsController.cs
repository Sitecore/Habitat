using System.Net;

namespace Habitat.Accounts.Controllers
{
  using System.Web.Mvc;
  using System.Web.Security;
  using Models;
  using Repositories;
  using Sitecore;
  using Sitecore.Diagnostics;

  public class AccountsController : Controller
  {
    private readonly IAccountRepository accountRepository;
namespace Habitat.Accounts.Controllers
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Net;
  using System.Net.Http;
  using System.Net.Http.Headers;
  using System.Text;
  using System.Web.Http;
  using System.Web.Http.Results;
  using Habitat.Accounts.Models;
  using Habitat.Accounts.Repositories;
  using Sitecore.Globalization;

    public AccountsController() : this(new AccountRepository())
    {
    }
  public class AccountsController : ApiController
  {
    private readonly IAccountsRepository _accountsRepository;

    public AccountsController(IAccountRepository accountRepository)
    public AccountsController():this(new AccountRepository())
    {
    }

    public AccountsController(IAccountsRepository accountsRepository)
    {
      this._accountsRepository = accountsRepository;
    }


    [HttpPost]
    public LoginResult Login([FromBody]LoginCredentials credentials)
    {
      var loginResult = this._accountsRepository.Login(credentials.UserName, credentials.Password);

      var result = new LoginResult {IsAuthenticated = loginResult, ValidationMessage = Translate.Text("Username or password is not valid.")};

      return result;
    }
  }
}
      this.accountRepository = accountRepository;
    }

    [HttpGet]
    public ActionResult Register()
    {
      return this.View();
    }

    [HttpPost]
    public ActionResult Register(RegistrationInfo registrationInfo)
    {
      if (Context.IsLoggedIn)
      {
        return this.Redirect(Context.Site.StartPath);
      }

      if (!this.ModelState.IsValid)
      {
        return this.View(registrationInfo);
      }

      if (this.accountRepository.Exists(registrationInfo.UserName))
      {
        this.ModelState.AddModelError("UserName", "User with specified login already exists");

        return this.View(registrationInfo);
      }

      try
      {
        this.accountRepository.RegisterUser(registrationInfo);
      }
      catch (MembershipCreateUserException ex)
      {
        Log.Error($"Can't create user with name {registrationInfo.UserName}", ex, this);
        this.ModelState.AddModelError("Register", ex.Message);
      }

      return this.Redirect(Sitecore.Context.Site.StartPath);
    }

    [HttpPost]
    public ActionResult Logout()
    {
      accountRepository.Logout();
      return this.Redirect(Sitecore.Context.Site.StartPath);
    }

  }
}