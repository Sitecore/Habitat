﻿namespace Sitecore.Feature.News.Tests
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using System.Threading.Tasks;
  using FluentAssertions;
  using Sitecore.Data;
  using Sitecore.Data.Items;
  using Sitecore.FakeDb;
  using Sitecore.FakeDb.AutoFixture;
  using Sitecore.Feature.News.Repositories;
  using Sitecore.Feature.News.Tests.Extensions;
  using Sitecore.Foundation.Alerts.Exceptions;
  using Xunit;

  public class NewsRepositoryFactoryTests
  {
    [Theory]
    [AutoDbData]
    public void Create_ShouldReturnNewsRepository(NewsRepositoryFactory factory, Db db, string itemName, ID itemId)
    {
      db.Add(new DbItem(itemName, itemId, Templates.NewsFolder.ID));
      var contextItem = db.GetItem(itemId);
      var repo = factory.Create(contextItem);
      repo.Should().BeAssignableTo<INewsRepository>();
    }

    [Theory]
    [AutoDbData]
    public void Create_ContextItemWithWrongTemplate_ShouldReturnNewsRepository(NewsRepositoryFactory factory, [Content] Item contextItem)
    {
      var repo = factory.Invoking(x => x.Create(contextItem)).ShouldThrow<InvalidDataSourceItemException>();
    }
  }
}
