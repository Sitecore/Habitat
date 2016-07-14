namespace Sitecore.Foundation.Installer.Tests.MongoRestore
{
  using NSubstitute;
  using Ploeh.AutoFixture.Xunit2;
  using Sitecore.Foundation.Installer.MongoRestore;
  using Sitecore.Foundation.Testing.Attributes;
  using Xunit;

  public class MongoRestoreProcessorTests
  {
    [Theory]
    [AutoDbData]
    public void Process_IsRestored_DontRestore([Frozen] IMongoRestoreService mongoRestoreService, [Greedy] MongoRestoreProcessor sut )
    {
      //Arrange
      mongoRestoreService.IsRestored(Arg.Is("analytics")).Returns(true);
      //Act
      sut.Process(null);
      //Assert      
      mongoRestoreService.DidNotReceive().RestoreDatabases();
    }


    [Theory]
    [AutoDbData]
    public void Process_IsRestored_DontRebuildIndex([Frozen] IMongoRestoreService mongoRestoreService, [Greedy] MongoRestoreProcessor sut)
    {
      //Arrange
      mongoRestoreService.IsRestored(Arg.Is("analytics")).Returns(true);
      //Act
      sut.Process(null);
      //Assert      
      mongoRestoreService.DidNotReceive().StartRebuildAnalyticsIndexJob();
    }

    [Theory]
    [AutoDbData]
    public void Process_IsNotRestored_Restore([Frozen] IMongoRestoreService mongoRestoreService, [Greedy] MongoRestoreProcessor sut)
    {
      //Arrange
      mongoRestoreService.IsRestored(Arg.Is("analytics")).Returns(false);
      //Act
      sut.Process(null);
      //Assert      
      mongoRestoreService.Received().RestoreDatabases();
    }

    [Theory]
    [AutoDbData]
    public void Process_IsNotRestored_RebuildIndex([Frozen] IMongoRestoreService mongoRestoreService, [Greedy] MongoRestoreProcessor sut)
    {
      //Arrange
      mongoRestoreService.IsRestored(Arg.Is("analytics")).Returns(false);
      //Act
      sut.Process(null);
      //Assert      
      mongoRestoreService.Received().StartRebuildAnalyticsIndexJob();
    }
  }
}
