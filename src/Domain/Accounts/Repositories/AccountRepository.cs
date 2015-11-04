namespace Habitat.Accounts.Repositories
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Web;
  public class AccountRepository : IAccountsRepository
  {
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
  }
}