namespace Sitecore.Feature.Metadata.Infrastructure.Pipelines.GetPageMetadata
{
    using Sitecore.Foundation.DependencyInjection;

    [Service]
    public class CombineTitle
    {
        public void Process(GetPageMetadataArgs args)
        {
            args.Metadata.Title = args.Metadata.PageTitle + args.Metadata.SiteTitle;
        }
    }
}