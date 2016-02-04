namespace Sitecore.Feature.Demo.Tests.Controllers
{
  using System.Web.Mvc;
  using FluentAssertions;
  using NSubstitute;
  using Sitecore.Analytics;
  using Sitecore.Analytics.Tracking;
  using Sitecore.Feature.Demo.Controllers;
  using Sitecore.Feature.Demo.Models;
  using Sitecore.Feature.Demo.Services;
  using Sitecore.Foundation.SitecoreExtensions.Services;
  using Sitecore.Foundation.Testing.Attributes;
  using Xunit;

  public class DemoControllerTests
  {
    [Theory]
    [AutoDbData]
    public void VisitDetails_TrackerInteractionNotInitialized_ShouldReturnNull(IContactProfileProvider contact, IProfileProvider profile, ITracker tracker)
    {
      //arrange
      var controller = new DemoController(contact, profile);
      using (new TrackerSwitcher(tracker))
      {
        controller.VisitDetails().Should().Be(null);
      }
    }

    [Theory]
    [AutoDbData]
    public void VisitDetails_TrackerInitialized_ShouldReturnVisitInformation(IContactProfileProvider contact, IProfileProvider profile, ITracker tracker, CurrentInteraction interaction)
    {
      tracker.Interaction.Returns(interaction);
      //arrange
      var controller = new DemoController(contact, profile);
      using (new TrackerSwitcher(tracker))
      {
        controller.VisitDetails().As<ViewResult>().Model.Should().BeOfType<VisitInformation>();
      }
    }
  }
}