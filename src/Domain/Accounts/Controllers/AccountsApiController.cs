namespace Habitat.Accounts.Controllers
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Web;
  using System.Net.Http;
  using System.Net.Http.Headers;
  using System.Text;
  using System.Web.Http;
  using System.Web.Http.Results;
  using Habitat.Accounts.Models;
  using Habitat.Accounts.Repositories;
  using Sitecore.Globalization;
  public class AccountsApiController: ApiController
  {
    private readonly IAccountRepository _accountsRepository;
    

    public AccountsApiController():this(new AccountRepository())
    {
    }

    public AccountsApiController(IAccountRepository accountsRepository)
    {
      this._accountsRepository = accountsRepository;
    }


    [HttpPost]
    public LoginResult Login([FromBody]LoginCredentials credentials)
    {
      var loginResult = this._accountsRepository.Login(credentials.UserName, credentials.Password);

      var result = new LoginResult { IsAuthenticated = loginResult, ValidationMessage = Translate.Text("Username or password is not valid.") };

      return result;
    }
  }
}