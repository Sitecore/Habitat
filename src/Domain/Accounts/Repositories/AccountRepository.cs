namespace Habitat.Accounts.Repositories
{
  using System.Web.Security;
  using Habitat.Accounts.Models;
  using Sitecore;
  using Sitecore.Diagnostics;
  using Sitecore.Security.Accounts;
  using Sitecore.Security.Authentication;
  using Sitecore.Security.Domains;

  public class AccountRepository : IAccountRepository
  {
    public bool Exists(string userName)
    {
      var domainName = Context.Domain.GetFullName(userName);

      return User.Exists(domainName);
    }
    
    public bool Login(string userName, string password)
    {
      string accountName = string.Empty;
      var domain = Sitecore.Context.Site.Domain;
      if (domain != null)
      {
        accountName = domain.GetFullName(userName);
  
      }            

      return AuthenticationManager.Login(accountName, password);
    }

    public void Logout()
    {
      AuthenticationManager.Logout();
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
    