namespace Sitecore.Foundation.Common.Specflow.Infrastructure
{
  using System.Configuration;

  public class BaseSettings
  {
    public static string BaseUrl => ConfigurationManager.AppSettings["baseUrl"];
    public static string RegisterPageUrl => BaseUrl + ConfigurationManager.AppSettings["registerUrl"];

    public static string LoginPageUrl => BaseUrl + ConfigurationManager.AppSettings["loginPageUrl"];

    public static string EditUserProfileUrl => BaseUrl + ConfigurationManager.AppSettings["editUserProfileUrl"];

    public static string ContactUsPageUrl => BaseUrl + ConfigurationManager.AppSettings["contactUsPageUrl"];

    public static string TestHelperService => BaseUrl + ConfigurationManager.AppSettings["testsProxyUrl"];

    public static string ForgotPasswordPageUrl => BaseUrl + ConfigurationManager.AppSettings["forgotPasswordUrl"];
    public static string EndSessionUrl => BaseUrl + ConfigurationManager.AppSettings["endSessionUrl"];
    public static string UtfHelperService => BaseUrl + ConfigurationManager.AppSettings["utfProxyUrl"];
    public static string UserName => "sitecore\\admin";
    public static string Password => "b";

    public static string EndVisitUrl => BaseUrl + ConfigurationManager.AppSettings["endVisitUrl"];

    public static string DemoSiteURL => ConfigurationManager.AppSettings["demoSiteUrl"];

    public static string DemoSiteCampaignUrl => ConfigurationManager.AppSettings["demoSiteCampaignUrl"];

    public static string FormsPageUrl =>BaseSettings.BaseUrl + ConfigurationManager.AppSettings["formsPageUrl"];

    public static string EmployeeList => BaseUrl + ConfigurationManager.AppSettings["employeeList"];
    public static string GettingStartedPageUrl => BaseSettings.BaseUrl + ConfigurationManager.AppSettings["GettingStartedPageUrl"];
  }
}