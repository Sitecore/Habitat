namespace Sitecore.Feature.Metadata.Models
{
    using System.Collections.Generic;

    public interface IMetadata
    {
        string PageTitle { get; set; }
        string SiteTitle { get; set; }
        string Description { get; set; }
        ICollection<string> KeywordsList { get; }
        string Title { get; set; }
        ICollection<string> Robots { get; }
        ICollection<KeyValuePair<string, string>> CustomMetadata { get; }
    }
}