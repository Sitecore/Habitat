﻿using System;

namespace Sitecore.Feature.Events.Models
{
    using Sitecore.Data.Items;
    using Sitecore.Foundation.SitecoreExtensions.Extensions;
    using Sitecore.Foundation.SitecoreExtensions.Model;
    using Sitecore.Links;

    public class EventHeaderModel
    {
        public string Title { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string ImageUrl { get; set; }
        public Image Image { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string EventUrl { get; set; }
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
            EventUrl = LinkManager.GetItemUrl(item);
            Title = item.GetString(Templates.Event.Fields.Title);
            StartDate = item.GetDate(Templates.Event.Fields.StartDate);
            EndDate = item.GetDate(Templates.Event.Fields.EndDate);
            Location = item.GetString(Templates.Event.Fields.Location);
            Description = item.GetString(Templates.Event.Fields.Description);
            ImageUrl = GetImageUrl(item);
            var plainDesc = StringUtil.RemoveTags(Description);
            Summary = StringUtil.Clip(plainDesc, 250, true);
        }

        public static string GetImageUrl(Item currentItem)
        {
            var imageUrl = string.Empty;
            Data.Fields.ImageField imageField = currentItem.Fields[Templates.Event.Fields.Image];
            if (imageField?.MediaItem != null)
            {
                MediaItem image = new MediaItem(imageField.MediaItem);
                imageUrl = StringUtil.EnsurePrefix('/', Resources.Media.MediaManager.GetMediaUrl(image));
            }
            return imageUrl;
        }

        public string GetEventFormattedTimeUtc()
        {
            var eventTime = string.Empty;

            if (StartDate != null)
            {
                eventTime = StartDate.GetValueOrDefault().ToUniversalTime().ToShortTimeString();
            }

            if (EndDate != null)
            {
                eventTime += " - " + EndDate.GetValueOrDefault().ToUniversalTime().ToShortTimeString();
            }

            return eventTime;
        }
    }
}