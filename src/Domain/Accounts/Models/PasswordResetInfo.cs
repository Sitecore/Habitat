namespace Habitat.Accounts.Models
{
  using System.ComponentModel.DataAnnotations;
  using Attributes;
  using Texts;

  public class PasswordResetInfo
  {
    [Display(Name = nameof(Email), ResourceType = typeof(Captions))]
    [Required(ErrorMessageResourceName = nameof(Errors.Required), ErrorMessageResourceType = typeof(Errors))]
    [EmailAddress(ErrorMessageResourceName = nameof(Errors.EmailAddress), ErrorMessageResourceType = typeof(Errors))]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }
  }
}