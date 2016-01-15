namespace Sitecore.Feature.Accounts
{
  using System.Web.Http;

  public static class WebApiConfig
  {
    public static void Register(HttpConfiguration config)
    {
      config.Routes.MapHttpRoute("SitecoreFeatureAccountsApi", "api/{controller}/{action}");
    }
  }
}