namespace Sitecore.Foundation.Installer.MongoRestore
{
  using System;
  using System.Configuration;
  using MongoDB.Driver;
  using Sitecore.Diagnostics;

  public class MongoRestoreService
  {
    public const string LogToken = "[MongoRestore]:";

    private readonly IMongoPathsProvider mongoFileProvider;
    private readonly IProcessRunner processRunner;

    public MongoRestoreService(IMongoPathsProvider mongoFileProvider, IProcessRunner processRunner)
    {
      this.mongoFileProvider = mongoFileProvider;
      this.processRunner = processRunner;
    }

    public MongoRestoreService() : this(new MongoPathsProvider(), new ProcessRunner() { LogPrefix = LogToken })
    {
    }

    public void RestoreDatabases()
    {
      foreach (var dumpName in this.mongoFileProvider.GetDumpNames())
      {
        this.RestoreDatabase(dumpName);
      }
    }

    public void RestoreDatabase(string dumpName)
    {
      var mongoConnectionString = ConfigurationManager.ConnectionStrings[dumpName]?.ConnectionString;
      if (string.IsNullOrEmpty(mongoConnectionString))
      {
        Log.Error($"{LogToken} Connection string with name {dumpName} wasn't found", this);
      }

      try
      {
        var mongoUrl = new MongoUrl(mongoConnectionString);

        var arguments = this.GetArgumentsLine(mongoUrl.Server.ToString(), mongoUrl.DatabaseName, this.mongoFileProvider.GetDumpDirectory(dumpName));
        this.processRunner.Run(this.mongoFileProvider.MongoRestoreExe, arguments);
      }
      catch (Exception ex)
      {
        Log.Error($"{LogToken} {ex.Message}", ex, this);
      }
    }

    protected virtual string GetArgumentsLine(string host, string databaseName, string dumbPath) => $"--host {host} --db {databaseName} --dir {dumbPath}";
  }
}