namespace Sitecore.Feature.Accounts.Services
{
    using Sitecore.Security;

    public interface IAccountTrackerService
    {
        void TrackRegistration();
        void TrackRegistrationOutcome();
        void TrackLoginAndIdentifyContact(string source, string identifier);
        void TrackLogout(string userName);
        void TrackRegistrationFailed(string email);
        void TrackLoginFailed(string userName);
        void TrackEditProfile(UserProfile userProfile);
    }
}