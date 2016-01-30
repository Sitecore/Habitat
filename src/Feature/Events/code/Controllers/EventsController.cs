using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sitecore.Feature.Events.Controllers
{
    using Sitecore.Feature.Events.Models;
    using System.Globalization;
    using Sitecore.Feature.Events.Repositories;
    using Sitecore.Mvc.Controllers;
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
            var events = this._eventRepository.Get().Select(c => new EventModel(c));
            return this.View(events);
        }       

        public ActionResult EventDetail()
        {
            var eventModel = new EventModel(RenderingContext.Current.Rendering.Item);
            return this.View(eventModel);
        }

        public ActionResult EventCalendar()
        {
            return this.View();
        }

    }
}