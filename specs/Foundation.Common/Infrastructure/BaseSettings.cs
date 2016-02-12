namespace Sitecore.Foundation.Common.Specflow.Infrastructure
{
  using System.Configuration;

  public class BaseSettings
  {
    public string BaseUrl => ConfigurationManager.AppSettings["baseUrl"];
    public string RegisterPageUrl => BaseUrl + ConfigurationManager.AppSettings["registerUrl"];

    public string LoginPageUrl => BaseUrl + ConfigurationManager.AppSettings["loginPageUrl"];

    public string EditUserProfileUrl => BaseUrl + ConfigurationManager.AppSettings["editUserProfileUrl"];

    public string ContactUsPageUrl => BaseUrl + ConfigurationManager.AppSettings["contactUsPageUrl"];

    public string TestHelperService => BaseUrl + ConfigurationManager.AppSettings["testsProxyUrl"];

    public string ForgotPasswordPageUrl => BaseUrl + ConfigurationManager.AppSettings["forgotPasswordUrl"];
    public string EndSessionUrl => BaseUrl + ConfigurationManager.AppSettings["endSessionUrl"];
    public string UtfHelperService => BaseUrl + ConfigurationManager.AppSettings["utfProxyUrl"];
    public string UserName => "sitecore\\admin";
    public string Password => "b";

    public string EndVisitUrl => BaseUrl + ConfigurationManager.AppSettings["endVisitUrl"];

    public string DemoSiteURL => ConfigurationManager.AppSettings["demoSiteUrl"];
  }
}