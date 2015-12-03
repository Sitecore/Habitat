namespace Sitecore.Feature.Accounts.Texts
{
  using Sitecore.Foundation.SitecoreExtensions.Repositories;

  public static class Captions
  {
    public static string Name => DictionaryRepository.Get("/Accounts/Captions/Email", "Name");

    public static string Email => DictionaryRepository.Get("/Accounts/Captions/Email", "E-mail");

    public static string Login => DictionaryRepository.Get("/Accounts/Captions/Login", "Login");

    public static string Logout => DictionaryRepository.Get("/Accounts/Captions/Logout", "Logout");

    public static string Password => DictionaryRepository.Get("/Accounts/Captions/Password", "Password");

    public static string ConfirmPassword => DictionaryRepository.Get("/Accounts/Captions/ConfirmPassword", "Confirm password");

    public static string Register => DictionaryRepository.Get("/Accounts/Captions/Register", "Register");

    public static string Update => DictionaryRepository.Get("/Accounts/Captions/Update", "Update");

    public static string ResetPassword => DictionaryRepository.Get("/Accounts/Captions/ResetPassword", "Reset password");

    public static string ResetPasswordInfo => DictionaryRepository.Get("/Accounts/Captions/ResetPasswordInfo", "The new password will be sent to your e-mail.");

    public static string ResetPasswordSuccess => DictionaryRepository.Get("/Accounts/Captions/ResetPasswordSuccess", "Your password has been reset.");

    public static string RegisterSuccess => DictionaryRepository.Get("/Accounts/Captions/RegisterSuccess", "Registration was successfully completed");

    public static string EditProfileSuccess => DictionaryRepository.Get("/Accounts/Captions/EditProfileSuccess", "Profile was successfully updated");

    public static string ForgotYourPassword => DictionaryRepository.Get("/Accounts/Captions/ForgotYourPassword", "Forgot your password?");

    public static string Interests => DictionaryRepository.Get("/Accounts/Captions/Interests", "Interests");
    public static string Cancel => DictionaryRepository.Get("/Accounts/Captions/Cancel", "Cancel");

    public static string EditDetails => DictionaryRepository.Get("/Accounts/Captions/EditDetails", "Edit details");

    public static string FirstName => DictionaryRepository.Get("/Accounts/Captions/FirstName", "First name");
    public static string LastName => DictionaryRepository.Get("/Accounts/Captions/LastName", "Last name");
    public static string PhoneNumber => DictionaryRepository.Get("/Accounts/Captions/PhoneNumber", "Phone number");
    



  }
}