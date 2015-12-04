namespace Habitat.Accounts.Repositories
{
  using System.Web.Security;
  using Habitat.Accounts.Services;
  using Sitecore;
  using Sitecore.Analytics;
  using Sitecore.Diagnostics;
  using Sitecore.Security.Accounts;
  using Sitecore.Security.Authentication;

  public class AccountRepository : IAccountRepository
  {
    private readonly IAccountTrackerService accountTrackerService;

    public AccountRepository(IAccountTrackerService accountTrackerService)
    {
      this.accountTrackerService = accountTrackerService;
    }

    public bool Exists(string userName)
    {
      var fullName = Context.Domain.GetFullName(userName);

      return User.Exists(fullName);
    }

    public bool Login(string userName, string password)
    {
      var accountName = string.Empty;
      var domain = Context.Domain;
      if (domain != null)
      {
        accountName = domain.GetFullName(userName);
      }

      var result =  AuthenticationManager.Login(accountName, password);

      if (result)
      {
        accountTrackerService.TrackLogin();
        accountTrackerService.IdentifyContact(accountName);
      }

      return result;
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

    public void RegisterUser(string email, string password, string profileId)
    {
      Assert.ArgumentNotNullOrEmpty(email, nameof(email));
      Assert.ArgumentNotNullOrEmpty(password, nameof(password));

      var fullName = Context.Domain.GetFullName(email);
      Assert.IsNotNullOrEmpty(fullName, "Can't retrieve full userName");

      var user = User.Create(fullName, password);
      user.Profile.Email = email;
      if (!string.IsNullOrEmpty(profileId))
      {
        user.Profile.ProfileItemId = profileId;
      }

      user.Profile.Save();

      accountTrackerService.TrackRegister();

      AuthenticationManager.Login(user);

      accountTrackerService.TrackLogin();
    }
  }
}