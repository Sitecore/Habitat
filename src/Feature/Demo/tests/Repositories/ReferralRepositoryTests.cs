namespace Sitecore.Feature.Demo.Tests.Repositories
{
    using System;
    using System.Linq;
    using System.Web;
    using FluentAssertions;
    using NSubstitute;
    using Ploeh.AutoFixture.Xunit2;
    using Sitecore.Analytics;
    using Sitecore.Analytics.Tracking;
    using Sitecore.Feature.Demo.Models;
    using Sitecore.Feature.Demo.Repositories;
    using Sitecore.Foundation.Testing.Attributes;
    using Xunit;

    public class ReferralRepositoryTests
    {
        [Theory]
        [AutoDbData]
        public void Get_Call_ShouldReturnReferringSite(string site, CurrentInteraction currentInteraction, ITracker tracker, ICampaignRepository campaignRepository, [Greedy] ReferralRepository referralRepository, HttpContext httpContext)
        {
            //Arrange
            HttpContext.Current = httpContext;
            tracker.Interaction.Returns(currentInteraction);
            tracker.Interaction.ReferringSite.Returns(site);

            using (new TrackerSwitcher(tracker))
            {
                //Act
                var referral = referralRepository.Get();
                //Assert      
                referral.ReferringSite.Should().Be(site);
            }
        }

        [Theory]
        [AutoDbData]
        public void Get_Call_ShouldCombineActiveAndHistoricCampaigns(string site, CurrentInteraction currentInteraction, ITracker tracker, [Frozen] ICampaignRepository campaignRepository, [Greedy] ReferralRepository referralRepository, HttpContext httpContext)
        {
            //Arrange
            HttpContext.Current = httpContext;
            tracker.Interaction.Returns(currentInteraction);
            tracker.Interaction.ReferringSite.Returns(site);

            campaignRepository.GetCurrent().Returns(new Campaign() {Title = "camp1"});
            campaignRepository.GetHistoric().Returns(new[] {new Campaign() {Title = "camp2"}, new Campaign() {Title = "camp3"}});

            using (new TrackerSwitcher(tracker))
            {
                //Act
                var referral = referralRepository.Get();
                //Assert      
                referral.TotalNoOfCampaigns.Should().Be(3);
                referral.Campaigns.Select(x => x.Title).Should().Contain(new[] {"camp1", "camp2", "camp3",});
            }
        }
    }
}