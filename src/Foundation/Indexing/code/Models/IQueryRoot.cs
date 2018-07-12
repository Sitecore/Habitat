namespace Sitecore.Foundation.Indexing.Models
{
    using Sitecore.Data.Items;

    public interface IQueryRoot
    {
        Item Root { get; set; }
    }
}