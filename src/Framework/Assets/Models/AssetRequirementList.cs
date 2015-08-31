using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sitecore.Caching;

namespace Habitat.Framework.Assets.Models
{
    internal class AssetRequirementList : ICacheable, IEnumerable<AssetRequirement>
    {
        private readonly List<AssetRequirement> _items = new List<AssetRequirement>();

        public AssetRequirementList()
        {
            Cacheable = true;
        }

        public event DataLengthChangedDelegate DataLengthChanged { add { } remove { } }

        public bool Cacheable { get; set; }

        public bool Immutable => true;

        public long GetDataLength()
        {
            return _items.Sum(x => x.GetDataLength());
        }

        public IEnumerator<AssetRequirement> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(AssetRequirement requirement)
        {
            _items.Add(requirement);
        }
    }
}