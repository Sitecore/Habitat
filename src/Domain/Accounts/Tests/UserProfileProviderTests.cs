namespace Habitat.Accounts.Tests
{
  using System.Collections.Generic;
  using Habitat.Accounts.Services;
  using Extensions;
  using FluentAssertions;
  using NSubstitute;
  using Ploeh.AutoFixture;
  using Sitecore.FakeDb;
  using Sitecore.FakeDb.AutoFixture;
  using Sitecore.Security;
  using Xunit;

  public class UserProfileProviderTests
  {
    [Theory, AutoDbDataWithExtension(typeof(UserProfileCustomization))]
    public void GetCustomPropertiesShouldReturnProperties([CoreDb]Db db, [Content]ProfileTemplate template, [Content]DbItem profileItem, UserProfileProvider userProfileProvider, UserProfile userProfile)
    {
      userProfileProvider.GetCustomProperties(userProfile).Should().ContainKeys("FirstName", "LastName", "Phone");
    }

    [Theory, AutoDbDataWithExtension(typeof(UserProfileCustomization))]
    public void GetCustomPropertiesShouldReturnEmptyProperties([CoreDb]Db db, [Content]ProfileTemplate template, DbItem profileItem, UserProfileProvider userProfileProvider, UserProfile userProfile)
    {
      userProfileProvider.GetCustomProperties(userProfile).Should().BeEmpty();
    }

    [Theory, AutoDbDataWithExtension(typeof(UserProfileCustomization))]
    public void SetCustomProfileShouldSetProperties([CoreDb]Db db, UserProfileProvider userProfileProvider, UserProfile userProfile, IDictionary<string,string> properties)
    {
      userProfileProvider.SetCustomProfile(userProfile, properties);
      foreach (var property in properties)
      {
        userProfile.Received()[property.Key] = property.Value;
      }
    }

    [Theory, AutoDbDataWithExtension(typeof(UserProfileCustomization))]
    public void SetCustomProfileShouldSetEmptyProperties([CoreDb]Db db, UserProfileProvider userProfileProvider, UserProfile userProfile, IDictionary<string, string> properties, string nullKey)
    {
      properties.Add(nullKey, null);

      userProfileProvider.SetCustomProfile(userProfile, properties);
      userProfile.Received()[nullKey] = string.Empty;
    }

    [Theory, AutoDbDataWithExtension(typeof(UserProfileCustomization))]
    public void SetCustomProfileShouldSaveProfile([CoreDb]Db db, UserProfileProvider userProfileProvider, UserProfile userProfile, IDictionary<string, string> properties)
    {
      userProfileProvider.SetCustomProfile(userProfile, properties);
      userProfile.Received().Save();
    }

    public class ProfileTemplate : DbTemplate
    {
      public ProfileTemplate()
      {
        this.Add("FirstName");
        this.Add("LastName");
        this.Add("Phone");
      }
    }

    public class UserProfileCustomization : ICustomization
    {
      public void Customize(IFixture fixture)
      {
        fixture.Freeze<ProfileTemplate>();
        fixture.Freeze<DbItem>(c => c.With(x => x.TemplateID, fixture.Create<ProfileTemplate>().ID));

        fixture.Register<ProfileTemplate, DbItem, UserProfile>((profileTemplate, dbItem) =>
         {
           var profile = Substitute.For<UserProfile>();
           profile.ProfileItemId.Returns(dbItem.ID.ToString());
           foreach (var field in profileTemplate.Fields)
           {
             profile[Arg.Is(field.Name)].Returns(field.Name);
           }

           return profile;
         });
      }
    }
  }
}
