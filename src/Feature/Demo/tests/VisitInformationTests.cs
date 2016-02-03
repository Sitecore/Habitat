namespace Sitecore.Feature.Demo.Tests
{
  using System.Linq;
  using FluentAssertions;
  using NSubstitute;
  using Sitecore.Analytics;
  using Sitecore.Analytics.Data.Items;
  using Sitecore.Collections;
  using Sitecore.Data;
  using Sitecore.Data.Items;
  using Sitecore.FakeDb.AutoFixture;
  using Sitecore.FakeDb.Sites;
  using Sitecore.Feature.Demo.Models;
  using Sitecore.Feature.Demo.Services;
  using Sitecore.Foundation.Testing.Attributes;
  using Sitecore.Sites;
  using Xunit;

  public class VisitInformationTests
  {
    [Theory]
    [AutoDbData]
    public void LoadProfiles_TrackerInactive_ShouldReturnEmptyEnumerable(ITracker tracker)
    {
      //arrange
      tracker.IsActive.Returns(false);
      using (new TrackerSwitcher(tracker))
      {
        var model = new VisitInformation(null);
        //act
        var profiles = model.LoadProfiles();
        //assert
        profiles.Count().Should().Be(0);
      }
    }


    [Theory]
    [AutoDbData]
    public void LoadProfiles_NoSetProfiles_ShouldReturnEmptyProfilesEnumerable(Database db, [Content] Item item, ITracker tracker, IProfileProvider provider)
    {
      //arrange
      tracker.IsActive.Returns(true);

      var fakeSiteContext = new FakeSiteContext(new StringDictionary
      {
        {
          "rootPath", "/sitecore"
        },
        {
          "startItem", item.Paths.FullPath.Remove(0, "/sitecore".Length)
        }
      });
      fakeSiteContext.Database = db;

      using (new SiteContextSwitcher(fakeSiteContext))
      {
        using (new TrackerSwitcher(tracker))
        {
          var model = new VisitInformation(provider);
          model.LoadProfiles().Count().Should().Be(0);
        }
      }
    }


    [Theory]
    [AutoProfileDbData]
    public void LoadProfiles_SetProfiles_ShouldReturnExistentProfilesEnumerable([Content] Item item, PatternMatch patternMatch, ITracker tracker, IProfileProvider provider)
    {
      //arrange
      tracker.IsActive.Returns(true);

      var profileItem = item.Add("profile", new TemplateID(ProfileItem.TemplateID));
      using (new EditContext(profileItem))
      {
        profileItem.Fields[ProfileItem.FieldIDs.Type].Value = "Average";
        profileItem.Fields[ProfileItem.FieldIDs.NameField].Value = profileItem.Name;
      }

      provider.GetSiteProfiles().Returns(new[]{new ProfileItem(profileItem)});
      provider.HasMatchingPattern(null).ReturnsForAnyArgs(true);
      provider.GetPatternsWithGravityShare(null).ReturnsForAnyArgs(new [] { patternMatch });


      var fakeSiteContext = new FakeSiteContext("fake")
      {
        Database = item.Database
      };

      using (new SiteContextSwitcher(fakeSiteContext))
      {
        using (new TrackerSwitcher(tracker))
        {
          var model = new VisitInformation(provider);
          //act
          var profiles = model.LoadProfiles().ToArray();

          //assert
          profiles.Count().Should().Be(1);
          profiles.First().Name.Should().Be(profileItem.Name);
          profiles.First().PatternMatches.Single().Should().Be(patternMatch);
        }
      }
    }

    [Theory]
    [AutoProfileDbData]
    public void LoadProfiles_NoMatchingProfile_ShouldReturnEmptyProfilesEnumerable([Content] Item item, ITracker tracker, IProfileProvider provider)
    {
      //arrange
      tracker.IsActive.Returns(true);
      var profileItem = item.Add("profile", new TemplateID(ProfileItem.TemplateID));
      provider.GetSiteProfiles().Returns(new[]
      {
        new ProfileItem(profileItem)
      });
      provider.HasMatchingPattern(null).ReturnsForAnyArgs(false);


      using (new TrackerSwitcher(tracker))
      {
        var model = new VisitInformation(provider);
        //act
        var profiles = model.LoadProfiles().ToArray();

        //assert
        profiles.Length.Should().Be(0);
      }
    }
  }
}