namespace Habitat.Accounts.Models
{
  using System.ComponentModel.DataAnnotations;
  using Attributes;
  using Texts;

  public class RegistrationInfo
  {
    [Display(Name = nameof(Captions.Email), ResourceType = typeof(Captions))]
    [Required(ErrorMessageResourceName = nameof(Errors.Required), ErrorMessageResourceType = typeof(Errors))]
    [EmailAddress(ErrorMessageResourceName = nameof(Errors.IvalidEmailAddress), ErrorMessageResourceType = typeof(Errors))]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    [Display(Name = nameof(Captions.Password), ResourceType = typeof(Captions))]
    [Required(ErrorMessageResourceName = nameof(Errors.Required), ErrorMessageResourceType = typeof(Errors))]
    [PasswordMinLength(ErrorMessageResourceName = nameof(Errors.MinimumPasswordLength), ErrorMessageResourceType = typeof(Errors))]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Display(Name = nameof(Captions.ConfirmPassword), ResourceType = typeof(Captions))]
    [DataType(DataType.Password)]
    [Compare(nameof(Password), ErrorMessageResourceName = nameof(Errors.ConfirmPasswordMismatch), ErrorMessageResourceType = typeof(Errors))]
    public string ConfirmPassword { get; set; }
  }
}