namespace Sitecore.Foundation.Installer.MongoRestore
{
  public class MongoRestoreAction:IPostStepAction
  {
    private readonly IMongoRestoreService mongoRestoreService;

    public MongoRestoreAction(IMongoRestoreService mongoRestoreService)
    {
      this.mongoRestoreService = mongoRestoreService;
    }

    public MongoRestoreAction():this(new MongoRestoreService())
    {
    }

    public void Run()
    {
      if (MongoRestoreSettings.RestoreMongoDatabases)
      {
        this.mongoRestoreService.RestoreDatabases();
      }
    }
  }
}