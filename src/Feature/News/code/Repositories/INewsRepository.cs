namespace Sitecore.Feature.News.Repositories
{
	using System.Collections.Generic;
	using Sitecore.Data.Items;
	using Sitecore.Feature.News.Models.Feature.News;

	public interface INewsRepository
	{
		IEnumerable<Item> Get();
		IEnumerable<Item> GetLatestNews(int count);
	}

	public interface ISynthesisNewsRepository
	{
		IEnumerable<I_NewsArticleItem> Get(int count);
	}
}