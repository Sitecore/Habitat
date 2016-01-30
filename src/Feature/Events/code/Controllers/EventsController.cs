using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sitecore.Feature.Events.Controllers
{
    public class EventsController : Controller
    {
        public ActionResult EventsList()
        {
            return this.View();
        }
    }
}