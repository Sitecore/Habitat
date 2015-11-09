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
      var fullName = Context.Domain.GetFullName(userName);

      return User.Exists(fullName);
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
      Assert.ArgumentNotNullOrEmpty(registrationInfo.Email, "registrationInfo.Email");
      Assert.ArgumentNotNullOrEmpty(registrationInfo.Password, "registrationInfo.Password");
      Assert.ArgumentNotNullOrEmpty(registrationInfo.ConfirmPassword, "registrationInfo.ConfirmPassword");

      var fullName = Context.Domain.GetFullName(registrationInfo.Email);
      Assert.IsNotNullOrEmpty(fullName,"Can't retrieve full userName");

      var user = User.Create(fullName, registrationInfo.Password);
      user.Profile.Email = registrationInfo.Email;
      user.Profile.Save();

      AuthenticationManager.Login(user);
    }
  }
}
    
