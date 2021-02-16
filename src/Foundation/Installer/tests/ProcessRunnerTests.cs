namespace Sitecore.Foundation.Installer.Tests
{
    using FluentAssertions;
    using log4net.Appender;
    using log4net.Config;
    using log4net.spi;
    using Ploeh.AutoFixture.Xunit2;
    using Sitecore.Foundation.Testing.Attributes;
    using Xunit;

    public class ProcessRunnerTests
  {
    [Theory]
    [AutoData]
    public void Run_InvalidArguments_ThrowException(ProcessRunner sut)
    {
      //Assert      
      sut.Invoking(x=>x.Run("xcopy", "/d/d/d/d/d/")).Should().Throw<ProcessException>();
    }

    [Theory]
    [AutoData]
    public void Run_InvalidCommand_ThrowException(ProcessRunner sut)
    {
      //Assert      
      sut.Invoking(x => x.Run("aaaaa", "/d/d/d/d/d/")).Should().Throw<ProcessException>();
    }

    [Theory]
    [AutoDbData]
    public void Run_ValidCommand_LogOutputToDebug(MemoryAppender appender, ProcessRunner sut)
    {
      //Arrange
      BasicConfigurator.Configure(appender);
      
      //Act
      sut.Run("help", "dir");
      
      //Assert      
      appender.Events.Should().OnlyContain(x => x.Level == Level.DEBUG);
    }

    [Theory]
    [AutoDbData]
    public void Run_ValidCommand_AddPrefixToLog(string prefix, MemoryAppender appender, ProcessRunner sut)
    {
      //Arrange
      sut.LogPrefix = prefix;
      BasicConfigurator.Configure(appender);

      //Act
      sut.Run("help", "dir");
      
      //Assert      
      appender.Events.Should().OnlyContain(x => x.RenderedMessage.Contains(prefix));
    }
  }
}
