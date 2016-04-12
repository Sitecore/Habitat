namespace Sitecore.Foundation.Installer
{
  using System.Web.Security;
  using Sitecore.Diagnostics;
  using Sitecore.SecurityModel;

  public class AccountsEnableAction : IPostStepAction
  {
    public void Run()
    {
      using (new SecurityDisabler())
      {
        foreach (MembershipUser user in Membership.GetAllUsers())
        {
          if (!user.IsApproved)
          {
            Log.Info($"Enabling user {user.UserName}", this);

            user.IsApproved = true;
            Membership.UpdateUser(user);
          }
        }
      }
    }
  }
}