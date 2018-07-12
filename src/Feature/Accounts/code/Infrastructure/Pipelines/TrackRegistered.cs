namespace Sitecore.Feature.Accounts.Infrastructure.Pipelines
{
    using Sitecore.Feature.Accounts.Services;
    using Sitecore.Foundation.Accounts.Pipelines;
    using Sitecore.Foundation.DependencyInjection;

    public class TrackRegistered
    {
        private readonly IAccountTrackerService accountTrackerService;
        private readonly IUpdateContactFacetsService updateContactFacetsService;

        public TrackRegistered(IAccountTrackerService accountTrackerService, IUpdateContactFacetsService updateContactFacetsService)
        {
            this.accountTrackerService = accountTrackerService;
            this.updateContactFacetsService = updateContactFacetsService;
        }

        public void Process(AccountsPipelineArgs args)
        {
            this.updateContactFacetsService.UpdateContactFacets(args.User.Profile);
            this.accountTrackerService.TrackRegistration();
        }

    }
}