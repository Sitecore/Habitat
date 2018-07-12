namespace Sitecore.Feature.Accounts.Services
{
    using Sitecore.Security;

    public interface IUpdateContactFacetsService
    {
        void UpdateContactFacets(UserProfile profile);
    }
}