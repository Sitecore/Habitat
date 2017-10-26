namespace Sitecore.Feature.Accounts.Infrastructure.Pipelines
{
    using Sitecore.Analytics;
    using Sitecore.Feature.Accounts.Services;
    using Sitecore.Foundation.Accounts.Pipelines;

    public class TrackLoggedIn
    {
        private readonly IAccountTrackerService accountTrackerService;

        public TrackLoggedIn(IAccountTrackerService accountTrackerService)
        {
            this.accountTrackerService = accountTrackerService;
        }

        public void Process(LoggedInPipelineArgs args)
        {
            var contactId = args.ContactId;
            this.accountTrackerService.TrackLoginAndIdentifyContact(args.Source, args.UserName);
            args.ContactId = Tracker.Current?.Contact?.ContactId;
            args.PreviousContactId = contactId;
        }
    }
}