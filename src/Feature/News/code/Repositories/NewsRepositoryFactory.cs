namespace Sitecore.Feature.News.Repositories
{
  using System;
  using Sitecore.Data.Items;
  using Sitecore.Foundation.Alerts;
  using Sitecore.Foundation.Alerts.Exceptions;

  public class NewsRepositoryFactory : INewsRepositoryFactory
  {
    public INewsRepository Create(Item contextItem)
    {
      try
      {
        return new NewsRepository(contextItem);
      }
      catch (ArgumentException ex)
      {
        throw new InvalidDataSourceItemException($"{AlertTexts.InvalidDataSource}", ex);
      }
    }
  }
}