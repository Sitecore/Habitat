namespace Sitecore.Foundation.Installer.MongoRestore
{
  using System.Collections.Generic;

  public interface IMongoPathsProvider
  {
    string GetDumpDirectory(string connectioName);
    string MongoRestoreExe { get; }
    IEnumerable<string> GetDumpNames();
  }
}