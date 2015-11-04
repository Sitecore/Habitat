namespace Habitat.Accounts.Repositories
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Web;
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
    
    public bool Login(string userName, string password)
    {
      string accountName = string.Empty;
      var defaultDomain = "extranet";
      var domain = Sitecore.Security.Domains.Domain.GetDomain("extranet");
      if (domain != null)
      {
        accountName = domain.GetFullName(userName);
  
      }            

      return Sitecore.Security.Authentication.AuthenticationManager.Login(accountName, password);
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
    