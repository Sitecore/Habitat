namespace Sitecore.Feature.Maps.Tests
{
  using System;
  using System.Web.Mvc;
  using Controllers;
  using Extensions;
  using FluentAssertions;
  using Repositories;
  using Xunit;

  public class MapsControllerTest
  {
    [Theory]    
    [AutoDbData]
    public void DefaultConstructor_ShouldNotThrow(IMapRepository mapRepository)
    {
      Action act = () => new MapsController(mapRepository);
      act.ShouldNotThrow();
    }   
  }
}
