using System.Collections.Generic;
using Sitecore.Data.Items;

namespace Habitat.News.Repositories
{
    public interface INewsRepository
    {
        IEnumerable<Item> Get();
        IEnumerable<Item> GetLatestNews(int count);
    }
}