namespace Sitecore.Feature.Demo.Tests.Services
{
  using System.Collections;
  using NSubstitute;
  using Ploeh.AutoFixture.Xunit2;
  using Sitecore.Analytics.Data.Items;
  using Sitecore.Analytics.Tracking;
  using Sitecore.Data;
  using Sitecore.Data.Items;
  using Sitecore.FakeDb;
  using Sitecore.FakeDb.AutoFixture;
  using Sitecore.Feature.Demo.Services;
  using Sitecore.Foundation.Testing.Attributes;
  using System.Collections.Generic;
  using System.Linq;
  using FluentAssertions;
  using Sitecore.SecurityModel;
  using Xunit;

  public class BehaviorProfileDecoratorTests
  {
    [Theory]
    [AutoDbData]
    public void GetEnumerator_Call_ReturnScoresWithKeyName(Db db, ID keyId1, ID keyId2, DbItem profileItem, IBehaviorProfileContext behaviorProfile)
    {
      //Arrange
      using (new SecurityDisabler())
      {
        profileItem.Add(new DbItem("Key1", keyId1, ProfileKeyItem.TemplateID)
        {
          {ProfileKeyItem.FieldIDs.NameField,"key1name" }
        });
        profileItem.Add(new DbItem("Key2", keyId2, ProfileKeyItem.TemplateID)
        {
          {ProfileKeyItem.FieldIDs.NameField,"key2name" }
        });

        db.Add(profileItem);

        var item = db.GetItem(profileItem.FullPath);
        var profile = new ProfileItem(item);

        var behaviorScores = new List<KeyValuePair<ID, float>>() { new KeyValuePair<ID, float>(keyId1, 10), new KeyValuePair<ID, float>(keyId2, 20) };
        behaviorProfile.Scores.Returns(behaviorScores);
        var behaviorProfileDecorator = new BehaviorProfileDecorator(profile, behaviorProfile);

        //Act
        var result = behaviorProfileDecorator.ToList();

        //Assert      
        result.Should().BeEquivalentTo(new[] { new KeyValuePair<string, float>("key1name", 10), new KeyValuePair<string, float>("key2name", 20) });
      }
    }

    [Theory]
    [AutoDbData]
    public void GetCount_NumberOfTimesScored_ReturnNumberOfTimesScored(int numberOfTimesScored, IBehaviorProfileContext behaviorProfile)
    {
      //Arrange
      var behaviorProfileDecorator = new BehaviorProfileDecorator(null, behaviorProfile);
      behaviorProfile.NumberOfTimesScored.Returns(numberOfTimesScored);
      //Assert
      behaviorProfileDecorator.Count.Should().Be(numberOfTimesScored);
    }

    [Theory]
    [AutoDbData]
    public void Total_Total_ReturnTotal(int total, IBehaviorProfileContext behaviorProfile)
    {
      //Arrange
      var behaviorProfileDecorator = new BehaviorProfileDecorator(null, behaviorProfile);
      behaviorProfile.Total.Returns(total);
      //Assert
      behaviorProfileDecorator.Total.Should().Be(total);
    }

    [Theory]
    [AutoDbData]
    public void Indexer_NullProfileKey_ReturnZero([Content]Item profileItem, IBehaviorProfileContext behaviorProfile)
    {
      //Arrange
      var profile = new ProfileItem(profileItem);

      var behaviorProfileDecorator = new BehaviorProfileDecorator(profile, behaviorProfile);
      //Assert
      behaviorProfileDecorator["profileKey"].Should().Be(0);
    }
  }
}
