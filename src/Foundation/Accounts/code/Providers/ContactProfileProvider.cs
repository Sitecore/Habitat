﻿namespace Sitecore.Foundation.Accounts.Providers
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
      this.contactManager = (ContactManager)Factory.CreateObject("tracking/contactManager", false);
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

        return this.contact ?? (this.contact = this.contactManager?.CreateContact(ID.NewID));
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
      return this.contactManager?.FlushContactToXdb2(this.Contact);
    }

    protected T GetFacet<T>(string facetName) where T : class, IFacet
    {
      return this.Contact?.GetFacet<T>(facetName);
    }
  }
}