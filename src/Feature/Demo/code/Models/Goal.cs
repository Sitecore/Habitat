namespace Sitecore.Feature.Demo.Models
{
    using System;

    public class PageEvent
    {
        public int EngagementValue { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public bool IsCurrentVisit { get; set; }
        public string Data { get; set; }
    }
}