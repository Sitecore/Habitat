namespace Sitecore.Foundation.Installer.MongoRestore
{
  using Sitecore.Jobs;

  public interface IMongoRestoreService
  {
    void RestoreDatabase(string dumpName);
    void RestoreDatabases();
    bool IsRestored(string connectionName);
    Job StartRebuildAnalyticsIndexJob();
  }
}