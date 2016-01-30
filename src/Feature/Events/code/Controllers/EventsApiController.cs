using System.Linq;
using System.Web.Mvc;

namespace Sitecore.Feature.Events.Controllers
{
    using System;
    using System.Globalization;
    using Sitecore.Analytics;
    using Sitecore.Analytics.Data;
    using Sitecore.Data;
    using Sitecore.Data.Items;
    using Sitecore.Diagnostics;
    using Sitecore.Feature.Events.Models;
    using Sitecore.Feature.Events.Repositories;
    using Sitecore.Resources.Media;

    public class EventsApiController : Controller
    {
        [HttpGet]
        public ActionResult GetCalendarEventsJson(string id)
        {
            ID eventListId;
            if (!ID.TryParse(id, out eventListId))
            {
                //throw new Exception("Invalid event list id");
                return new EmptyResult();
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
            var eventRepository = new EventRepository(eventListItem);
            var events = eventRepository.Get().Select(c => new EventModel(c) as EventHeaderModel);

            return Json(events.Where(e => e.StartDate.HasValue).Select(c =>
              new
              {
                 
                  title = c.Title,
                  startsAtTxt = c.StartDate?.ToUniversalTime().ToString(CultureInfo.InvariantCulture) ?? "",
                  endsAtTxt = c.EndDate?.ToUniversalTime().ToString(CultureInfo.InvariantCulture) ?? "",
                  description = c.Description,
                  location = c.Location,
                  image = GetImageUrl(c.Image),
                  eventUrl=c.EventUrl


              }).ToArray(), JsonRequestBehavior.AllowGet);

        }

        public ActionResult RegisterCalendarAddPageEvent(string id, string data, string text)
        {
            ID itemId;
            if (ID.TryParse(id, out itemId))
            {
                //register event
                RegisterPageEvent(Settings.PageEvents.EventSavedToCalendar_ItemName, Settings.PageEvents.EventSavedToCalendar.Guid, itemId.Guid, data, text);
            }

            //action does not need to acknowledge
            return new EmptyResult();
        } 
        
        private void RegisterPageEvent(string name, Guid definitionId, Guid itemId, string data, string text)
        {
            if (Tracker.Current != null && Tracker.Current.Session != null && Tracker.Current.Session.Interaction != null)
            {
                var interaction = Tracker.Current.Session.Interaction;
                var pageEventData = new PageEventData(name, definitionId)
                {
                    ItemId = itemId,
                    Data = data,
                    Text = text
                };
                interaction.CurrentPage.Register(pageEventData);
            }            
        }

        private string GetImageUrl(Sitecore.Foundation.SitecoreExtensions.Model.Image image)
        {
            if (image != null && !string.IsNullOrEmpty(image.MediaId))
            {
                return MediaManager.GetMediaUrl(new MediaItem(Sitecore.Context.Database.GetItem(image.MediaId)));
            }


            return string.Empty;
        }


    }
}