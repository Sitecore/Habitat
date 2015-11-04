namespace Habitat.Accounts.Models
{
  using System.ComponentModel.DataAnnotations;
  using global::Habitat.Accounts.Attributes;
  using Texts;

  public class RegistrationInfo
  {
    [Display(Name = "Email", ResourceType = typeof(Captions))]
    [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Errors))]
    [EmailAddress(ErrorMessageResourceName = "EmailAddress", ErrorMessageResourceType = typeof(Errors))]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    [Display(Name = "Login", ResourceType = typeof(Captions))]
    [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Errors))]
    public string UserName { get; set; }

    [Display(Name = "Password", ResourceType = typeof(Captions))]
    [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Errors))]
    [PasswordMinLength(ErrorMessageResourceName = "MinimumPasswordLength", ErrorMessageResourceType = typeof(Errors))]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Display(Name = "ConfirmPassword", ResourceType = typeof(Captions))]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessageResourceName = "ConfirmPasswordMismatch", ErrorMessageResourceType = typeof(Errors))]
    public string ConfirmPassword { get; set; }
  }
}