using Sitecore.Data.Items;

namespace Habitat.Framework.Indexing.Models
{
    public interface ISearchSettings
    {
        Item Root { get; set; }
    }
}