using Sitecore.Sites;

namespace Sitecore.Foundation.Dictionary.Repositories
{
    public interface IDictionaryRepository
  {
    Models.Dictionary Get(SiteContext site);
  }
}