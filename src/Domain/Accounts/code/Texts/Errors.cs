namespace Sitecore.Feature.Accounts.Texts
{
  using Sitecore.Framework.SitecoreExtensions.Repositories;

  public static class Errors
  {
    public static string Required => DictionaryRepository.Get("/Accounts/Errors/Required", "{0} is required");

    public static string IvalidEmailAddress => DictionaryRepository.Get("/Accounts/Errors/IvalidEmailAddress", "Invalid email address");

    public static string MinimumPasswordLength => DictionaryRepository.Get("/Accounts/Errors/MinimumPasswordLength", "Minimum password length is {1}");

    public static string ConfirmPasswordMismatch => DictionaryRepository.Get("/Accounts/Errors/ConfirmPasswordMismatch", "Wrong confirm password");

    public static string UserAlreadyExists => DictionaryRepository.Get("/Accounts/Errors/UserAlreadyExists", "User with specified login already exists");

    public static string UserDoesNotExist => DictionaryRepository.Get("/Accounts/Errors/UserDoesNotExist", "User with specified email does not exist");

    public static string WrongInterest => DictionaryRepository.Get("/Accounts/Errors/WrongInterest", "Wrong interest has been selected");

    public static string ProfileMismatch => DictionaryRepository.Get("/Accounts/Errors/ProfileMismatch", "Your profile has wrong format");

    public static string MaxLength => DictionaryRepository.Get("/Accounts/Errors/MaxLength", "{0} lenght should be less than {1}");

    public static string PhoneNumberFormat => DictionaryRepository.Get("/Accounts/Errors/PhoneNumberFormat", "Phone number should contain only +, ( ) and digits");
  }
}