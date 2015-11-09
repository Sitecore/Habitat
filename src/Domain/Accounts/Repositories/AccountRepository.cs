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

    public string RestorePassword(string userName)
    {
      var domainName = Context.Domain.GetFullName(userName);
      var user = Membership.GetUser(domainName);
      return user.ResetPassword();
    }

    public void RegisterUser(RegistrationInfo registrationInfo)
    {
      Assert.ArgumentNotNull(registrationInfo, nameof(registrationInfo));
      Assert.ArgumentNotNullOrEmpty(registrationInfo.Email, nameof(registrationInfo.Email));
      Assert.ArgumentNotNullOrEmpty(registrationInfo.Password, nameof(registrationInfo.Password));
      Assert.ArgumentNotNullOrEmpty(registrationInfo.ConfirmPassword, nameof(registrationInfo.ConfirmPassword));

      var domainName = Context.Domain.GetFullName(registrationInfo.Email);
      Membership.CreateUser(domainName, registrationInfo.Password, registrationInfo.Email);
      AuthenticationManager.Login(domainName, registrationInfo.Password, true);
    }
  }
}
    
