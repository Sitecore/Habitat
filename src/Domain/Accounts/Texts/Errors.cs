using System;

namespace Habitat.Accounts.Texts
{
  using Sitecore.Globalization;

  public static class Errors
  {
    public static string Required => Translate.Text("{0} is required");

    public static string EmailAddress => Translate.Text("Invalid email address");

    public static string MinimumPasswordLength => Translate.Text("Minimum password length is {1}");

    public static string ConfirmPasswordMismatch => Translate.Text("Wrong confirm password");

    public static string UserAlreadyExists => Translate.Text("User with specified login already exists");

    public static string UserDoesNotExist => Translate.Text("User with specified email does not exist");
  }
}