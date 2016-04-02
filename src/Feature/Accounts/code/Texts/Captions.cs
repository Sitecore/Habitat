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
    public static string PlaceholderLoginEmail => DictionaryRepository.Get("/Accounts/Captions/Login E-mail Placeholder", "Enter your e-mail");
    public static string PlaceholderLoginPassword => DictionaryRepository.Get("/Accounts/Captions/Login Password Placeholder", "Enter your password");
    public static string PlaceholderEditProfilePhone => DictionaryRepository.Get("/Accounts/Captions/Edit Profile Phone Placeholder", "Please enter your contact phone number");
    public static string PlaceholderEditProfileLastName => DictionaryRepository.Get("/Accounts/Captions/Edit Profile Last Name Placeholder", "Please enter your last name");
    public static string PlaceholderEditProfileFirstName => DictionaryRepository.Get("/Accounts/Captions/Edit Profile First Name Placeholder", "Please enter your first name");
    public static string PlaceholderRegisterEmail => DictionaryRepository.Get("/Accounts/Captions/Register Email Placeholder", "Please enter your e-mail address");
    public static string PlaceholderRegisterPassword => DictionaryRepository.Get("/Accounts/Captions/Register Password Placeholder", "Please enter your new password");
    public static string PlaceholderRegisterConfirmPassword => DictionaryRepository.Get("/Accounts/Captions/Register Confirm Password Placeholder", "Please confirm you new password");
    public static string PlaceholderForgotPasswordEmailPlaceholder => DictionaryRepository.Get("/Accounts/Captions/Forgot Password Email Placeholder", "Please enter the e-mail address you used to register");
  }
}