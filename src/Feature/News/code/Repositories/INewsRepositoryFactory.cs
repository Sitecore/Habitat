using Sitecore.Data.Items;

namespace Sitecore.Feature.News.Repositories
{
	using Sitecore.Feature.News.Models.Feature.News;

	public interface INewsRepositoryFactory
	{
		INewsRepository Create(Item contextItem);
	}

	public interface ISynthesisNewsRepositoryFactory
	{
		ISynthesisNewsRepository Create(I_NewsFolderItem newsFolder);
	}
}