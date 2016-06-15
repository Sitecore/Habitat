namespace Sitecore.Feature.News.Repositories
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Sitecore.ContentSearch;
	using Sitecore.Feature.News.Models.Feature.News;
	using Synthesis;

	public class SynthesisNewsRepository : ISynthesisNewsRepository
	{
		public I_NewsFolderItem ContextItem { get; set; }

		public SynthesisNewsRepository(I_NewsFolderItem contextItem)
		{
			if (contextItem == null)
			{
				throw new ArgumentNullException(nameof(contextItem));
			}

			this.ContextItem = contextItem;
		}

		public IEnumerable<I_NewsArticleItem> Get(int count)
		{
			// NOTE: We had to go to Sitecore API item here because ContentSearch API. You could also use an index name based factory.
			using (var searchContext = ContentSearchManager.CreateSearchContext(new SitecoreIndexableItem(this.ContextItem.InnerItem)))
			{
				return searchContext.GetSynthesisQueryable<I_NewsArticleItem>()
					.Where(article => article.AncestorIds.Contains(this.ContextItem.Id))
					.OrderByDescending(article => article.NewsDate)
					.Take(count)
					.WhereResultIsValidDatabaseItem()
					.ToArray();
			}
		}
	}
}