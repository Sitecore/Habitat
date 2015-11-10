namespace Habitat.Accounts.Controllers
{
  using System.Linq;
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
      var validationMessage = Translate.Text("Username or password is not valid.");
      var result = new LoginResult {IsAuthenticated = false, ValidationMessage = validationMessage};
      if (this.ModelState.IsValid)
      {
        var loginResult = this._accountsRepository.Login(credentials.Email, credentials.Password);
        result.IsAuthenticated = loginResult;
      }
      else
      {
        result.ValidationMessage = this.ModelState.Values.First().Errors.First().ErrorMessage;
      }

      return result;
    }
  }
}