namespace Sitecore.Foundation.Assets.Models
{
  using Sitecore.Caching;
  using Sitecore.Data;

  internal class AssetRequirementCache : CustomCache
  {
    public AssetRequirementCache(long maxSize) : base("Sitecore.Foundation.AssetRequirements", maxSize)
    {
    }

    public AssetRequirementList Get(ID cacheKey)
    {
      return (AssetRequirementList)this.GetObject(cacheKey);
    }

    public void Set(ID cacheKey, AssetRequirementList requirementList)
    {
      this.SetObject(cacheKey, requirementList);
    }
  }
}