namespace Sitecore.Feature.Demo.Repositories
{
    using Sitecore.Analytics.Tracking;
    using Sitecore.Feature.Demo.Models;

    public interface IPageViewRepository
    {
        PageView Get(ICurrentPageContext pageContext);
    }
}