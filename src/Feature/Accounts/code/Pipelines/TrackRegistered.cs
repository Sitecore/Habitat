using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sitecore.Feature.Accounts.Pipelines
{
    using Sitecore.Feature.Accounts.Services;
    using Sitecore.Foundation.Accounts.Pipelines;
    using Sitecore.Foundation.SitecoreExtensions.Services;

    public class TrackRegistered
    {
        private readonly AccountTrackerService accountTrackerService;

        public TrackRegistered()
        {
            this.accountTrackerService = new AccountTrackerService(new AccountsSettingsService(), new TrackerService());
        }

        public void Process(AccountsPipelineArgs args)
        {
            this.accountTrackerService.TrackRegistration();
        }
    }
}