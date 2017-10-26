namespace Sitecore.Feature.Accounts.Infrastructure.Pipelines
{
    using Sitecore.Analytics;
    using Sitecore.Feature.Accounts.Services;
    using Sitecore.Foundation.Accounts.Pipelines;

    public class TrackLoggedOut
    {
        private readonly IAccountTrackerService accountTrackerService;

        public TrackLoggedOut(IAccountTrackerService accountTrackerService)
        {
            this.accountTrackerService = accountTrackerService;
        }

        public void Process(AccountsPipelineArgs args)
        {
            this.accountTrackerService.TrackLogout(args.UserName);
        }
    }
}