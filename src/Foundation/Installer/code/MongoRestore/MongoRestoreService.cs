namespace Sitecore.Foundation.Installer.MongoRestore
{
  using System;
  using System.Configuration;
  using MongoDB.Driver;
  using Sitecore.Diagnostics;

  public class MongoRestoreService : IMongoRestoreService
  {
    public const string LogToken = "[MongoRestore]:";

    private readonly IMongoPathsProvider mongoFileProvider;
    private readonly IProcessRunner processRunner;

    public MongoRestoreService(IMongoPathsProvider mongoFileProvider, IProcessRunner processRunner)
    {
      this.mongoFileProvider = mongoFileProvider;
      this.processRunner = processRunner;
    }

    public MongoRestoreService() : this(new MongoPathsProvider(), new ProcessRunner
                                                                  {
                                                                    LogPrefix = LogToken
                                                                  })
    {
    }

    public void RestoreDatabases()
    {
      Log.Info($"{LogToken} Starting restore for all databases", this);

      foreach (var dumpName in this.mongoFileProvider.GetDumpNames())
      {
        this.RestoreDatabase(dumpName);
      }

      Log.Info($"{LogToken} Databases restoring has been finished", this);
    }

    public void RestoreDatabase(string dumpName)
    {
      var mongoConnectionString = ConfigurationManager.ConnectionStrings[dumpName]?.ConnectionString;
      if (string.IsNullOrEmpty(mongoConnectionString))
      {
        Log.Error($"{LogToken} Connection string with name {dumpName} wasn't found", this);
      }

      Log.Info($"{LogToken} Starting restore for {dumpName} database", this);

      try
      {
        var mongoUrl = new MongoUrl(mongoConnectionString);

        var arguments = this.GetArgumentsLine(mongoUrl.Server.ToString(), mongoUrl.DatabaseName, this.mongoFileProvider.GetDumpDirectory(dumpName));
        this.processRunner.Run(this.mongoFileProvider.MongoRestoreExe, arguments);
        this.MarkAsRestored(mongoUrl);
      }
      catch (Exception ex)
      {
        Log.Error($"{LogToken} {ex.Message}", ex, this);
      }
    }

    public bool IsRestored(string connectionName)
    {
      MongoServer server = null;
      var connectionString = ConfigurationManager.ConnectionStrings[connectionName]?.ConnectionString;

      if (string.IsNullOrEmpty(connectionString))
      {
        return false;
      }

      try
      {
        var mongoUrl = new MongoUrl(connectionString);
        var mongoClient = new MongoClient(mongoUrl);
        server = mongoClient.GetServer();

        return server.GetDatabase(mongoUrl.DatabaseName).CollectionExists(MongoRestoreSettings.RestoredDbTokenCollection);
      }
      catch (FormatException ex)
      {
        Log.Error("Wrong connection string format", ex, this);
        throw;
      }
      finally
      {
        server?.Disconnect();
      }
    }

    private void MarkAsRestored(MongoUrl mongoUrl)
    {
      MongoServer server = null;
      try
      {
        var mongoClient = new MongoClient(mongoUrl);
        server = mongoClient.GetServer();

        server.GetDatabase(mongoUrl.DatabaseName).CreateCollection(MongoRestoreSettings.RestoredDbTokenCollection);
      }
      finally
      {
        server?.Disconnect();
      }
    }

    protected virtual string GetArgumentsLine(string host, string databaseName, string dumbPath) => $"--host {host} --db {databaseName} --dir {dumbPath}";
  }
}