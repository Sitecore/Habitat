namespace Sitecore.Feature.Events.Repositories
{
    using System.Collections.Generic;
    using Sitecore.Data.Items;

    public interface IEventRepository
  {
    IEnumerable<Item> Get();
    IEnumerable<Item> GetLatest(int count);
    IEnumerable<Item> GetCalendarEvents(int month);
  }
}