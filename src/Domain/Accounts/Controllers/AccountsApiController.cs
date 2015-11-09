namespace Habitat.Accounts.Controllers
{
  using System.Web.Http;
  using Models;
  using Repositories;
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