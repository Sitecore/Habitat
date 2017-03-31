using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sitecore.Feature.Accounts.Pipelines
{
    using Sitecore.Analytics;
    using Sitecore.Feature.Accounts.Services;
    using Sitecore.Foundation.Accounts.Pipelines;
    using Sitecore.Foundation.SitecoreExtensions.Services;

    public class TrackLoggedIn
    {
        private readonly IAccountTrackerService accountTrackerService;

        public TrackLoggedIn() : this(new AccountTrackerService(new AccountsSettingsService(), new TrackerService()))
        {
        }

        public TrackLoggedIn(AccountTrackerService accountTrackerService)
        {
            this.accountTrackerService = accountTrackerService;
        }

        public void Process(LoggedInPipelineArgs args)
        {
            var contactId = args.ContactId;
            this.accountTrackerService.TrackLoginAndIdentifyContact(args.UserName);
            args.ContactId = Tracker.Current?.Contact?.ContactId;
            args.PreviousContactId = contactId;
        }
    }
}