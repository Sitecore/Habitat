namespace Sitecore.Feature.Accounts.Infrastructure.Pipelines.SignedIn
{
    using System.Security.Claims;
    using Sitecore.Analytics;
    using Sitecore.Feature.Accounts.Services;
    using Sitecore.Owin.Authentication.Configuration;
    using Sitecore.Owin.Authentication.Pipelines.CookieAuthentication.SignedIn;

    public class TrackSignedIn : SignedInProcessor
    {
        public TrackSignedIn(IAccountTrackerService accountTrackerService, IUpdateContactFacetsService updateContactFacetsService, FederatedAuthenticationConfiguration federatedAuthenticationConfiguration)
        {
            this.AccountTrackerService = accountTrackerService;
            this.UpdateContactFacetsService = updateContactFacetsService;
            this.FederatedAuthenticationConfiguration = federatedAuthenticationConfiguration;
        }

        private IAccountTrackerService AccountTrackerService { get; }
        private IUpdateContactFacetsService UpdateContactFacetsService { get; }
        private FederatedAuthenticationConfiguration FederatedAuthenticationConfiguration { get; }

        public override void Process(SignedInArgs args)
        {
            //Do not track the user signin if this is a response to a membership provider login
            var provider = this.GetProvider(args.Context.Identity);
            if (provider.Name == Owin.Authentication.Constants.LocalIdentityProvider)
            {
                return;
            }

            if (Tracker.Current == null)
            {
                Tracker.Initialize();
            }
            this.AccountTrackerService.TrackLoginAndIdentifyContact(provider.Name, args.User.Id);
            this.UpdateContactFacetsService.UpdateContactFacets(args.User.InnerUser.Profile);
        }

        private IdentityProvider GetProvider(ClaimsIdentity identity)
        {
            return this.FederatedAuthenticationConfiguration.GetIdentityProvider(identity);
        }
    }
}