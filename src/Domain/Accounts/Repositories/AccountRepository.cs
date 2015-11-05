namespace Habitat.Accounts.Repositories
{
  using System.Web.Security;
  using Models;
  using Sitecore;
  using Sitecore.Diagnostics;
  using Sitecore.Security.Accounts;
  using Sitecore.Security.Authentication;

  public class AccountRepository : IAccountRepository
  {
    public bool Exists(string userName)
    {
      var domainName = Context.Domain.GetFullName(userName);

      return User.Exists(domainName);
    }

    public void Logout()
    {
      AuthenticationManager.Logout();
    }

    public void RegisterUser(RegistrationInfo registrationInfo)
    {
      Assert.IsNotNullOrEmpty(registrationInfo.Email, nameof(registrationInfo.Email));
      Assert.IsNotNullOrEmpty(registrationInfo.Password, nameof(registrationInfo.Password));
      Assert.IsNotNullOrEmpty(registrationInfo.Email, nameof(registrationInfo.ConfirmPassword));

      var domainName = Context.Domain.GetFullName(registrationInfo.Email);
      Membership.CreateUser(domainName, registrationInfo.Password, registrationInfo.Email);
      AuthenticationManager.Login(domainName, registrationInfo.Password, true);
    }
  }
}