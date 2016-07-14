namespace Sitecore.Foundation.Installer.MongoRestore
{
  using System;
  using System.Configuration;
  using System.Linq;
  using System.Threading;
  using MongoDB.Driver;
  using Sitecore.Analytics.Data.DataAccess.MongoDb;
  using Sitecore.Analytics.Model;
  using Sitecore.Analytics.Processing.ProcessingPool;
  using Sitecore.Configuration;
  using Sitecore.ContentSearch;
  using Sitecore.Diagnostics;
  using Sitecore.SecurityModel;

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

    public void RebuildAnalyticsIndex()
    {
      using (new SecurityDisabler())
      {
        ContentSearchManager.GetAnalyticsIndex().Reset();
        var poolPath = "aggregationProcessing/processingPools/live";
        var pool = Factory.CreateObject(poolPath, true) as ProcessingPool;
        var beforeRebuild = pool.GetCurrentStatus().ItemsPending;
        var driver = MongoDbDriver.FromConnectionString("analytics");
        var visitorData = driver.Interactions.FindAllAs<VisitData>();
        var keys = visitorData.Select(data => new InteractionKey(data.ContactId, data.InteractionId));
        foreach (var key in keys)
        {
          var poolItem = new ProcessingPoolItem(key.ToByteArray());
          pool.Add(poolItem);
        }

        while (pool.GetCurrentStatus().ItemsPending > beforeRebuild)
        {
          Thread.Sleep(1000);
        }
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

    protected virtual string GetArgumentsLine(string host, string databaseName, string dumbPath) => $"--drop --host {host} --db {databaseName} --dir {dumbPath}";
  }
}