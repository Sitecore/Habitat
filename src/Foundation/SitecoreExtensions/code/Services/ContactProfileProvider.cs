namespace Sitecore.Foundation.SitecoreExtensions.Services
{
  using System;
  using System.Collections.Generic;
  using Sitecore.Analytics;
  using Sitecore.Analytics.Model.Entities;
  using Sitecore.Analytics.Model.Framework;
  using Sitecore.Analytics.Tracking;
  using Sitecore.Configuration;
  using Sitecore.Data;
  using Sitecore.Diagnostics;

  public class ContactProfileProvider : IContactProfileProvider
  {
    private Contact contact;

    private readonly ContactManager contactManager;

    public ContactProfileProvider()
    {
      contactManager = (ContactManager)Factory.CreateObject("tracking/contactManager", false);
    }

    public Contact Contact
    {
      get
      {
        if (!Tracker.IsActive)
        {
          return null;
        }

        if (contact == null)
        {
          contact = Tracker.Current.Contact;
        }

        return contact ?? (contact = contactManager?.CreateContact(ID.NewID));
      }
    }

    public IContactPicture Picture => GetFacet<IContactPicture>("Picture");
    public IContactPreferences Preferences => GetFacet<IContactPreferences>("Preferences");

    public Analytics.Tracking.KeyBehaviorCache KeyBehaviorCache
    {
      get
      {
        try
        {
          return Contact?.GetKeyBehaviorCache();
        }
        catch (Exception e)
        {
          Log.Warn(e.Message, e, this);
          return null;
        }
      }
    }

    public IContactPersonalInfo PersonalInfo => GetFacet<IContactPersonalInfo>("Personal");
    public IContactAddresses Addresses => GetFacet<IContactAddresses>("Addresses");
    public IContactEmailAddresses Emails => GetFacet<IContactEmailAddresses>("Emails");
    public IContactCommunicationProfile CommunicationProfile => GetFacet<IContactCommunicationProfile>("Communication Profile");
    public IContactPhoneNumbers PhoneNumbers => GetFacet<IContactPhoneNumbers>("Phone Numbers");
    public IEnumerable<IBehaviorProfileContext> BehaviorProfiles => Contact.BehaviorProfiles.Profiles;
    public Contact Flush()
    {
      return contactManager?.FlushContactToXdb2(Contact);
    }

    protected T GetFacet<T>(string facetName) where T : class, IFacet
    {
      return Contact?.GetFacet<T>(facetName);
    }
  }
}