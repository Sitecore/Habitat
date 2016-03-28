namespace Sitecore.Foundation.Tests.Helpers
{
  using System.IO;
  using System.Reflection;
  using System.Web;
  using System.Web.Mvc;
  using System.Web.Routing;
  using FluentAssertions;
  using log4net.Appender;
  using log4net.Config;
  using NSubstitute;
  using Sitecore.Data;
  using Sitecore.FakeDb.Sites;
  using Sitecore.Foundation.Alerts;
  using Sitecore.Foundation.Alerts.Extensions;
  using Sitecore.Foundation.Alerts.Models;
  using Sitecore.Foundation.Testing.Attributes;
  using Sitecore.Sites;
  using Xunit;

  public class AlertHtmlHelpersTests
  {
    [Theory]
    [AutoDbData]
    public void PageEditorError_Call_LogError(string errorMessage, FakeSiteContext siteContext, MemoryAppender appender, [RegisterView(ViewPath.InfoMessage)]IView view, HtmlHelper htmlHelper)
    {
      //Arrange
      typeof(SiteContext).GetField("displayMode", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(siteContext, DisplayMode.Edit);
      BasicConfigurator.Configure(appender);

      //Act
      using (new SiteContextSwitcher(siteContext))
      {
        AlertHtmlHelpers.PageEditorError(htmlHelper, errorMessage);
      }

      //Assert
      appender.Events.Should().Contain(x => x.RenderedMessage.Contains(errorMessage));
    }

    [Theory]
    [AutoDbData]
    public void PageEditorError_NormalMode_ReturnEmptyString(string errorMessage, FakeSiteContext siteContext)
    {
      //Arrange
      typeof(SiteContext).GetField("displayMode", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(siteContext, DisplayMode.Normal);

      //Act
      MvcHtmlString result;
      using (new SiteContextSwitcher(siteContext))
      {
        result = AlertHtmlHelpers.PageEditorError(null, errorMessage);
      }

      //Assert
      result.ToString().Should().BeEmpty();
    }

    [Theory]
    [AutoDbData]
    public void PageEditorError_EditMode_RenderErrorView(string errorMessage, FakeSiteContext siteContext, [RegisterView(ViewPath.InfoMessage)]IView view, HtmlHelper helper)
    {
      //Arrange
      typeof(SiteContext).GetField("displayMode", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(siteContext, DisplayMode.Edit);

      //Act
      MvcHtmlString result;
      using (new SiteContextSwitcher(siteContext))
      {
        result = AlertHtmlHelpers.PageEditorError(helper, errorMessage);

        //Assert
        view.Received().Render(Arg.Is<ViewContext>(v=>v.ViewData.Model.As<InfoMessage>().Type == InfoMessage.MessageType.Error), Arg.Any<TextWriter>());
      }
    }

    [Theory]
    [AutoDbData]
    public void PageEditorError_EditMode_RenderErrorViewFriendlyMessage(string errorMessage, string friendlyMessage, FakeSiteContext siteContext, [RegisterView(ViewPath.InfoMessage)]IView view, HtmlHelper helper)
    {
      //Arrange
      typeof(SiteContext).GetField("displayMode", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(siteContext, DisplayMode.Edit);

      //Act
      MvcHtmlString result;
      using (new SiteContextSwitcher(siteContext))
      {
        result = AlertHtmlHelpers.PageEditorError(helper, errorMessage, friendlyMessage, ID.NewID, ID.NewID);

        //Assert
        view.Received().Render(Arg.Is<ViewContext>(v => v.ViewData.Model.As<InfoMessage>().Type == InfoMessage.MessageType.Error), Arg.Any<TextWriter>());
      }
    }

    [Theory]
    [AutoDbData]
    public void PageEditorErrorFriendlyMessage_NormalMode_ReturnEmptyString(string errorMessage, string  friendlyMessage, FakeSiteContext siteContext)
    {
      //Arrange
      typeof(SiteContext).GetField("displayMode", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(siteContext, DisplayMode.Normal);

      //Act
      MvcHtmlString result;
      using (new SiteContextSwitcher(siteContext))
      {
        result = AlertHtmlHelpers.PageEditorError(null, errorMessage, friendlyMessage, ID.NewID, ID.NewID);
      }

      //Assert
      result.ToString().Should().BeEmpty();
    }
  }
}