namespace Sitecore.Foundation.Indexing.Models
{
    using System;
    using System.Web.Script.Serialization;
    using System.Xml.Serialization;
    using Sitecore.Data.Items;

    public interface ISearchResult
    {
        Item Item { get; }
        string Title { get; set; }
        string ContentType { get; set; }
        string Description { get; set; }
        Uri Url { get; set; }
        string ViewName { get; set; }
        MediaItem Media { get; set; }
    }
}