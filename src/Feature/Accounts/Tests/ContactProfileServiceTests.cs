namespace Sitecore.Feature.Accounts.Tests
{
  using System;
  using FluentAssertions;
  using NSubstitute;
  using Ploeh.AutoFixture.Xunit2;
  using Sitecore.Analytics.Model.Entities;
  using Sitecore.Analytics.Tracking;
  using Sitecore.Feature.Accounts.Models;
  using Sitecore.Feature.Accounts.Services;
  using Sitecore.Feature.Accounts.Tests.Extensions;
  using Sitecore.Foundation.Accounts.Providers;
  using Sitecore.Foundation.SitecoreExtensions.Services;
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
    public void ShouldSetPreferredEmail([Frozen] IContactProfileProvider contactProfileProvider, [Greedy] ContactProfileService contactProfileService, string email, IEmailAddress emailAddress)
    {
      contactProfileService.SetPreferredEmail(emailAddress.SmtpAddress);
      contactProfileProvider.Emails.Entries[contactProfileProvider.Emails.Preferred].SmtpAddress.ShouldBeEquivalentTo(emailAddress.SmtpAddress);
    }

    [Theory]
    [AutoDbData]
    public void SetTag_NotEmptyStringParameters_ShouldCallSetOnTagsPropertyOfContactWithSameParameters([Frozen] IContactProfileProvider contactProfileProvider, [Greedy] ContactProfileService contactProfileService, string tagName, string tagValue)
    {
      contactProfileService.SetTag(tagName, tagValue);
      contactProfileProvider.Contact.Tags.Received().Set(tagName, tagValue);
    }

    [Theory]
    [AutoDbData]
    public void SetTag_TagValueParameterIsEmpty_ShouldNotCallSetOnTagsPropertyOfContact([Frozen] IContactProfileProvider contactProfileProvider, [Greedy] ContactProfileService contactProfileService, string tagName)
    {
      contactProfileService.SetTag(tagName, null);
      contactProfileProvider.Contact.Tags.DidNotReceive().Set(tagName, null);
    }
  }
}
