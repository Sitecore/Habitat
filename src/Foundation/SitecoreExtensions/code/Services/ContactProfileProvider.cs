namespace Sitecore.Foundation.SitecoreExtensions.Services
{
  using System;
  using System.Collections.Generic;
  using System.Linq.Expressions;
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

    private ContactManager contactManager;

    public ContactProfileProvider()
    {
      try
      {
        this.contactManager = Factory.CreateObject("tracking/contactManager", true) as ContactManager;
      }
      catch
      {
        Sitecore.Diagnostics.Log.Error("Contact manager cannot be created", this);
      }
    }
    public Contact Contact
    {
      get
      {
        if (!Tracker.IsActive)
        {
          return null;
        }

        if (this.contact == null)
        {
          this.contact = Tracker.Current.Contact;
        }

        if (this.contact == null)
        {
          this.contact = this.contactManager.CreateContact(ID.NewID);
        }

        return this.contact;
      }
    }

    public IContactPicture Picture => this.GetFacet<IContactPicture>("Picture");

    public IContactPreferences Preferences => this.GetFacet<IContactPreferences>("Preferences");

    public Analytics.Tracking.KeyBehaviorCache KeyBehaviorCache
    {

      get
      {
        try
        {
          return this.Contact?.GetKeyBehaviorCache();
        }

        catch (Exception e)
        {
          Log.Warn(e.Message, e, this);
          return null;
        }
      }
    }

    public IContactPersonalInfo PersonalInfo => this.GetFacet<IContactPersonalInfo>("Personal");

    public IContactAddresses Addresses => this.GetFacet<IContactAddresses>("Addresses");

    public IContactEmailAddresses Emails => this.GetFacet<IContactEmailAddresses>("Emails");

    public IContactCommunicationProfile CommunicationProfile => this.GetFacet<IContactCommunicationProfile>("Communication Profile");

    public IContactPhoneNumbers PhoneNumbers => this.GetFacet<IContactPhoneNumbers>("Phone Numbers");
    public IEnumerable<IBehaviorProfileContext> BehaviorProfiles => this.Contact.BehaviorProfiles.Profiles;

    public Contact Flush()
    {
      return this.contactManager.FlushContactToXdb2(this.Contact);
    }

    protected T GetFacet<T>(string facetName) where T : class, IFacet
    {
      if (this.Contact != null)
      {
        return this.Contact.GetFacet<T>(facetName);
      }

      return null;
    }
  }
}