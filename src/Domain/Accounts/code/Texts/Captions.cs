namespace Habitat.Accounts.Texts
{
  using Habitat.Framework.SitecoreExtensions.Repositories;

  public static class Captions
  {
    public static string Email => DictionaryRepository.Get("/Accounts/Captions/Email", "E-mail");

    public static string Login => DictionaryRepository.Get("/Accounts/Captions/Login", "Login");

    public static string Logout => DictionaryRepository.Get("/Accounts/Captions/Logout", "Logout");

    public static string Password => DictionaryRepository.Get("/Accounts/Captions/Password", "Password");

    public static string ConfirmPassword => DictionaryRepository.Get("/Accounts/Captions/ConfirmPassword", "Confirm password");

    public static string Register => DictionaryRepository.Get("/Accounts/Captions/Register", "Register");

    public static string ResetPassword => DictionaryRepository.Get("/Accounts/Captions/ResetPassword", "Reset password");

    public static string ResetPasswordInfo => DictionaryRepository.Get("/Accounts/Captions/ResetPasswordInfo", "The new password will be sent to your e-mail.");

    public static string ResetPasswordSuccess => DictionaryRepository.Get("/Accounts/Captions/ResetPasswordSuccess", "Your password has been reset.");

    public static string RegisterSuccess => DictionaryRepository.Get("/Accounts/Captions/RegisterSuccess", "Registration was successfully completed");
  }
}