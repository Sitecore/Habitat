using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Habitat.Accounts.Tests
{
  using FluentAssertions;
  using Habitat.Accounts.Models;
  using Habitat.Accounts.Services;
  using Habitat.Accounts.Tests.Extensions;
  using NSubstitute;
  using Ploeh.AutoFixture.Xunit2;
  using Sitecore.Analytics.Model.Entities;
  using Xunit;

  public class ContactProfileServiceTests
  {
    [Theory]
    [AutoDbData]
    public void ShouldSetContactProfile([Frozen] IContactProfileProvider contactProfileProvider, string key, IPhoneNumber phoneNumber, string firstName, string lastName, IContactPersonalInfo personalInfo, Sitecore.Analytics.Tracking.Contact contact)
    {
      contactProfileProvider.Contact.Returns(contact);
      var contactProfileService = new ContactProfileService(contactProfileProvider);
      contactProfileProvider.PersonalInfo.Returns(personalInfo);
      var profile = new EditProfile();
      profile.FirstName = firstName;
      profile.LastName = lastName;
      profile.PhoneNumber = phoneNumber.Number;
      contactProfileService.SetProfile(profile);
      contactProfileProvider.PersonalInfo.FirstName.ShouldBeEquivalentTo(profile.FirstName);
      contactProfileProvider.PersonalInfo.Surname.ShouldBeEquivalentTo(profile.LastName);
      contactProfileProvider.PhoneNumbers.Entries[contactProfileProvider.PhoneNumbers.Preferred].Number.ShouldBeEquivalentTo(profile.PhoneNumber);
    }

    [Theory]
    [AutoDbData]
    public void ShouldSetPreferredEmail([Frozen] IContactProfileProvider contactProfileProvider, [NoAutoProperties] [Greedy] ContactProfileService contactProfileService, string email, IEmailAddress emailAddress)
    {
      contactProfileService.SetPreferredEmail(emailAddress.SmtpAddress);
      contactProfileProvider.Emails.Entries[contactProfileProvider.Emails.Preferred].SmtpAddress.ShouldBeEquivalentTo(emailAddress.SmtpAddress);
    }
  }
}
