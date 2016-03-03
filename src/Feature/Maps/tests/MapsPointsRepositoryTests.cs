namespace Sitecore.Feature.Maps.Tests
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Web.Mvc;
  using Controllers;
  using Data;
  using FakeDb;
  using FluentAssertions;
  using Repositories;
  using Xunit;
  using Foundation.Testing.Attributes;
  using Models;
  using NSubstitute;
  using Pipelines;

  public class MapsPointsRepositoryTests
  {
    [Theory]
    [AutoDbData]
    public void GetAll_NullPassed_ShouldThrowArgumentNullException()
    {
      var repository  = new MapPointRepository();
      Action a = () => repository.GetAll(null);
      a.ShouldThrow<ArgumentNullException>();
    }

    [Theory]
    [AutoDbData]
    public void GetAll_PointItemPassed_ShouldReturnSinglePoint(Db db)
    {
      var itemid = ID.NewID;
      db.Add(new DbItem("point",itemid, Templates.MapPoint.ID));
      var repository = new MapPointRepository();
      var actual = repository.GetAll(db.GetItem(itemid));
      actual.Single();

    }



  }
}