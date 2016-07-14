using System.Configuration;

namespace Sitecore.Foundation.Common.Specflow.Extensions.Infrastructure
{
  using System;
  using Sitecore.Foundation.Common.Specflow.UtfService;

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

    public static string UserNameOnUi => "admin";
    public static string Password => "b";

    public static string EndVisitUrl => BaseUrl + ConfigurationManager.AppSettings["endVisitUrl"];

    public static string DemoSiteUrl => ConfigurationManager.AppSettings["demoSiteUrl"];

    public static string DemoSiteCampaignUrl => ConfigurationManager.AppSettings["demoSiteCampaignUrl"];

    public static string FormsPageUrl =>BaseUrl + ConfigurationManager.AppSettings["formsPageUrl"];

    public static string EmployeeList => BaseUrl + ConfigurationManager.AppSettings["employeeList"];
    public static string GettingStartedPageUrl => BaseUrl + ConfigurationManager.AppSettings["GettingStartedPageUrl"];
    public static string ExperianceEditorUrl => BaseUrl + ConfigurationManager.AppSettings["ExperianceEditorUrl"];

    public static string SocialPageExperienceEditorUrl => BaseUrl + ConfigurationManager.AppSettings["SocialPageExperienceEditorUrl"];

    public static string SocialPageUrl => BaseUrl + ConfigurationManager.AppSettings["SocialPageUrl"];

    public static string MainPageExperienceEditorUrl => BaseUrl + ConfigurationManager.AppSettings["MainPageExperienceEditorUrl"];
    public static Database ContextDatabase => (Database)Enum.Parse(typeof(Database), ConfigurationManager.AppSettings["database"]);
  }
}