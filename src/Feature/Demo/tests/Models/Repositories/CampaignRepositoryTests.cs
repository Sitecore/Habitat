namespace Sitecore.Feature.Demo.Tests.Models.Repositories
{
  using System;
  using FluentAssertions;
  using NSubstitute;
  using Sitecore.Analytics;
  using Sitecore.Analytics.Tracking;
  using Sitecore.Feature.Demo.Models.Repository;
  using Sitecore.Foundation.Testing.Attributes;
  using Xunit;

  public class CampaignRepositoryTests
  {
    [Theory]
    [AutoDbData]
    public void GetCurrent_NoCampaign_RetrunNull(CurrentInteraction currentInteraction, ITracker tracker)
    {
      //Arrange
      tracker.Interaction.Returns(currentInteraction);
      tracker.Interaction.CampaignId = null;
      var campaignRepository = new CampaignRepository();

      using (new TrackerSwitcher(tracker))
      {
        //Act
        var result = campaignRepository.GetCurrent();
        //Assert     
        result.Should().BeNull();
      }
    }
  }
}
