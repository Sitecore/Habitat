namespace Sitecore.Feature.Maps.Tests
{
  using System;
  using System.Collections.Generic;
  using System.Web.Mvc;
  using Controllers;
  using FakeDb;
  using FluentAssertions;
  using Repositories;
  using Xunit;
  using Foundation.Testing.Attributes;
  using Models;
  using NSubstitute;
  using Pipelines;


  public class MapsControllerTests
  {
    [Theory]
    [AutoDbData]
    public void Constructor_ShouldNotThrow(IMapPointRepository mapPointRepository)
    {
      Action act = () => new MapsController(mapPointRepository);
      act.ShouldNotThrow();
    }

    [Theory]
    [AutoDbData]
    public void GetMapPoints_ItemIdPassed_ShouldCallRepositoryWithItem(IMapPointRepository mapPointRepository, [FakeDb.AutoFixture.Content]Data.Items.Item item)
    {
      var controller = new MapsController(mapPointRepository);
      controller.GetMapPoints(item.ID.Guid);
      mapPointRepository.GetAll(item).ReceivedWithAnyArgs(1);
    }

    [Theory]
    [AutoDbData]
    public void GetMapPoints_ItemIdPassed_PointsReturnedFormRepo(IMapPointRepository mapPointRepository, Db db, MapPoint[] points,[FakeDb.AutoFixture.Content]Data.Items.Item item)
    {
      mapPointRepository.GetAll(null).ReturnsForAnyArgs(points);

      var controller = new MapsController(mapPointRepository);
      var actualPoints = controller.GetMapPoints(item.ID.Guid).Data as IEnumerable<MapPoint>;
      actualPoints.ShouldBeEquivalentTo(points);
    }
  }
}