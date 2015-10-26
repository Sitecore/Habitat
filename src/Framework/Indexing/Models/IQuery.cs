using Sitecore.Publishing.Pipelines.Publish;

namespace Habitat.Framework.Indexing.Models
{
    public interface IQuery
    {
        string QueryText { get; set; }
        int IndexOfFirstResult { get; set; }
        int NoOfResults { get; set; }
    }
}