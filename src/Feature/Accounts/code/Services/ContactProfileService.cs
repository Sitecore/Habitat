namespace Sitecore.Feature.Accounts.Services
{
  using Sitecore;
  using Sitecore.Feature.Accounts.Models;
  using Sitecore.Foundation.SitecoreExtensions.Services;

  public class ContactProfileService : IContactProfileService
  {
    private const string PrimaryEmailKey = "Primary";
    private const string PrimaryPhoneKey = "Primary";
    private readonly IContactProfileProvider contactProfileProvider;

    public ContactProfileService() : this(new ContactProfileProvider())
    {
    }

    public ContactProfileService(IContactProfileProvider contactProfileProvider)
    {
      this.contactProfileProvider = contactProfileProvider;
    }

    public void SetPreferredEmail(string email)
    {
      var emails = this.contactProfileProvider.Emails;
      if (emails != null)
      {
        emails.Preferred = PrimaryEmailKey;
        if (emails.Entries.Contains(PrimaryEmailKey))
        {
          emails.Entries[PrimaryEmailKey].SmtpAddress = Context.User.Profile.Email;
        }
        else
        {
          emails.Entries.Create(PrimaryEmailKey).SmtpAddress = Context.User.Profile.Email;
        }
      }

      this.contactProfileProvider.Flush();
    }

    public void SetPreferredPhoneNumber(string phone)
    {
      var phones = this.contactProfileProvider.PhoneNumbers;
      if (phones != null)
      {
        phones.Preferred = PrimaryPhoneKey;
        if (phones.Entries.Contains(PrimaryPhoneKey))
        {
          phones.Entries[PrimaryPhoneKey].Number = phone;
        }
        else
        {
          phones.Entries.Create(PrimaryPhoneKey).Number = phone;
        }
      }
    }

    public void SetProfile(EditProfile editProfileModel)
    {
      if (this.contactProfileProvider.Contact != null)
      {
        var personalInfo = this.contactProfileProvider.PersonalInfo;
        personalInfo.FirstName = editProfileModel.FirstName;
        personalInfo.Surname = editProfileModel.LastName;
        this.contactProfileProvider.Contact.Tags.Set("Interests", editProfileModel.Interest);
        this.SetPreferredPhoneNumber(editProfileModel.PhoneNumber);
        this.SetPreferredEmail(Context.User.Profile.Email);
        this.contactProfileProvider.Flush();
      }
    }
  }
}