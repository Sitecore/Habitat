using System;
using Sitecore.Data.Items;
using Sitecore.Foundation.Alerts;
using Sitecore.Foundation.Alerts.Exceptions;

namespace Sitecore.Feature.News.Repositories
{
	using Sitecore.Feature.News.Models.Feature.News;

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
	public class SynthesisNewsRepositoryFactory : ISynthesisNewsRepositoryFactory
	{
		public ISynthesisNewsRepository Create(I_NewsFolderItem newsFolder)
		{
			try
			{
				return new SynthesisNewsRepository(newsFolder);
			}
			catch (ArgumentException ex)
			{
				throw new InvalidDataSourceItemException($"{AlertTexts.InvalidDataSource}", ex);
			}
		}
	}
}