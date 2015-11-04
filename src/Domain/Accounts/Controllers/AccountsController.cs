using System.Net;

namespace Habitat.Accounts.Controllers
{
  using System.Web.Mvc;
  using System.Web.Security;
  using Models;
  using Repositories;
  using Sitecore.Diagnostics;

  public class AccountsController : Controller
  {
    private readonly IAccountRepository accountRepository;

    public AccountsController() : this(new AccountRepository())
    {
    }

    public AccountsController(IAccountRepository accountRepository)
    {
      this.accountRepository = accountRepository;
    }

    [HttpGet]
    public ActionResult Register()
    {
      return this.View();
    }

    public ActionResult Register(RegistrationInfo registrationInfo)
    {
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