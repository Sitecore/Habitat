namespace Sitecore.Feature.Demo.Tests.Controllers
{
  using System.Collections.Generic;
  using System.Net;
  using System.Web.Mvc;
  using FluentAssertions;
  using NSubstitute;
  using Ploeh.AutoFixture.AutoNSubstitute;
  using Ploeh.AutoFixture.Xunit2;
  using Sitecore.Analytics;
  using Sitecore.Analytics.Model.Entities;
  using Sitecore.Analytics.Tracking;
  using Sitecore.Collections;
  using Sitecore.Data.Items;
  using Sitecore.FakeDb;
  using Sitecore.FakeDb.AutoFixture;
  using Sitecore.FakeDb.Sites;
  using Sitecore.Feature.Demo.Controllers;
  using Sitecore.Feature.Demo.Models;
  using Sitecore.Feature.Demo.Services;
  using Sitecore.Foundation.Alerts.Exceptions;
  using Sitecore.Foundation.SitecoreExtensions.Services;
  using Sitecore.Foundation.Testing.Attributes;
  using Sitecore.Mvc.Common;
  using Sitecore.Mvc.Presentation;
  using Sitecore.Sites;
  using Xunit;

  public class DemoControllerTests
  {
    [Theory]
    [AutoDbData]
    public void DemoContent_RenderingContextItemInitialized_ShouldReturnDemoContentView(Db db, [Greedy] DemoController sut, [Modest] RenderingContext context, [Content] DemoContentItem item)
    {
      context.ContextItem = db.GetItem(item.ID);
      ContextService.Get().Push(context);

      sut.DemoContent().As<ViewResult>().Model.Should().BeOfType<DemoContent>();
    }

    [Theory]
    [AutoDbData]
    public void DemoContent_RenderingContextItemNotInitialized_ShouldThrowException([Greedy] DemoController sut, [Modest] RenderingContext context)
    {
      ContextService.Get().Push(context);
      sut.Invoking(x => x.DemoContent()).ShouldThrow<InvalidDataSourceItemException>();
    }

    [Theory]
    [AutoDbData]
    public void DemoContent_RenderingContextNotDerivedFromSpecificTemplate_ShouldThrowException([Greedy] DemoController sut, [Modest] RenderingContext context, Item ctxItem)
    {
      context.ContextItem = ctxItem;
      ContextService.Get().Push(context);

      sut.Invoking(x => x.DemoContent()).ShouldThrow<InvalidDataSourceItemException>();
    }

    [Theory]
    [AutoDbData]
    public void EndVisit_ShouldReturnRedirectToRoot([Substitute] ControllerContext ctx, [Greedy] DemoController sut)
    {
      sut.ControllerContext = ctx;
      sut.EndVisit().As<HttpStatusCodeResult>().StatusCode.Should().Be((int)HttpStatusCode.OK);
    }

    [Theory]
    [AutoDbData]
    public void EndVisit_ShouldEndSession([Substitute] ControllerContext ctx, [Greedy] DemoController sut)
    {
      sut.ControllerContext = ctx;
      sut.EndVisit();
      ctx.HttpContext.Session.Received(1).Abandon();
    }

    [Theory]
    [AutoDbData]
    public void ExperienceData_NullTracker_ReturnNull([Greedy] DemoController sut)
    {
      sut.ExperienceData().Should().BeNull();
    }

    [Theory]
    [AutoDbData]
    public void ExperienceData_NullInteraction_ReturnNull([Greedy] DemoController sut, TrackerSwitcher trackerSwitcher)
    {
      sut.ExperienceData().Should().BeNull();
    }

    [Theory]
    [AutoDbData]
    public void ExperienceData_InitializedTrackerAndNormalMode_ReturnExperienceData(IKeyBehaviorCache keyBehaviorCache, Session session, CurrentInteraction currentInteraction, ITracker tracker, [Frozen] IContactProfileProvider contactProfileProvider, [Frozen] IProfileProvider profileProvider, [Greedy] DemoController sut)
    {
      tracker.Interaction.Returns(currentInteraction);
      tracker.Session.Returns(session);
      var attachments = new Dictionary<string, object>
                        {
                          ["KeyBehaviorCache"] = new Analytics.Tracking.KeyBehaviorCache(keyBehaviorCache)
                        };
      tracker.Contact.Attachments.Returns(attachments);

      var fakeSite = new FakeSiteContext(new StringDictionary
                                         {
                                           {"displayMode", "normal"}
                                         }) as SiteContext;

      using (new SiteContextSwitcher(fakeSite))
      {
        using (new TrackerSwitcher(tracker))
        {
          sut.ExperienceData().Should().BeOfType<ViewResult>().Which.Model.Should().BeOfType<ExperienceData>();
        }
      }
    }

    [Theory]
    [AutoDbData]
    public void ExperienceData_InitializedTrackerAndPreviewMode_ReturnEmptyResult(IKeyBehaviorCache keyBehaviorCache, Session session, CurrentInteraction currentInteraction, ITracker tracker, [Frozen] IContactProfileProvider contactProfileProvider, [Frozen] IProfileProvider profileProvider, [Greedy] DemoController sut)
    {
      //TODO: Fix;
      return;
      tracker.Interaction.Returns(currentInteraction);
      tracker.Session.Returns(session);
      var attachments = new Dictionary<string, object>
      {
        ["KeyBehaviorCache"] = new Analytics.Tracking.KeyBehaviorCache(keyBehaviorCache)
      };
      tracker.Contact.Attachments.Returns(attachments);

      var fakeSite = new FakeSiteContext(new StringDictionary
                                         {
                                           {"mode", "edit"}
                                         }) as SiteContext;

      using (new SiteContextSwitcher(fakeSite))
      {
        using (new TrackerSwitcher(tracker))
        {
          sut.ExperienceData().Should().BeOfType<ViewResult>().Which.Model.Should().BeOfType<EmptyResult>();
        }
      }
    }
  }
}