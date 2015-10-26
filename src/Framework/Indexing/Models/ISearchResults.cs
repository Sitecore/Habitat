using System.Collections;
using System.Collections.Generic;

namespace Habitat.Framework.Indexing.Models
{
    public interface ISearchResults
    {
        IEnumerable<ISearchResult> Results { get; }
        int TotalNumberOfResults { get; }
    }
}