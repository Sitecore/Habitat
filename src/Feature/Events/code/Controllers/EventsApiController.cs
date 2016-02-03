﻿using System.Linq;
using System.Web.Mvc;

namespace Sitecore.Feature.Events.Controllers
{
    using System;
    using System.Globalization;
    using Sitecore.Analytics;
    using Sitecore.Analytics.Data;
    using Sitecore.Data;
    using Sitecore.Feature.Events.Models;
    using Sitecore.Feature.Events.Repositories;

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
            var eventListItem = Context.Database.GetItem(eventListId);
            if (eventListItem == null)
            {
                return new EmptyResult();
            }

            //set context item, as the Foundation.Indexing SearchService creates SearchContext based on Cotext.Item 
            //like this: using (var context = ContentSearchManager.GetIndex((SitecoreIndexableItem)Context.Item).CreateSearchContext())
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
                  imageUrl =c.ImageUrl,
                  eventUrl = c.EventUrl,
                  summary=c.Summary


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
            if (TrackerEnabled())
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

        private static bool TrackerEnabled()
        {
            return Tracker.IsActive && Tracker.Current.Session != null && Tracker.Current.Session.Interaction != null;
        }



    }
}