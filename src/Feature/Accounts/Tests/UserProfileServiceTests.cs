namespace Sitecore.Feature.Accounts.Tests
{
  using System.Collections.Generic;
  using System.Linq;
  using System.Web;
  using System.Web.Mvc;
  using FluentAssertions;
  using NSubstitute;
  using Ploeh.AutoFixture.AutoNSubstitute;
  using Ploeh.AutoFixture.Xunit2;
  using Sitecore.Data;
  using Sitecore.Data.Items;
  using Sitecore.FakeDb;
  using Sitecore.Feature.Accounts.Models;
  using Sitecore.Feature.Accounts.Services;
  using Sitecore.Feature.Accounts.Tests.Extensions;
  using Sitecore.Foundation.Dictionary.Repositories;
  using Sitecore.Foundation.Testing;
  using Sitecore.Foundation.Testing.Attributes;
  using Sitecore.Security;
  using Xunit;

  public class UserProfileServiceTests
  {
    public UserProfileServiceTests()
    {
      HttpContext.Current = HttpContextMockFactory.Create();
      HttpContext.Current.Items["DictionaryPhraseRepository.Current"] = Substitute.For<IDictionaryPhraseRepository>();
    }

    [Theory, AutoDbDataWithExtension(typeof(AutoConfiguredNSubstituteCustomization))]
    public void GetUserDefaultProfileIdShouldReturnId([Frozen] Item item, IProfileSettingsService profileSettingsService, IUserProfileProvider userProfileProvider, [Greedy] UserProfileService userProfileService)
    {
      userProfileService.GetUserDefaultProfileId().Should().Be(item.ID.ToString());
    }

    [Theory, AutoDbData]
    public void GetUserDefaultProfileIdShouldReturnNull(IProfileSettingsService profileSettingsService, IUserProfileProvider userProfileProvider, [Greedy] UserProfileService userProfileService)
    {
      userProfileService.GetUserDefaultProfileId().Should().BeNull();
    }

    [Theory, AutoDbDataWithExtension(typeof(AutoConfiguredNSubstituteCustomization))]
    public void GetEmptyProfileShouldReturnEditProfileWithInterests([Frozen] IEnumerable<string> interests, IProfileSettingsService profileSettingsService, IUserProfileProvider userProfileProvider, [Greedy] UserProfileService userProfileService)
    {
      var profile = userProfileService.GetEmptyProfile();

      profile.Should().NotBeNull();
      profile.FirstName.Should().BeNull();
      profile.LastName.Should().BeNull();
      profile.Interest.Should().BeNull();
      profile.PhoneNumber.Should().BeNull();
      profile.InterestTypes.Should().BeEquivalentTo(interests);
    }

    [Theory, AutoDbDataWithExtension(typeof(AutoConfiguredNSubstituteCustomization))]
    public void GetInterestsShouldReturnInterests([Frozen] IEnumerable<string> interests, IProfileSettingsService profileSettingsService, IUserProfileProvider userProfileProvider, [Greedy] UserProfileService userProfileService)
    {
      userProfileService.GetInterests().Should().BeEquivalentTo(interests);
    }

    [Theory, AutoDbDataWithExtension]
    public void GetProfileShouldReturnFullEditProfileModel(Db db, [Substitute] UserProfile userProfile, string username, [RightKeys("FirstName", "LastName", "Phone", "Interest")] IDictionary<string, string> properties,
      [Frozen] IProfileSettingsService profileSettingsService, [Frozen] IUserProfileProvider userProfileProvider, [Greedy] UserProfileService userProfileService)
    {
      var id = new ID();
      db.Add(new DbItem("Profile", id)
      {
        Fields =
          {
            new DbField("FirstName", Templates.UserProfile.Fields.FirstName),
            new DbField("LastName", Templates.UserProfile.Fields.LastName),
            new DbField("Phone", Templates.UserProfile.Fields.PhoneNumber),
            new DbField("Interest", Templates.UserProfile.Fields.Interest)
          }
      });
      profileSettingsService.GetUserDefaultProfile().Returns(db.GetItem(id));
      userProfileProvider.GetCustomProperties(Arg.Any<UserProfile>()).Returns(properties);

      var user = Substitute.For<Sitecore.Security.Accounts.User>($@"extranet\{username}", true);
      user.Profile.Returns(userProfile);
      var result = userProfileService.GetProfile(user);
      result.FirstName.Should().Be(properties["FirstName"]);
      result.LastName.Should().Be(properties["LastName"]);
      result.PhoneNumber.Should().Be(properties["Phone"]);
      result.Interest.Should().Be(properties["Interest"]);
    }

    [Theory, AutoDbDataWithExtension]
    public void SetProfileShouldUpdateUserProfile(Db db, [Substitute] UserProfile userProfile, [Substitute]EditProfile editProfile, [Frozen] IProfileSettingsService profileSettingsService, [Frozen] IUserProfileProvider userProfileProvider, [Greedy] UserProfileService userProfileService)
    {
      var id = new ID();
      db.Add(new DbItem("Profile", id)
      {
        Fields =
          {
            new DbField("FirstName", Templates.UserProfile.Fields.FirstName),
            new DbField("LastName", Templates.UserProfile.Fields.LastName),
            new DbField("Phone", Templates.UserProfile.Fields.PhoneNumber),
            new DbField("Interest", Templates.UserProfile.Fields.Interest)
          }
      });
      profileSettingsService.GetUserDefaultProfile().Returns(db.GetItem(id));
      userProfileService.SaveProfile(userProfile, editProfile);

      userProfileProvider.Received(1).SetCustomProfile(userProfile, Arg.Is<IDictionary<string, string>>(
        x => x["FirstName"] == editProfile.FirstName &&
             x["LastName"] == editProfile.LastName &&
             x["Interest"] == editProfile.Interest &&
             x["Phone"] == editProfile.PhoneNumber));
    }
  }
}
