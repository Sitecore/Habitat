namespace Sitecore.Feature.Accounts.Services
{
    using System;
    using Sitecore.Configuration;
    using Sitecore.Data;
    using Sitecore.Foundation.DependencyInjection;
    using Sitecore.Foundation.SitecoreExtensions.Services;
    using Sitecore.Security;

    [Service(typeof(IAccountTrackerService))]
    public class AccountTrackerService : IAccountTrackerService
    {
        private readonly IAccountsSettingsService accountsSettingsService;
        private readonly ITrackerService trackerService;

        public AccountTrackerService(IAccountsSettingsService accountsSettingsService, ITrackerService trackerService)
        {
            this.accountsSettingsService = accountsSettingsService;
            this.trackerService = trackerService;
        }

        public static Guid LogoutPageEventId => Guid.Parse("{D23A32CD-F893-495E-86F0-9FE852987376}");
        public static Guid RegistrationFailedPageEventId => Guid.Parse("{D98AAED9-CF5F-41D6-8A6E-109F60F1E950}");
        public static Guid LoginFailedPageEventId => Guid.Parse("{27E67C84-B055-4D57-ADEB-E73DEFCA22A8}");
        public static Guid EditProfilePageEvent => Guid.Parse("{7A2582D2-7270-4E53-8998-3934B84876C3}");
        public static Guid LoginGoalId => Guid.Parse(Settings.GetSetting("Sitecore.Feature.Accounts.LoginGoalId", "{66722F52-2D13-4DCC-90FC-EA7117CF2298}"));
        public static Guid RegistrationGoalId => Guid.Parse(Settings.GetSetting("Sitecore.Feature.Accounts.RegistrationGoalId", "{8FFB183B-DA1A-4C74-8F3A-9729E9FCFF6A}"));

        public virtual void TrackLoginAndIdentifyContact(string source, string identifier)
        {
            this.trackerService.TrackGoal(LoginGoalId, source);
            this.trackerService.IdentifyContact(source, identifier);
        }

        public void TrackLogout(string userName)
        {
            this.trackerService.TrackPageEvent(LogoutPageEventId, data: userName);
        }

        public virtual void TrackRegistration()
        {
            this.trackerService.TrackGoal(RegistrationGoalId);
            this.TrackRegistrationOutcome();
        }

        public virtual void TrackRegistrationFailed(string email)
        {
            this.trackerService.TrackPageEvent(RegistrationFailedPageEventId, data: email);
        }
        public virtual void TrackLoginFailed(string userName)
        {
            this.trackerService.TrackPageEvent(LoginFailedPageEventId, data: userName);
        }

        public void TrackEditProfile(UserProfile userProfile)
        {
            this.trackerService.TrackPageEvent(EditProfilePageEvent, data: userProfile.UserName);
        }

        public void TrackRegistrationOutcome()
        {
            var outcomeId = this.accountsSettingsService.GetRegistrationOutcome(Context.Item);
            if (outcomeId.HasValue)
            {
                this.trackerService.TrackOutcome(outcomeId.Value);
            }
        }
    }
}