namespace Sitecore.Feature.Metadata.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Sitecore.Data.Fields;
    using Sitecore.Data.Items;
    using Sitecore.Feature.Metadata.Infrastructure.Pipelines.GetPageMetadata;
    using Sitecore.Feature.Metadata.Models;
    using Sitecore.Foundation.DependencyInjection;
    using Sitecore.Foundation.SitecoreExtensions.Extensions;
    using Sitecore.Pipelines;
    using Sitecore.Web.UI.WebControls;

    [Service]
    public class MetadataRepository
    {
        public IMetadata Get(Item item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            var args = new GetPageMetadataArgs(new MetadataViewModel(), item);
            CorePipeline.Run("metadata.getPageMetadata", args);

            return args.Metadata;
        }
    }
}