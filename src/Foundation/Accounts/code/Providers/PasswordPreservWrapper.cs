namespace Foundation.Accounts.Providers
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Web;
  using System.Web.Security;

  public class PasswordPreservWrapper : System.Web.Security.SqlMembershipProvider
  {
    public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
    {
      var user = base.CreateUser(username, password, email, passwordQuestion, passwordAnswer, isApproved, providerUserKey, out status);
      var sitecoreUser = Sitecore.Security.Accounts.User.FromName(username, true);
      sitecoreUser.Profile["password"] = password;
      sitecoreUser.Profile.Save();
      return user;
    }
  }
}