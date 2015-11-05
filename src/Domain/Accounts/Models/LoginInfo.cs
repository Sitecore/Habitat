namespace Habitat.Accounts.Models
{
  using System;
  using System.Collections.Generic;
  using System.ComponentModel.DataAnnotations;
  using System.Linq;
  using System.Web;
  using Habitat.Accounts.Attributes;
  using Habitat.Accounts.Texts;

  public class LoginInfo
  {
    [Display(Name = "E_mail", ResourceType = typeof(Captions))]
    [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Errors))]
    [EmailAddress(ErrorMessageResourceName = "EmailAddress", ErrorMessageResourceType = typeof(Errors))]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    [Display(Name = "Password", ResourceType = typeof(Captions))]
    [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Errors))]
    [PasswordMinLength(ErrorMessageResourceName = "MinimumPasswordLength", ErrorMessageResourceType = typeof(Errors))]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    public string ReturnUrl { get; set; }
  }
}