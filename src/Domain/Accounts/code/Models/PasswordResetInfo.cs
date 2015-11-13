namespace Habitat.Accounts.Models
{
  using System.ComponentModel.DataAnnotations;
  using Habitat.Accounts.Texts;

  public class PasswordResetInfo
  {
    [Display(Name = nameof(Captions.Email), ResourceType = typeof(Captions))]
    [Required(ErrorMessageResourceName = nameof(Errors.Required), ErrorMessageResourceType = typeof(Errors))]
    [EmailAddress(ErrorMessageResourceName = nameof(Errors.IvalidEmailAddress), ErrorMessageResourceType = typeof(Errors))]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }
  }
}