namespace Habitat.Accounts.Specflow.Steps
{
  using System.Configuration;

  public class Settings
  {
    public string BaseUrl => ConfigurationManager.AppSettings["baseUrl"];
    public string RegisterPageUrl => ConfigurationManager.AppSettings["registerUrl"];

    public string LoginPageUrl => ConfigurationManager.AppSettings["loginPageUrl"];

    public string EditUserProfileUrl => ConfigurationManager.AppSettings["editUserProfileUrl"];

    public string ContactUsPageUrl => ConfigurationManager.AppSettings["contactUsPageUrl"];

    public string TestHelperService => BaseUrl + ConfigurationManager.AppSettings["testsProxyUrl"];

    public string ForgotPasswordPageUrl => ConfigurationManager.AppSettings["forgotPasswordUrl"];
    public string EndSessionUrl => ConfigurationManager.AppSettings["endSessionUrl"];
  }
}