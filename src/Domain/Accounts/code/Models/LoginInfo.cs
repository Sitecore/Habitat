namespace Sitecore.Feature.Accounts.Models
{
  using System.ComponentModel.DataAnnotations;
  using Sitecore.Feature.Accounts.Attributes;
  using Sitecore.Feature.Accounts.Texts;

  public class LoginInfo
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

    public string ReturnUrl { get; set; }
  }
}