namespace Sitecore.Feature.Metadata.Controllers
{
    using System.Web.Mvc;
    using Sitecore.Feature.Metadata.Repositories;
    using Sitecore.Foundation.DependencyInjection;
    using Sitecore.Mvc.Controllers;
    using Sitecore.Mvc.Presentation;

    public class MetadataController : SitecoreController
    {
        public MetadataController(MetadataRepository metadataRepository)
        {
            this.MetadataRepository = metadataRepository;
        }

        public ActionResult PageMetadata()
        {
            var metadata = this.MetadataRepository.Get(RenderingContext.Current.Rendering.Item);
            return this.View(metadata);
        }

        public MetadataRepository MetadataRepository { get; }
    }
}