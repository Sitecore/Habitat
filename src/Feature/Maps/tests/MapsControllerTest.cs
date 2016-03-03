namespace Sitecore.Feature.Maps.Tests
{
  using System;
  using Controllers;
  using FluentAssertions;
  using Repositories;
  using Xunit;
  using Foundation.Testing.Attributes;

  public class MapsControllerTest
  {
    [Theory]
    [AutoDbData]
    public void DefaultConstructor_ShouldNotThrow(IMapPointRepository mapPointRepository)
    {
      Action act = () => new MapsController(mapPointRepository);
      act.ShouldNotThrow();
    }
  }
}