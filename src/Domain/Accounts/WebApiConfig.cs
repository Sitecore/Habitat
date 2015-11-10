namespace Habitat.Accounts
{
  using System.Web.Http;

  public static class WebApiConfig
  {
    public static void Register(HttpConfiguration config)
    {
      config.Routes.MapHttpRoute("HabitatAccounts", "api/{controller}/{action}"
        );
    }
  }
}