namespace Sitecore.Feature.Accounts.Attributes
{
  using System;
  using System.ComponentModel.DataAnnotations;
  using System.Web.Security;

  [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
  public class PasswordMinLengthAttribute : MinLengthAttribute
  {
    public PasswordMinLengthAttribute() : base(Membership.MinRequiredPasswordLength)
    {
    }
  }
}