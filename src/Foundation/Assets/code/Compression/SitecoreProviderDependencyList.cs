namespace Sitecore.Foundation.Assets.Compression
{
    using System.Collections.Generic;
    using ClientDependency.Core;
    using ClientDependency.Core.FileRegistration.Providers;

    public class SitecoreProviderDependencyList
    {
        internal SitecoreProviderDependencyList(BaseFileRegistrationProvider provider)
        {
            this.Provider = provider;
            this.Dependencies = new List<IClientDependencyFile>();
        }

        internal bool ProviderIs(BaseFileRegistrationProvider provider)
        {
            return this.Provider.Name == provider.Name;
        }

        internal void AddDependencies(IEnumerable<IClientDependencyFile> list)
        {
            this.Dependencies.AddRange(list);
        }

        internal void AddDependency(IClientDependencyFile file)
        {
            this.Dependencies.Add(file);
        }

        internal List<IClientDependencyFile> Dependencies { get; private set; }

        internal BaseFileRegistrationProvider Provider { get; private set; }
    }
}