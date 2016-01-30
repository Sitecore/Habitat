using System.Collections.Generic;
using System.Web.Mvc;

namespace Sitecore.Feature.Events.Controllers
{
    using System.Globalization;
    using Sitecore.Feature.Events.Repositories;
    using Sitecore.Mvc.Presentation;

    public class EventsController : Controller
    {

        private readonly IEventRepository _eventRepository;

        public EventsController() : this(new EventRepository(RenderingContext.Current.Rendering.Item))
        {
        }

        public EventsController(IEventRepository eventsRepository)
        {
            this._eventRepository = eventsRepository;
        }

        public ActionResult EventList()
        {
            var items = this._eventRepository.Get();
            return this.View(items);
        }

        public ActionResult EventCalendar()
        {
            return this.View();
        }

        public ActionResult EventDetail()
        {
            return this.View();
        }

        

    }
}