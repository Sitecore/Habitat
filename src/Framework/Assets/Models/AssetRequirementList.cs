using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sitecore.Caching;

namespace Habitat.Framework.Assets.Models
{
    internal class AssetRequirementList : ICacheable, IEnumerable<AssetRequirement>
    {
        private readonly List<AssetRequirement> items = new List<AssetRequirement>();

        public AssetRequirementList()
        {
            Cacheable = true;
        }

        public event DataLengthChangedDelegate DataLengthChanged { add { } remove { } }

        public bool Cacheable { get; set; }

        public bool Immutable => true;

        public long GetDataLength()
        {
            return items.Sum(x => x.GetDataLength());
        }

        public IEnumerator<AssetRequirement> GetEnumerator()
        {
            return items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(AssetRequirement requirement)
        {
            items.Add(requirement);
        }
    }
}