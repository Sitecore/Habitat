namespace Sitecore.Foundation.Installer.Tests.MongoRestore
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using FluentAssertions;
  using log4net.Appender;
  using log4net.Config;
  using log4net.spi;
  using MongoDB.Driver;
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
      appender.Events.Should().Contain(x => x.Level == Level.ERROR);
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
      appender.Events.Should().Contain(x => x.Level == Level.ERROR);
    }

    [Theory]
    [AutoDbData]
    public void RestoreDatabase_NoConnection_DontCallRestore([Frozen]IProcessRunner processRunner, [Greedy]MongoRestoreService sut)
    {
      //Act
      sut.RestoreDatabase("wrongConnnection");
      //Assert      
      processRunner.DidNotReceive().Run(Arg.Any<string>(), Arg.Any<string>());
    }

    [Theory]
    [AutoDbData]
    public void RestoreDatabase_NotMongoConnection_DontCallRestore([Frozen]IProcessRunner processRunner, [Greedy]MongoRestoreService sut)
    {
      //Act
      sut.RestoreDatabase("sql");
      //Assert      
      processRunner.DidNotReceive().Run(Arg.Any<string>(), Arg.Any<string>());
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
      [Frozen]IMongoPathsProvider mongoPathsProvider,
      [Frozen]IProcessRunner processRunner,
      [Greedy]MongoRestoreService sut
      )
    {
      //Arrange
      var dumpNames = new List<string>() { "analytics", "tracking.live", "tracking.history", "tracking.contact" };
      var dbNames = new List<string>() { "habitat_local_analytics", "habitat_local_tracking_live", "habitat_local_tracking_history", "habitat_local_tracking_contact" };
      mongoPathsProvider.GetDumpNames().Returns(dumpNames);
      
      //Act
      sut.RestoreDatabases();
      //Assert
      dbNames.ForEach(db=>processRunner.Received().Run(Arg.Any<string>(),Arg.Is<string>(arg=>arg.Contains($"--db {db}"))));
    }

    [Theory]
    [AutoDbData]
    public void IsRstored_NoConnection_ReturnFalse(
      [Frozen]IMongoPathsProvider mongoPathsProvider,
      [Frozen]IProcessRunner processRunner,
      [Greedy]MongoRestoreService sut)
    {
      sut.IsRestored("wrongConnection").Should().BeFalse();
    }

    [Theory]
    [AutoDbData]
    public void IsRstored_NotMongoConnection_ReturnFalse(
      [Frozen]IMongoPathsProvider mongoPathsProvider,
      [Frozen]IProcessRunner processRunner,
      [Greedy]MongoRestoreService sut)
    {
      sut.Invoking(x => x.IsRestored("sql")).ShouldThrow<MongoConfigurationException>();
    }
  }
}
