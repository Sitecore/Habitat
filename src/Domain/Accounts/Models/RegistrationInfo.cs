namespace Habitat.Accounts.Models
{
  using System.ComponentModel.DataAnnotations;
  using Attributes;
  using Texts;

  public class RegistrationInfo
  {
    [Display(Name = nameof(Email), ResourceType = typeof(Captions))]
    [Required(ErrorMessageResourceName = nameof(Errors.Required), ErrorMessageResourceType = typeof(Errors))]
    [EmailAddress(ErrorMessageResourceName = nameof(Errors.EmailAddress), ErrorMessageResourceType = typeof(Errors))]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    [Display(Name = nameof(Password), ResourceType = typeof(Captions))]
    [Required(ErrorMessageResourceName = nameof(Errors.Required), ErrorMessageResourceType = typeof(Errors))]
    [PasswordMinLength(ErrorMessageResourceName = nameof(Errors.MinimumPasswordLength), ErrorMessageResourceType = typeof(Errors))]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Display(Name = nameof(ConfirmPassword), ResourceType = typeof(Captions))]
    [DataType(DataType.Password)]
    [Compare(nameof(Password), ErrorMessageResourceName = nameof(Errors.ConfirmPasswordMismatch), ErrorMessageResourceType = typeof(Errors))]
    public string ConfirmPassword { get; set; }
  }
}