using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sitecore.Feature.Events.Models
{
    using Sitecore.Data.Fields;
    using Sitecore.Data.Items;
    using Sitecore.Foundation.SitecoreExtensions.Extensions;

    public class EventModel
    {
        public string Title { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
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

        }

        public string GetEventFormattedTime()
        {
            string eventTime = string.Empty;
            if (StartDate != null)
            {
                eventTime = StartDate.GetValueOrDefault().ToShortTimeString();
            }

            if (EndDate != null)
            {
                eventTime = " - " + EndDate.GetValueOrDefault().ToShortTimeString();
            }

            return eventTime;
        }
    }
}