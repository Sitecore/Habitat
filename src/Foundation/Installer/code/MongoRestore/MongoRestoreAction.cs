namespace Sitecore.Foundation.Installer.MongoRestore
{
  using System.Collections.Specialized;

  public class MongoRestoreAction : IPostStepAction
  {
    private readonly IMongoRestoreService mongoRestoreService;

    public MongoRestoreAction(IMongoRestoreService mongoRestoreService)
    {
      this.mongoRestoreService = mongoRestoreService;
    }

    public MongoRestoreAction() : this(new MongoRestoreService())
    {
    }

    public void Run(NameValueCollection metaData)
    {
      if (MongoRestoreSettings.RestoreMongoDatabases)
      {
        this.mongoRestoreService.RestoreDatabases();
      }
    }
  }
}