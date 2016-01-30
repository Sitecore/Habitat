using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sitecore.Feature.Events.Controllers
{
    using System.Globalization;
    using Sitecore.Data;
    using Sitecore.Feature.Events.Models;
    using Sitecore.Feature.Events.Repositories;
    using Sitecore.Mvc.Presentation;

    public class EventsApiController : Controller
    {
        public EventsApiController()
        {
        }

        [HttpGet]
        public ActionResult GetEventsListJson()
        {
            var events = new List<object>
            {
                new {title = "EventsControllerevent1", startsAtTxt = System.DateTime.UtcNow.ToString(CultureInfo.InvariantCulture), endsAtTxt = System.DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)},
                new {title = "EventsControllerevent2", startsAtTxt = System.DateTime.UtcNow.AddDays(2).ToString(CultureInfo.InvariantCulture), endsAtTxt = System.DateTime.UtcNow.AddDays(2).AddHours(5).ToString(CultureInfo.InvariantCulture)},
                new {title = "EventsControllerevent3", startsAtTxt = System.DateTime.UtcNow.AddDays(4).ToString(CultureInfo.InvariantCulture), endsAtTxt = System.DateTime.UtcNow.AddDays(4).AddHours(5).ToString(CultureInfo.InvariantCulture)}
            };
            return Json(events.ToArray(), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetCalendarEventsJson(string id)
        {
            ID eventListId;
            if (!ID.TryParse(id, out eventListId))
            {
                throw new Exception("Invalid event list id");
            }

            //get event list item
            var eventListItem = Sitecore.Context.Database.GetItem(eventListId);
            if (eventListItem == null)
            {
                return new EmptyResult();
            }

            //set context item
            Context.Item = eventListItem;

            //initialize event repository
            var _eventRepository = new EventRepository(eventListItem);
            var events = _eventRepository.Get().Select(c => new EventModel(c) as EventHeaderModel);
                
            return Json(events.Select(c => new { title = c.Title, startsAtTxt = c.StartDate.Value}).ToArray(), JsonRequestBehavior.AllowGet);

        }
    }
}