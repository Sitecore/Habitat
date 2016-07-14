namespace Sitecore.Foundation.Installer.MongoRestore
{
  using System.Linq;
  using System.Threading;
  using Sitecore.Analytics.Data.DataAccess.MongoDb;
  using Sitecore.Analytics.Model;
  using Sitecore.Analytics.Processing.ProcessingPool;
  using Sitecore.Configuration;
  using Sitecore.ContentSearch;
  using Sitecore.Pipelines;

  public class MongoRestoreProcessor
  {
    private readonly IMongoRestoreService mongoRestoreService;

    public MongoRestoreProcessor(IMongoRestoreService mongoRestoreService)
    {
      this.mongoRestoreService = mongoRestoreService;
    }

    public MongoRestoreProcessor() : this(new MongoRestoreService())
    {
    }

    public void Process(PipelineArgs args)
    {
      if (MongoRestoreSettings.RestoreMongoDatabases && !this.mongoRestoreService.IsRestored("analytics"))
      {
        this.mongoRestoreService.RestoreDatabases();
        this.mongoRestoreService.StartRebuildAnalyticsIndexJob();
      }
    }
  }
}