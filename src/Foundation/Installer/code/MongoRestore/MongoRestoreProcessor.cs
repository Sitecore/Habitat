namespace Sitecore.Foundation.Installer.MongoRestore
{
  using Sitecore.Pipelines;

  public class MongoRestoreProcessor
  {
    private readonly IMongoRestoreService mongoRestoreService;

    public MongoRestoreProcessor(IMongoRestoreService mongoRestoreService)
    {
      this.mongoRestoreService = mongoRestoreService;
    }

    public MongoRestoreProcessor():this(new MongoRestoreService())
    {
    }

    public void Process(PipelineArgs args)
    {
      if (MongoRestoreSettings.RestoreMongoDatabases && !this.mongoRestoreService.IsRestored("analytics"))
      {
        this.mongoRestoreService.RestoreDatabases();
      }
    }
  }
}