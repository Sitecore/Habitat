namespace Sitecore.Feature.Demo.Tests.Services
{
  using System;
  using System.Linq;
  using FluentAssertions;
  using NSubstitute;
  using Sitecore.Analytics;
  using Sitecore.Analytics.Data.Items;
  using Sitecore.Analytics.Tracking;
  using Sitecore.Collections;
  using Sitecore.Data;
  using Sitecore.Data.Items;
  using Sitecore.FakeDb;
  using Sitecore.FakeDb.AutoFixture;
  using Sitecore.FakeDb.Sites;
  using Sitecore.Feature.Demo.Services;
  using Sitecore.Foundation.Testing.Attributes;
  using Sitecore.Sites;
  using Xunit;

  public class ProfileProviderTests
  {
    [Theory]
    [AutoProfileDbData]
    public void LoadProfiles_SettingWithProfiles_ShouldReturnExistentProfilesEnumerable([Content] Item item, CurrentInteraction currentInteraction, ITracker tracker, Profile profile)
    {
      var profileSettingItem = item.Add("profileSetting", new TemplateID(Templates.ProfilingSettings.ID));
      var profileItem = item.Add("profile", new TemplateID(ProfileItem.TemplateID));
      using (new EditContext(profileSettingItem))
      {
        profileSettingItem.Fields[Templates.ProfilingSettings.Fields.SiteProfiles].Value = profileItem.ID.ToString();
      }

      var provider = new ProfileProvider();

      var fakeSiteContext = new FakeSiteContext(new StringDictionary
      {
        {
          "rootPath", "/sitecore"
        },
        {
          "startItem", profileSettingItem.Paths.FullPath.Remove(0, "/sitecore".Length)
        }
      });

      fakeSiteContext.Database = item.Database;

      using (new SiteContextSwitcher(fakeSiteContext))
      {
        var siteProfiles = provider.GetSiteProfiles();
        siteProfiles.Count().Should().Be(1);
      }
    }


    [Theory]
    [AutoProfileDbData]
    public void LoadProfiles_SettingsIsEmpty_ShouldReturnExistentProfilesEnumerable([Content] Item item, CurrentInteraction currentInteraction, ITracker tracker, Profile profile)
    {
      var profileSettingItem = item.Add("profileSetting", new TemplateID(Templates.ProfilingSettings.ID));
      var profileItem = item.Add("profile", new TemplateID(ProfileItem.TemplateID));


      var provider = new ProfileProvider();

      var fakeSiteContext = new FakeSiteContext(new StringDictionary
      {
        {
          "rootPath", "/sitecore"
        },
        {
          "startItem", profileSettingItem.Paths.FullPath.Remove(0, "/sitecore".Length)
        }
      });

      fakeSiteContext.Database = item.Database;

      using (new SiteContextSwitcher(fakeSiteContext))
      {
        provider.GetSiteProfiles().Count().Should().Be(0);
      }
    }

    [Theory]
    [AutoProfileDbData]
    public void LoadProfiles_SettingsNotExists_ShouldReturnExistentProfilesEnumerable([Content] Item item)
    {
      var fakeSiteContext = new FakeSiteContext(new StringDictionary
      {
        {
          "rootPath", "/sitecore"
        },
        {
          "startItem", item.Paths.FullPath.Remove(0, "/sitecore".Length)
        }
      });

      fakeSiteContext.Database = item.Database;

      using (new SiteContextSwitcher(fakeSiteContext))
      {
        var provider = new ProfileProvider();
        provider.GetSiteProfiles().Count().Should().Be(0);
      }
    }


    [Theory]
    [AutoProfileDbData]
    public void HasMatchingPattern_ItemNotExists_ShouldReturnFalse([Content] Item profileItem, CurrentInteraction currentInteraction, ITracker tracker, Profile profile)
    {
      tracker.Interaction.Returns(currentInteraction);
      currentInteraction.Profiles[null].ReturnsForAnyArgs(profile);
      profile.PatternId = Guid.NewGuid();


      var fakeSiteContext = new FakeSiteContext("fake")
      {
        Database = Database.GetDatabase("master")
      };


      using (new TrackerSwitcher(tracker))
      {
        using (new SiteContextSwitcher(fakeSiteContext))
        {
          var provider = new ProfileProvider();
          provider.HasMatchingPattern(new ProfileItem(profileItem), ProfilingTypes.Active).Should().BeFalse();
        }
      }
    }


    [Theory]
    [AutoProfileDbData]
    public void HasMatchingPattern_ItemExists_ShouldReturnTrue([Content] Item profileItem, CurrentInteraction currentInteraction, ITracker tracker, Profile profile)
    {
      tracker.Interaction.Returns(currentInteraction);
      currentInteraction.Profiles[null].ReturnsForAnyArgs(profile);

      var pattern = profileItem.Add("fakePattern", new TemplateID(PatternCardItem.TemplateID));
      profile.PatternId = pattern.ID.Guid;
      var fakeSiteContext = new FakeSiteContext("fake")
      {
        Database = Database.GetDatabase("master")
      };


      using (new TrackerSwitcher(tracker))
      {
        using (new SiteContextSwitcher(fakeSiteContext))
        {
          var provider = new ProfileProvider();
          provider.HasMatchingPattern(new ProfileItem(profileItem), ProfilingTypes.Active).Should().BeTrue();
        }
      }
    }


    [Theory]
    [AutoProfileDbData]
    public void HasMatchingPattern_TrackerReturnsNull_ShouldReturnFalse([Content] Item profileItem, CurrentInteraction currentInteraction, ITracker tracker, Profile profile)
    {
      tracker.Interaction.Returns(currentInteraction);
      currentInteraction.Profiles[null].ReturnsForAnyArgs((Profile)null);


      var fakeSiteContext = new FakeSiteContext("fake")
      {
        Database = Database.GetDatabase("master")
      };


      using (new TrackerSwitcher(tracker))
      {
        using (new SiteContextSwitcher(fakeSiteContext))
        {
          var provider = new ProfileProvider();
          provider.HasMatchingPattern(new ProfileItem(profileItem), ProfilingTypes.Active).Should().BeFalse();
        }
      }
    }


    [Theory]
    [AutoProfileDbData]
    public void HasMatchingPattern_TrackerReturnsProfileWithoutID_ShouldReturnFalse([Content] Item profileItem, CurrentInteraction currentInteraction, ITracker tracker, Profile profile)
    {
      tracker.Interaction.Returns(currentInteraction);
      currentInteraction.Profiles[null].ReturnsForAnyArgs(profile);
      profile.PatternId = null;

      var fakeSiteContext = new FakeSiteContext("fake")
      {
        Database = Database.GetDatabase("master")
      };


      using (new TrackerSwitcher(tracker))
      {
        using (new SiteContextSwitcher(fakeSiteContext))
        {
          var provider = new ProfileProvider();
          provider.HasMatchingPattern(new ProfileItem(profileItem), ProfilingTypes.Active).Should().BeFalse();
        }
      }
    }

    [Theory]
    [AutoProfileDbData]
    public void HasMatchingPattern_HistoricProfileProfileNotExitsts_ReturnFalse([Content] Item profileItem, Contact contact, ITracker tracker, Profile profile)
    {
      //Arrange
      tracker.Contact.Returns(contact);
      contact.BehaviorProfiles[Arg.Is(profileItem.ID)].Returns(x => null);

      var fakeSiteContext = new FakeSiteContext("fake")
      {
        Database = Database.GetDatabase("master")
      };
      //Act
      using (new TrackerSwitcher(tracker))
      {
        using (new SiteContextSwitcher(fakeSiteContext))
        {
          var provider = new ProfileProvider();
          var result = provider.HasMatchingPattern(new ProfileItem(profileItem), ProfilingTypes.Historic);
          //Assert 
          result.Should().BeFalse();
        }
      }
    }

    [Theory]
    [AutoProfileDbData]
    public void HasMatchingPattern_HistoricProfileAndItemExists_ShouldReturnTrue([Content] Item profileItem, Contact contact, ITracker tracker, Profile profile)
    {
      //Arrange
      tracker.Contact.Returns(contact);
      var behaviorPattern = Substitute.For<IBehaviorProfileContext>();
      behaviorPattern.PatternId.Returns(profileItem.ID);
      contact.BehaviorProfiles[Arg.Is(profileItem.ID)].Returns(behaviorPattern);

      var pattern = profileItem.Add("fakePattern", new TemplateID(PatternCardItem.TemplateID));
      profile.PatternId = pattern.ID.Guid;
      var fakeSiteContext = new FakeSiteContext("fake")
      {
        Database = Database.GetDatabase("master")
      };


      using (new TrackerSwitcher(tracker))
      {
        using (new SiteContextSwitcher(fakeSiteContext))
        {
          var provider = new ProfileProvider();
          provider.HasMatchingPattern(new ProfileItem(profileItem), ProfilingTypes.Historic).Should().BeTrue();
        }
      }
    }

    [Theory]
    [AutoDbData]
    public void GetPatternsWithGravityShare_NullProfile_ThrowArgumentNullException(ProfilingTypes profilingTypes, ProfileProvider profileProvider)
    {
      //Act
      profileProvider.Invoking(x => x.GetPatternsWithGravityShare(null, profilingTypes)).ShouldThrow<ArgumentNullException>();
    }
  }
}