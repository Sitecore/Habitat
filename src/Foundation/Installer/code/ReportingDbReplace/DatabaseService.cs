namespace Sitecore.Foundation.Installer.ReportingDbReplace
{
  using System;
  using System.Collections.Specialized;
  using System.Data.SqlClient;
  using System.IO;
  using Microsoft.SqlServer.Management.Common;
  using Microsoft.SqlServer.Management.Smo;
  using Sitecore.Diagnostics;

  public class DatabaseService : IDatabaseService
  {
    public void ReplaceDatabase(string connectionString, string dbReplacementPath)
    {
      var connection = new SqlConnectionStringBuilder(connectionString);
      var databaseName = connection.InitialCatalog;
      if (!File.Exists(dbReplacementPath))
      {
        Log.Error($"Can't find file by path {dbReplacementPath}", this);
        return;
      }

      ServerConnection serverConnection = null;
      try
      {
        serverConnection = new ServerConnection(new SqlConnection(connectionString));
        var server = new Server(serverConnection);

        if (server.Databases[databaseName] != null)
        {
          server.KillAllProcesses(databaseName);
          server.DetachDatabase(databaseName, true);
        }

        server.AttachDatabase(databaseName,new StringCollection() {dbReplacementPath}, AttachOptions.RebuildLog);
      }
      catch (Exception ex)
      {
        Log.Error(ex.Message, ex, this);
      }
      finally
      {
        serverConnection?.Disconnect();
      }
    }
  }
}