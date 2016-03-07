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
  using Ploeh.AutoFixture.AutoNSubstitute;
  using Sitecore.Data;
  using Sitecore.Data.Items;
  using Sitecore.FakeDb;
  using Sitecore.FakeDb.AutoFixture;
  using Sitecore.Foundation.Alerts.Exceptions;
  using Sitecore.Mvc.Common;
  using Sitecore.Mvc.Presentation;

  public class DemoControllerTests
  {
    [Theory]
    [AutoDbData]
    public void DemoContent_RenderingContextItemInitialized_ShouldReturnDemoContentView(Db db,IContactProfileProvider contact, IProfileProvider profile, ITracker tracker)
    {
      //arrange

      var itemID = ID.NewID;
      db.Add(new DbItem("ctx",itemID, Templates.DemoContent.ID));
      var controller = new DemoController(contact, profile);
      var context = new RenderingContext();
      context.ContextItem =  db.GetItem(itemID);
      ContextService.Get().Push(context);
      using (new TrackerSwitcher(tracker))
      {
        controller.DemoContent().As<ViewResult>().Model.Should().BeOfType<DemoContent>();
      }
    }

    [Theory]
    [AutoDbData]
    public void DemoContent_RenderingContextItemNotInitialized_ShouldThrowException(IContactProfileProvider contact, IProfileProvider profile, ITracker tracker)
    {
      //arrange

      var controller = new DemoController(contact, profile);
      var context = new RenderingContext();
      ContextService.Get().Push(context);
      using (new TrackerSwitcher(tracker))
      {
        controller.Invoking(x => x.DemoContent()).ShouldThrow<InvalidDataSourceItemException>();
      }
    }

    [Theory]
    [AutoDbData]
    public void DemoContent_RenderingContextNotDerivedFromSpecificTemplate_ShouldThrowException([Content]Item ctxItem, IContactProfileProvider contact, IProfileProvider profile, ITracker tracker)
    {
      //arrange
      var controller = new DemoController(contact, profile);
      var context = new RenderingContext();
      context.ContextItem = ctxItem;
      ContextService.Get().Push(context);
      using (new TrackerSwitcher(tracker))
      {
        controller.Invoking(x => x.DemoContent()).ShouldThrow<InvalidDataSourceItemException>();
      }
    }


    [Theory]
    [AutoDbData]
    public void EndVisit_ShouldReturnRedirectToRoot(IContactProfileProvider contact, IProfileProvider profile, [Substitute]ControllerContext ctx)
    {
      //arrange
      var controller = new DemoController(contact, profile);
      controller.ControllerContext = ctx;
      controller.EndVisit().As<RedirectResult>().Url.Should().Be("/");
    }

    [Theory]
    [AutoDbData]
    public void EndVisit_ShouldEndSession(IContactProfileProvider contact, IProfileProvider profile, [Substitute]ControllerContext ctx)
    {
      //arrange
      var controller = new DemoController(contact, profile);
      controller.ControllerContext = ctx;
      controller.EndVisit();
      ctx.HttpContext.Session.Received(1).Abandon();
    }
  }
}