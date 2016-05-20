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
  using FakeDb.AutoFixture;
  using Ploeh.AutoFixture.AutoNSubstitute;

  public class MapsPointsRepositoryTests
  {
    [Theory]
    [AutoDbData]
    public void GetAll_NullPassed_ShouldThrowArgumentNullException()
    {
      var repository = new MapPointRepository();
      Action a = () => repository.GetAll(null);
      a.ShouldThrow<ArgumentNullException>();
    }

    [Theory]
    [AutoDbData]
    public void GetAll_PointItemPassed_ShouldReturnSinglePoint(Db db)
    {
      var itemid = ID.NewID;
      db.Add(new DbItem("point", itemid, Templates.MapPoint.ID) { { Templates.MapPoint.Fields.Name, "nameField" } });
      var repository = new MapPointRepository();
      var actual = repository.GetAll(db.GetItem(itemid));
      actual.Single().Name.Should().Be("nameField");
    }


    [Theory]
    [AutoDbData]
    public void GetAll_WrongItemPassed_ShouldThrowException([Content]Data.Items.Item item)
    {
      var repository = new MapPointRepository();
      Action a = () => repository.GetAll(item);
      a.ShouldThrow<ArgumentException>();
    }


    [Theory]
    [AutoDbData]
    public void GetAll_PointFolderItemPassed_ShouldCallSearchService(Db db,Foundation.Indexing.Repositories.ISearchServiceRepository searchRepo, [Substitute]Foundation.Indexing.Services.SearchService service)
    {
      var itemid = ID.NewID;
      db.Add(new DbItem("point", itemid, Templates.MapPointsFolder.ID));
      searchRepo.Get().Returns(service);
      var repository = new MapPointRepository(searchRepo);
      repository.GetAll(db.GetItem(itemid));
      service.FindAll().Received(1);
    }



    [Theory]
    [AutoDbData]
    public void GetAll_PointFolderItemPassed_ShouldReturnsItemsFromSearchService([Content]Data.Items.Item[] items,Db db, Foundation.Indexing.Repositories.ISearchServiceRepository searchRepo, [Substitute]Foundation.Indexing.Services.SearchService service, Foundation.Indexing.Models.ISearchResults results, Foundation.Indexing.Models.ISearchResult result)
    {
      var itemid = ID.NewID;
      db.Add(new DbItem("point", itemid, Templates.MapPointsFolder.ID));
      searchRepo.Get().Returns(service);
      service.FindAll().Returns(results);
      var searchResutls = items.Select(x =>
      {
        var sr = Substitute.For<Foundation.Indexing.Models.ISearchResult>();
        sr.Item.Returns(x);
        return sr;
      });

      results.Results.Returns(searchResutls);

      var repository = new MapPointRepository(searchRepo);
      var actual = repository.GetAll(db.GetItem(itemid));
      actual.Count().Should().Be(items.Length);
    }
  }
}