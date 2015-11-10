namespace Habitat.Accounts.Models
{
  using System.ComponentModel.DataAnnotations;
  using Habitat.Accounts.Attributes;
  using Habitat.Accounts.Texts;

  public class LoginCredentials
  {
    [Required(ErrorMessageResourceName = nameof(Errors.Required), ErrorMessageResourceType = typeof(Errors))]
    [EmailAddress(ErrorMessageResourceName = nameof(Errors.IvalidEmailAddress), ErrorMessageResourceType = typeof(Errors))]
    public string Email { get; set; }

    public string ReturnUrl { get; set; }
    [Required(ErrorMessageResourceName = nameof(Errors.Required), ErrorMessageResourceType = typeof(Errors))]
    [PasswordMinLength(ErrorMessageResourceName = nameof(Errors.MinimumPasswordLength), ErrorMessageResourceType = typeof(Errors))]
    public string Password { get; set; }
  }
}