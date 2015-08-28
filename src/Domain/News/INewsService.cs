using System.Collections.Generic;
using Sitecore.Data.Items;

namespace Habitat.News
{
    public interface INewsService
    {
        IEnumerable<Item> GetNews();
        IEnumerable<Item> GetLatestNews(int count);
    }
}