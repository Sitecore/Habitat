using System.Linq;
using System.Web.Mvc;

namespace Sitecore.Feature.Events.Controllers
{
    using Sitecore.Data;
    using Sitecore.Feature.Events.Models;
    using Sitecore.Feature.Events.Repositories;
    using Sitecore.Mvc.Presentation;
    using Sitecore.Links;

    public class EventsController : Controller
    {

        private readonly IEventRepository eventRepository;

        public EventsController() : this(new EventRepository(RenderingContext.Current.Rendering.Item))
        {
        }

        public EventsController(IEventRepository eventsRepository)
        {
            eventRepository = eventsRepository;
        }

        public ActionResult EventList()
        {
            var events = eventRepository.Get().Select(c => new EventModel(c));
            return View(events);
        }

        public ActionResult EventDetail()
        {
            var eventModel = new EventModel(RenderingContext.Current.Rendering.Item);
            return View(eventModel);
        }
        public ActionResult EventCalendar()
        {
            var eventlistid = RenderingContext.Current.Rendering.DataSource;
            var eventlisturl = "";

            if (!string.IsNullOrEmpty(eventlistid))
            {
                var item = Context.Database.GetItem(ID.Parse(eventlistid) );
                if (item != null)
                {
                    eventlisturl = LinkManager.GetItemUrl(item);
                }

            }
         
            var model = new EventCalendarModel {EventlistId = eventlistid,EventListUrl = eventlisturl};
            return View(model);
        }


    }
}