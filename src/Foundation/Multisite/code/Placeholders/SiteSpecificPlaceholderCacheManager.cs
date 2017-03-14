namespace Sitecore.Foundation.Multisite.Placeholders
{
    using System;
    using System.Collections.Concurrent;
    using Sitecore.Caching.Placeholders;
    using Sitecore.Configuration;
    using Sitecore.Data.Items;
    using Sitecore.Diagnostics;

    public class SiteSpecificPlaceholderCacheManager : DefaultPlaceholderCacheManager
    {
        private readonly ConcurrentDictionary<Tuple<string, string>, PlaceholderCache> caches = new ConcurrentDictionary<Tuple<string, string>, PlaceholderCache>();

        public override PlaceholderCache GetPlaceholderCache(string databaseName)
        {
            Assert.ArgumentNotNull(databaseName, "databaseName");
            return this.GetOrCreateCache(databaseName, Sitecore.Context.Site?.Name);
        }

        public PlaceholderCache GetPlaceholderCache(string databaseName, string siteName)
        {
            Assert.ArgumentNotNull(databaseName, "databaseName");
            return this.GetOrCreateCache(databaseName, siteName);
        }

        private PlaceholderCache GetOrCreateCache(string databaseName, string siteName)
        {
            return this.caches.GetOrAdd(this.GetCacheKey(databaseName, siteName), this.InstantiateCache);
        }

        public override void UpdateCache(Item item)
        {
            if (item == null)
            {
                return;
            }

            foreach (var cache in this.caches.Values)
            {
                cache.UpdateCache(item);
            }

        }

        private Tuple<string, string> GetCacheKey(string databaseName, string siteName)
        {
            return new Tuple<string, string>(databaseName, siteName);
        }

        private PlaceholderCache InstantiateCache(Tuple<string, string> keys)
        {
            var databaseName = keys.Item1;
            var siteName = keys.Item2;
            var databaseCache = !string.IsNullOrEmpty(siteName) ? this.GetOrCreateCache(databaseName, null) : null;
            var cache = new SiteSpecificPlaceholderCache(databaseName, siteName, databaseCache);
            if (Factory.GetDatabase(databaseName)?.GetItem(cache.ItemRootId) == null)
            {
                return null;
            }
            cache.Reload();
            return cache;
        }
    }
}