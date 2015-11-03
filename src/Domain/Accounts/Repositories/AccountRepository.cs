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

    public void RegisterUser(RegistrationInfo registrationInfo)
    {
      Assert.IsNotNullOrEmpty(registrationInfo.UserName, "UserName");
      Assert.IsNotNullOrEmpty(registrationInfo.Password, "Password");
      Assert.IsNotNullOrEmpty(registrationInfo.Email, "Email");

      var domainName = Context.Domain.GetFullName(registrationInfo.UserName);
      Membership.CreateUser(domainName, registrationInfo.Password, registrationInfo.Email);
      AuthenticationManager.Login(domainName, registrationInfo.Password, true);
    }
  }
}