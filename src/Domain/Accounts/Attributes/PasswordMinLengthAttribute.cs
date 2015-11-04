namespace Habitat.Accounts.Attributes
{
  using System;
  using System.ComponentModel.DataAnnotations;
  using System.Web.Mvc;
  using System.Web.Security;

  [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
  public class PasswordMinLengthAttribute: MinLengthAttribute
  {
    public PasswordMinLengthAttribute() : base(Membership.MinRequiredPasswordLength)
    {
    }
  }
}