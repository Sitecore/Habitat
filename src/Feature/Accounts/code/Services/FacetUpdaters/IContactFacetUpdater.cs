namespace Sitecore.Feature.Accounts.Services.FacetUpdaters
{
    using System.Collections.Generic;
    using Sitecore.Security;
    using Sitecore.XConnect;

    public interface IContactFacetUpdater
    {
        IList<string> FacetsToUpdate { get; }
        bool SetFacets(UserProfile profile, Contact contact, IXdbContext client);
    }
}
