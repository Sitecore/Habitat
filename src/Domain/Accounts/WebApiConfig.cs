using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Habitat.Accounts
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "HabitatAccounts",
                routeTemplate: "api/{controller}/{action}"
            );
        }
    }
}
