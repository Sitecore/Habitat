using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sitecore.Feature.Events.Models
{
    using Sitecore.Data.Fields;
    using Sitecore.Data.Items;

    public class EventModel
    {
        public string Title { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime StartTime { get; set; }
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
            Title = item[Templates.Event.Fields.Title];
        }
    }
}