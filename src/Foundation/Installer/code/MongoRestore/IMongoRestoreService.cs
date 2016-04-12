namespace Sitecore.Foundation.Installer.MongoRestore
{
  public interface IMongoRestoreService
  {
    void RestoreDatabase(string dumpName);
    void RestoreDatabases();
    bool IsRestored(string connectionName);
  }
}