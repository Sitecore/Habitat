using Sitecore.Caching;
using Sitecore.Data;

namespace Habitat.Framework.Assets.Models
{
    internal class AssetRequirementCache : CustomCache
    {
        public AssetRequirementCache(long maxSize)
            : base("Habitat.AssetRequirements", maxSize)
        {
        }

        public AssetRequirementList Get(ID cacheKey)
        {
            return (AssetRequirementList)GetObject(cacheKey);
        }

        public void Set(ID cacheKey, AssetRequirementList requirementList)
        {
            SetObject(cacheKey, requirementList);
        }
    }
}