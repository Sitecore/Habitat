namespace Sitecore.Foundation.Installer.Tests.MongoRestore
{
  using System.Collections.Generic;
  using System.Linq;
  using FluentAssertions;
  using log4net.Appender;
  using log4net.Config;
  using log4net.spi;
  using NSubstitute;
  using Ploeh.AutoFixture.Xunit2;
  using Sitecore.Foundation.Installer.MongoRestore;
  using Sitecore.Foundation.Testing.Attributes;
  using Xunit;

  public class MongoRestoreServiceTests
  {
    [Theory]
    [AutoDbData]
    public void RestoreDatabase_NoConnection_LogAnError(MemoryAppender appender, MongoRestoreService sut)
    {
      //Arrange
      BasicConfigurator.Configure(appender);
      //Act
      sut.RestoreDatabase("wrongConnnection");
      //Assert      
      appender.Events.Should().OnlyContain(x => x.Level == Level.ERROR);
    }

    [Theory]
    [AutoDbData]
    public void RestoreDatabase_NotMongoConnection_LogAnError(MemoryAppender appender, MongoRestoreService sut)
    {
      //Arrange
      BasicConfigurator.Configure(appender);
      //Act
      sut.RestoreDatabase("sql");
      //Assert      
      appender.Events.Should().OnlyContain(x => x.Level == Level.ERROR);
    }

    [Theory]
    [AutoDbData]
    public void RestoreDatabase_MongoConnection_RunRestoreProcessFile(
      string mongoRestorePath,
      [Frozen]IMongoPathsProvider mongoPathsProvider,
      [Frozen]IProcessRunner processRunner,
      [Greedy]MongoRestoreService sut)
    {
      //Arrange
      mongoPathsProvider.MongoRestoreExe.Returns(mongoRestorePath);
      //Act
      sut.RestoreDatabase("analytics");
      //Assert      
      processRunner.Received(1).Run(mongoPathsProvider.MongoRestoreExe, Arg.Any<string>());
    }

    [Theory]
    [AutoDbData]
    public void RestoreDatabase_MongoConnection_RunRestoreWithArguments(
      string dumpDirectory,
      [Frozen]IMongoPathsProvider mongoPathsProvider,
      [Frozen]IProcessRunner processRunner,
      [Greedy]MongoRestoreService sut)
    {
      //Arrange
      mongoPathsProvider.GetDumpDirectory(Arg.Is("analytics")).Returns(dumpDirectory);
      //Act
      sut.RestoreDatabase("analytics");
      //Assert      
      processRunner.Received(1).Run(Arg.Any<string>(), Arg.Is<string>(x => x.Contains("--host localhost:27017") && x.Contains("--db habitat_local_analytics") && x.Contains("--dir ")));
    }

    [Theory]
    [AutoDbData]
    public void RestoreDatabases_Call_RestoreAllDumps(
      List<string> dumpNames,
      [Frozen]IMongoPathsProvider mongoPathsProvider,
      [Frozen]IProcessRunner processRunner
      )
    {
      //Arrange
      mongoPathsProvider.GetDumpNames().Returns(dumpNames);
      var sut = Substitute.ForPartsOf<MongoRestoreService>(mongoPathsProvider, processRunner);
      //Act
      sut.RestoreDatabases();
      //Assert      
      sut.Received(dumpNames.Count).RestoreDatabase(Arg.Is<string>(x => dumpNames.Contains(x)));
    }
  }
}
