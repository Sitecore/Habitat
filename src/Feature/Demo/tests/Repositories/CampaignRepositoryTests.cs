namespace Sitecore.Feature.Demo.Tests.Repositories
{
  using FluentAssertions;
  using NSubstitute;
  using Sitecore.Analytics;
  using Sitecore.Analytics.Tracking;
  using Sitecore.Feature.Demo.Repositories;
  using Sitecore.Foundation.Testing.Attributes;
  using Sitecore.Marketing.Definitions;
  using Sitecore.Marketing.Definitions.Campaigns;
  using Sitecore.Marketing.Taxonomy;
  using Xunit;

  public class CampaignRepositoryTests
  {
    [Theory]
    [AutoDbData]
    public void GetCurrent_NoCampaign_ReturnsNull(CurrentInteraction currentInteraction, ITracker tracker, ITaxonomyManagerProvider taxonomyManagerProvider, IDefinitionManager<ICampaignActivityDefinition> definitionManager)
    {
      //Arrange
      tracker.Interaction.Returns(currentInteraction);
      tracker.Interaction.CampaignId = null;
      var campaignRepository = new CampaignRepository(taxonomyManagerProvider, definitionManager);

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
