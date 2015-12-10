namespace Habitat.Accounts.Specflow.Steps
{
  using System.Configuration;

  internal static class Settings
  {
    public static string BaseUrl => ConfigurationManager.AppSettings["baseUrl"];
    public static string RegisterPageUrl => ConfigurationManager.AppSettings["registerUrl"];

    public static string LoginPageUrl => ConfigurationManager.AppSettings["loginPageUrl"];

    public static string EditUserProfileUrl => ConfigurationManager.AppSettings["editUserProfileUrl"];

    public static string ContactUsPageUrl => ConfigurationManager.AppSettings["contactUsPageUrl"];

    public static string TestHelperService => BaseUrl + ConfigurationManager.AppSettings["testsProxyUrl"];

    public static string ForgotPasswordPageUrl => ConfigurationManager.AppSettings["forgotPasswordUrl"];
    public static string EndSessionUrl => ConfigurationManager.AppSettings["endSessionUrl"];
  }
}