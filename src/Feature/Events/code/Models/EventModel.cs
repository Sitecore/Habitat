using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sitecore.Feature.Events.Models
{
    using Sitecore.Data.Fields;
    using Sitecore.Data.Items;
    using Sitecore.Foundation.SitecoreExtensions.Extensions;
    using Sitecore.Foundation.SitecoreExtensions.Model;

    public class EventHeaderModel
    {
        public string Title { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
    }

    public class EventModel : EventHeaderModel
    {

        public Item Item { get; set; }

        public EventModel()
        {

        }

        /// <summary>
        /// Create event model from the event item
        /// </summary>
        /// <param name="item"></param>
        public EventModel(Item item)
        {
            Item = item;

            //fields
            Title = item.GetString(Templates.Event.Fields.Title);
            StartDate = item.GetDate(Templates.Event.Fields.StartDate);
            EndDate = item.GetDate(Templates.Event.Fields.EndDate);
            Location = item.GetString(Templates.Event.Fields.Location);
            Description = item.GetString(Templates.Event.Fields.Description);
       //     Image = item.GetImage(Templates.Event.Fields.Image) != null ? item.GetImage(Templates.Event.Fields.Image).MediaPath : "#";

        }

      

        public string GetEventFormattedTime()
        {
            var eventTime = string.Empty;

            if (StartDate != null)
            {
                eventTime = StartDate.GetValueOrDefault().ToShortTimeString();
            }

            if (EndDate != null)
            {
                eventTime += " - " + EndDate.GetValueOrDefault().ToShortTimeString();
            }

            return eventTime;
        }
    }
}