namespace Habitat.Accounts.Texts
{
  using Habitat.Framework.SitecoreExtensions.Repositories;

  public static class Errors
  {
    public static string Required => DictionaryRepository.Get("/Accounts/Errors/Required", "{0} is required");

    public static string IvalidEmailAddress => DictionaryRepository.Get("/Accounts/Errors/IvalidEmailAddress", "Invalid email address");

    public static string MinimumPasswordLength => DictionaryRepository.Get("/Accounts/Errors/MinimumPasswordLength", "Minimum password length is {1}");

    public static string ConfirmPasswordMismatch => DictionaryRepository.Get("/Accounts/Errors/ConfirmPasswordMismatch", "Wrong confirm password");

    public static string UserAlreadyExists => DictionaryRepository.Get("/Accounts/Errors/UserAlreadyExists", "User with specified login already exists");

    public static string UserDoesNotExist => DictionaryRepository.Get("/Accounts/Errors/UserDoesNotExist", "User with specified email does not exist");
  }
}