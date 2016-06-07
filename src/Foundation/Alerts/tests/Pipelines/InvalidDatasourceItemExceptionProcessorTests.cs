namespace Sitecore.Foundation.Tests.Pipelines
{
  using System;
  using System.Reflection;
  using System.Web;
  using System.Web.Mvc;
  using FluentAssertions;
  using log4net.Appender;
  using log4net.Config;
  using NSubstitute;
  using Ploeh.AutoFixture.AutoNSubstitute;
  using Ploeh.AutoFixture.Xunit2;
  using Sitecore.Data;
  using Sitecore.FakeDb.Sites;
  using Sitecore.Foundation.Alerts;
  using Sitecore.Foundation.Alerts.Exceptions;
  using Sitecore.Foundation.Alerts.Pipelines.MvcException;
  using Sitecore.Foundation.Dictionary.Repositories;
  using Sitecore.Foundation.Testing;
  using Sitecore.Foundation.Testing.Attributes;
  using Sitecore.Mvc.Pipelines.MvcEvents.Exception;
  using Sitecore.Sites;
  using Xunit;

  public class InvalidDatasourceItemExceptionProcessorTests
  {
    public InvalidDatasourceItemExceptionProcessorTests()
    {
      HttpContext.Current = HttpContextMockFactory.Create();
      HttpContext.Current.Items["DictionaryPhraseRepository.Current"] = Substitute.For<IDictionaryPhraseRepository>();
    }

    [Theory]
    [AutoDbData]
    public void Process_HandledException_DontSetView(FakeSiteContext siteContext, InvalidDatasourceItemExceptionProcessor processor, [Modest]ExceptionContext exceptionContext, [Substitute]ExceptionArgs exceptionArgs)
    {
      //Arrange
      typeof(SiteContext).GetField("displayMode", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(siteContext, DisplayMode.Edit);
      exceptionArgs.ExceptionContext.ExceptionHandled = true;

      //Act
      using (new SiteContextSwitcher(siteContext))
      {
        processor.Process(exceptionArgs);

        //Assert
        exceptionArgs.ExceptionContext.Result.Should().BeOfType<EmptyResult>();
      }
    }

    [Theory]
    [AutoDbData]
    public void Process_NotInvalidDataSourceItemException_DontSetView(FakeSiteContext siteContext, InvalidDatasourceItemExceptionProcessor processor, [Modest]ExceptionContext exceptionContext, [Substitute]ExceptionArgs exceptionArgs, Exception testException)
    {
      //Arrange
      typeof(SiteContext).GetField("displayMode", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(siteContext, DisplayMode.Edit);
      exceptionArgs.ExceptionContext.ExceptionHandled = false;
      exceptionArgs.ExceptionContext.Exception = testException;

      //Act
      using (new SiteContextSwitcher(siteContext))
      {
        processor.Process(exceptionArgs);

        //Assert
        exceptionArgs.ExceptionContext.Result.Should().BeOfType<EmptyResult>();
      }
    }

    [Theory]
    [AutoDbData]
    public void Process_DataSourceExceptionInNormalMode_HandleException(FakeSiteContext siteContext, InvalidDatasourceItemExceptionProcessor processor, [Modest]ExceptionContext exceptionContext, [Substitute]ExceptionArgs exceptionArgs, [Modest]InvalidDataSourceItemException exception)
    {
      //Arrange
      typeof(SiteContext).GetField("displayMode", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(siteContext, DisplayMode.Normal);
      exceptionArgs.ExceptionContext.ExceptionHandled = false;
      exceptionArgs.ExceptionContext.Exception = exception;

      //Act
      using (new SiteContextSwitcher(siteContext))
      {
        processor.Process(exceptionArgs);

        //Assert
        exceptionArgs.ExceptionContext.ExceptionHandled.Should().BeTrue();
        exceptionArgs.ExceptionContext.Result.Should().BeOfType<EmptyResult>();
      }
    }

    [Theory]
    [AutoDbData]
    public void Process_DataSourceExceptionInEditMode_SetView(Database db, FakeSiteContext siteContext, InvalidDatasourceItemExceptionProcessor processor, [Modest]ExceptionContext exceptionContext, [Substitute]ExceptionArgs exceptionArgs, [Modest]InvalidDataSourceItemException exception)
    {
      //Arrange
      typeof(SiteContext).GetField("displayMode", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(siteContext, DisplayMode.Edit);
      exceptionArgs.ExceptionContext.ExceptionHandled = false;
      exceptionArgs.ExceptionContext.Exception = exception;

      //Act
      using (new SiteContextSwitcher(siteContext))
      {
        processor.Process(exceptionArgs);

        //Assert
        exceptionArgs.ExceptionContext.Result.Should().BeOfType<ViewResult>().Which.ViewName.Should().Be(ViewPath.InfoMessage);
        exceptionArgs.ExceptionContext.ExceptionHandled.Should().BeTrue();
      }
    }

    [Theory]
    [AutoDbData]
    public void Process_DataSourceException_LogError(Database db, FakeSiteContext siteContext, InvalidDatasourceItemExceptionProcessor processor, [Modest]ExceptionContext exceptionContext, [Substitute]ExceptionArgs exceptionArgs, [Modest]InvalidDataSourceItemException exception, MemoryAppender appender)
    {
      //Arrange
      typeof(SiteContext).GetField("displayMode", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(siteContext, DisplayMode.Edit);
      BasicConfigurator.Configure(appender);

      exceptionArgs.ExceptionContext.ExceptionHandled = false;
      exceptionArgs.ExceptionContext.Exception = exception;

      //Act
      using (new SiteContextSwitcher(siteContext))
      {
        processor.Process(exceptionArgs);

        //Assert
        appender.Events.Should().Contain(x => x.RenderedMessage.Contains(exception.Message));
      }
    }
  }
}
