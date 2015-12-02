using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Habitat.Accounts.Services
{
  using System.Runtime.CompilerServices;
  using System.Web.DynamicData;
  using Habitat.Accounts.Models;
  using Sitecore;
  using Sitecore.Analytics;
  using Sitecore.Analytics.Data;
  using Sitecore.Analytics.Model.Entities;
  using Sitecore.Analytics.Model.Framework;
  using Sitecore.Analytics.Tracking;
  using Sitecore.Analytics.Tracking.SharedSessionState;
  using Sitecore.Configuration;
  using Sitecore.Data;

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

    public IContactPersonalInfo PersonalInfo => this.GetFacet<IContactPersonalInfo>("Personal");

    public IContactAddresses Adresses { get; }

    public IContactEmailAddresses Emails => this.GetFacet<IContactEmailAddresses>("Emails");

    public IContactCommunicationProfile CommunicationProfile => this.GetFacet<IContactCommunicationProfile>("Communication Profile");

    public IContactPhoneNumbers PhoneNumbers => this.GetFacet<IContactPhoneNumbers>("Phone Numbers");

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