namespace Sitecore.Feature.Accounts.Models
{
  using System.ComponentModel.DataAnnotations;
  using Sitecore.Feature.Accounts.Texts;
  using Sitecore.Foundation.Dictionary.Repositories;

  public class PasswordResetInfo
  {
    [Display(Name = nameof(EmailCaption), ResourceType = typeof(PasswordResetInfo))]
    [Required(ErrorMessageResourceName = nameof(Required), ErrorMessageResourceType = typeof(PasswordResetInfo))]
    [EmailAddress(ErrorMessageResourceName = nameof(InvalidEmailAddress), ErrorMessageResourceType = typeof(PasswordResetInfo))]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    public static string EmailCaption => DictionaryPhraseRepository.Current.Get("/Accounts/Forgot Pasword/Email", "E-mail");
    public static string Required => DictionaryPhraseRepository.Current.Get("/Accounts/Forgot Pasword/Required", "Please enter a value for {0}");
    public static string InvalidEmailAddress => DictionaryPhraseRepository.Current.Get("/Accounts/Forgot Pasword/Invalid Email Address", "Please enter a valid email address");
  }
}