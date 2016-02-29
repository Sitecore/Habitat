namespace Sitecore.Feature.Accounts.Texts
{
  using Sitecore.Foundation.SitecoreExtensions.Repositories;

  public static class Errors
  {
    public static string Required => DictionaryRepository.Get("/Accounts/Errors/Required", "Please enter a value for {0}");

    public static string IvalidEmailAddress => DictionaryRepository.Get("/Accounts/Errors/IvalidEmailAddress", "Please enter a valid email address");

    public static string MinimumPasswordLength => DictionaryRepository.Get("/Accounts/Errors/MinimumPasswordLength", "Please enter a password with at lease {1} characters");

    public static string ConfirmPasswordMismatch => DictionaryRepository.Get("/Accounts/Errors/ConfirmPasswordMismatch", "Your password confirmation does not match. Please enter a new password.");

    public static string UserAlreadyExists => DictionaryRepository.Get("/Accounts/Errors/UserAlreadyExists", "A user with specified e-mail address already exists");

    public static string UserDoesNotExist => DictionaryRepository.Get("/Accounts/Errors/UserDoesNotExist", "User with specified e-mail address does not exist");

    public static string WrongInterest => DictionaryRepository.Get("/Accounts/Errors/WrongInterest", "Please select an interest from the list.");

    public static string ProfileMismatch => DictionaryRepository.Get("/Accounts/Errors/ProfileMismatch", "There was a internal error with your user profile. Please contact support.");

    public static string MaxLength => DictionaryRepository.Get("/Accounts/Errors/MaxLength", "{0} length should be less than {1}");

    public static string PhoneNumberFormat => DictionaryRepository.Get("/Accounts/Errors/PhoneNumberFormat", "Phone number should contain only +, ( ) and digits");
  }
}