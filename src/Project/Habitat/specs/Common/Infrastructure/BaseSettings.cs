namespace Sitecore.Foundation.Common.Specflow.Infrastructure
{
  using System.Configuration;

  public class BaseSettings
  {
    public string BaseUrl => ConfigurationManager.AppSettings["baseUrl"];
    public string RegisterPageUrl => ConfigurationManager.AppSettings["registerUrl"];

    public string LoginPageUrl => ConfigurationManager.AppSettings["loginPageUrl"];

    public string EditUserProfileUrl => ConfigurationManager.AppSettings["editUserProfileUrl"];

    public string ContactUsPageUrl => ConfigurationManager.AppSettings["contactUsPageUrl"];

    public string TestHelperService => BaseUrl + ConfigurationManager.AppSettings["testsProxyUrl"];

    public string ForgotPasswordPageUrl => ConfigurationManager.AppSettings["forgotPasswordUrl"];
    public string EndSessionUrl => ConfigurationManager.AppSettings["endSessionUrl"];
    public string UtfHelperService => BaseUrl + ConfigurationManager.AppSettings["utfProxyUrl"];
    public string UserName => "sitecore\\admin";
    public string Password => "b";

    public string EndVisitUrl => ConfigurationManager.AppSettings["endVisitUrl"];
  }
}