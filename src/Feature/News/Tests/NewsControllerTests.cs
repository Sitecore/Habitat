namespace Sitecore.Feature.News.Tests
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using System.Threading.Tasks;
  using System.Web.Mvc;
  using FluentAssertions;
  using Sitecore.Data;
  using Sitecore.FakeDb;
  using Sitecore.Feature.News.Controllers;
  using Sitecore.Feature.News.Repositories;
  using Sitecore.Feature.News.Tests.Extensions;
  using Sitecore.Mvc.Common;
  using Sitecore.Mvc.Presentation;
  using Xunit;

  public class NewsControllerTests
  {
    [Theory]
    [AutoDbData]
    public void NewsList_ShouldReturnViewResult(Db db, string itemName, ID itemId, INewsRepository repository)
    {
      //Arrange
      var controller = new NewsController(repository);
      db.Add(new DbItem(itemName, itemId, Templates.NewsFolder.ID));
      var contextItem = db.GetItem(itemId);
      var context = new RenderingContext();
      context.Rendering = new Rendering();
      context.Rendering.Item = contextItem;
      ContextService.Get().Push(context);
      //Act
      var list = controller.NewsList();
      //Assert      
      list.Should().BeOfType<ViewResult>();
    }

    [Theory]
    [AutoDbData]
    public void LatestNews_ShouldReturnViewResult(Db db, string itemName, ID itemId, INewsRepository repository)
    {
      //Arrange
      var controller = new NewsController(repository);
      db.Add(new DbItem(itemName, itemId, Templates.NewsFolder.ID));
      var contextItem = db.GetItem(itemId);
      var context = new RenderingContext();
      context.Rendering = new Rendering();
      context.Rendering.Item = contextItem;
      ContextService.Get().Push(context);
      //Act
      var list = controller.LatestNews();
      //Assert      
      list.Should().BeOfType<ViewResult>();
    }
  }
}
