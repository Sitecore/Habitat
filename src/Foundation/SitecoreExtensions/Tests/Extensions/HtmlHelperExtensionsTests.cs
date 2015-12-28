namespace Sitecore.Foundation.SitecoreExtensions.Tests.Extensions
{
  using System.Linq;
  using System.Reflection;
  using System.Web.Mvc;
  using FluentAssertions;
  using log4net.Appender;
  using log4net.Config;
  using log4net.spi;
  using Sitecore.FakeDb.Sites;
  using Sitecore.Foundation.SitecoreExtensions.Extensions;
  using Sitecore.Foundation.SitecoreExtensions.Tests.Common;
  using Sitecore.Sites;
  using Sitecore.Web;
  using Xunit;

  public class HtmlHelperExtensionsTests
  {
    [Theory]
    [AutoDbData]
    public void PageEditorError_Call_LogError(string errorMessage, FakeSiteContext siteContext, MemoryAppender appender)
    {
      //Arrange
      typeof(SiteContext).GetField("displayMode", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(siteContext, DisplayMode.Edit);
      BasicConfigurator.Configure(appender);

      //Act
      using (new SiteContextSwitcher(siteContext))
      {
        HtmlHelperExtensions.PageEditorError(null, errorMessage);
      }
      
      //Assert
      appender.Events.Should().Contain(x=>x.RenderedMessage.Contains(errorMessage));
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
        result = HtmlHelperExtensions.PageEditorError(null, errorMessage);
      }

      //Assert
      result.ToString().Should().BeEmpty();
    }

    [Theory]
    [AutoDbData]
    public void PageEditorError_EditMode_ReturnErrorBlock(string errorMessage, FakeSiteContext siteContext)
    {
      //Arrange
      typeof(SiteContext).GetField("displayMode", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(siteContext, DisplayMode.Edit);

      //Act
      MvcHtmlString result;
      using (new SiteContextSwitcher(siteContext))
      {
        result = HtmlHelperExtensions.PageEditorError(null, errorMessage);
      }

      //Assert
      result.ToString().Should().Contain(errorMessage).And.StartWith("<p").And.EndWith("</p>");
    }
  }
}
