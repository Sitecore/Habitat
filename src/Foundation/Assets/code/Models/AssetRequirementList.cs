namespace Sitecore.Foundation.Assets.Models
{
  using System.Collections;
  using System.Collections.Generic;
  using System.Linq;
  using Sitecore.Caching;

  internal class AssetRequirementList : ICacheable, IEnumerable<Asset>
  {
    private readonly List<Asset> _items = new List<Asset>();

    public AssetRequirementList()
    {
      this.Cacheable = true;
    }

    public event DataLengthChangedDelegate DataLengthChanged
    {
      add
      {
      }
      remove
      {
      }
    }

    public bool Cacheable { get; set; }

    public bool Immutable => true;

    public long GetDataLength()
    {
      return this._items.Sum(x => x.GetDataLength());
    }

    public IEnumerator<Asset> GetEnumerator()
    {
      return this._items.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return this.GetEnumerator();
    }

    public void Add(Asset requirement)
    {
      this._items.Add(requirement);
    }
  }
}