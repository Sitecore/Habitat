namespace Sitecore.Feature.Accounts.Services
{
    using Sitecore.Abstractions;
    using Sitecore.Analytics;
    using Sitecore.Analytics.Model;
    using Sitecore.Analytics.Tracking;
    using Sitecore.Foundation.DependencyInjection;

    /// <summary>
    /// ContactManager cannot be easily mocked, so we wrap it in a simple service.
    /// </summary>
    [Service(typeof(IContactManagerService))]
    public class ContactManagerService : IContactManagerService
    {
        private readonly ContactManager contactManager;

        public ContactManagerService(BaseFactory sitecoreFactory)
        {
            this.contactManager = sitecoreFactory.CreateObject("tracking/contactManager", true) as ContactManager;
        }

        public void SaveContact()
        {
            if (Tracker.Current?.Contact == null)
            {
                return;
            }
            if (Tracker.Current.Contact.IsNew)
            {
                Tracker.Current.Contact.ContactSaveMode = ContactSaveMode.AlwaysSave;
                this.contactManager.SaveContactToCollectionDb(Tracker.Current.Contact);
            }
        }

        public void ReloadContact()
        {
            if (Tracker.Current?.Session == null)
            {
                return;
            }
            this.contactManager.RemoveFromSession(Tracker.Current.Contact.ContactId);
            Tracker.Current.Session.Contact = this.contactManager.LoadContact(Tracker.Current.Contact.ContactId);
        }
    }
}