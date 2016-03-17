namespace Sitecore.Feature.Demo.Tests.Models.Repositories
{
  using FluentAssertions;
  using NSubstitute;
  using Sitecore.Analytics;
  using Sitecore.Analytics.Model;
  using Sitecore.Analytics.Tracking;
  using Sitecore.Feature.Demo.Models.Repository;
  using Sitecore.Foundation.Testing.Attributes;
  using Xunit;

  public class LocationRepositoryTests
  {
    [Theory]
    [AutoDbData]
    public void GetCurrent_HasNoGeoIpDAta_ReturnNull(ITracker tracker, CurrentInteraction interaction )
    {
      //Arrange
      interaction.HasGeoIpData.Returns(false);
      tracker.Interaction.Returns(interaction);
      var locationRepository = new LocationRepository();

      using (new TrackerSwitcher(tracker))
      {
        //Act
        var location = locationRepository.GetCurrent();
        //Assert      
        location.Should().BeNull();
      }
    }

    [Theory]
    [AutoDbData]
    public void GetCurrent_NullLatitude_ReturnNull(double? longitude, ITracker tracker, CurrentInteraction interaction, WhoIsInformation whoIsInformation)
    {
      //Arrange
      whoIsInformation.Longitude = longitude;
      whoIsInformation.Latitude = null;
      interaction.HasGeoIpData.Returns(true);
      interaction.GeoData.Returns(new ContactLocation(()=>whoIsInformation));
      tracker.Interaction.Returns(interaction);

      var locationRepository = new LocationRepository();

      using (new TrackerSwitcher(tracker))
      {
        //Act
        var location = locationRepository.GetCurrent();
        //Assert      
        location.Should().BeNull();
      }
    }

    [Theory]
    [AutoDbData]
    public void GetCurrent_NullLongitude_ReturnNull(double? latitude, ITracker tracker, CurrentInteraction interaction, WhoIsInformation whoIsInformation)
    {
      //Arrange
      whoIsInformation.Latitude = latitude;
      whoIsInformation.Longitude = null;
      interaction.HasGeoIpData.Returns(true);
      interaction.GeoData.Returns(new ContactLocation(() => whoIsInformation));
      tracker.Interaction.Returns(interaction);

      var locationRepository = new LocationRepository();

      using (new TrackerSwitcher(tracker))
      {
        //Act
        var location = locationRepository.GetCurrent();
        //Assert      
        location.Should().BeNull();
      }
    }

    [Theory]
    [AutoDbData]
    public void GetCurrent_Call_ReturnCityAndCountry(string city, string country, ITracker tracker, CurrentInteraction interaction, WhoIsInformation whoIsInformation)
    {
      //Arrange
      whoIsInformation.City = city;
      whoIsInformation.Country = country;
      interaction.HasGeoIpData.Returns(true);
      interaction.GeoData.Returns(new ContactLocation(() => whoIsInformation));
      tracker.Interaction.Returns(interaction);

      var locationRepository = new LocationRepository();

      using (new TrackerSwitcher(tracker))
      {
        //Act
        var location = locationRepository.GetCurrent();
        //Assert      
        location.Should().NotBeNull();
        location.City.Should().Be(city);
        location.Country.Should().Be(country);
      }
    }
  }
}
