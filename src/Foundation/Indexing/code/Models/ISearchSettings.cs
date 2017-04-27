namespace Sitecore.Foundation.Indexing.Models
{
    using System.Collections.Generic;
    using Sitecore.Data;

    public interface ISearchSettings : IQueryRoot
    {
        IEnumerable<ID> Templates { get; }
        bool MustHaveFormatter { get; }
    }
}